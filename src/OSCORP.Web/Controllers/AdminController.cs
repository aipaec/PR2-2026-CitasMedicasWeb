using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Data;
using OSCORP.Web.Models;
using OSCORP.Web.ViewModels;

namespace OSCORP.Web.Controllers;

[Authorize(Roles = "Administrador")]
public class AdminController(OscorpDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Pacientes = await db.Pacientes.CountAsync();
        ViewBag.Doctores = await db.Doctores.CountAsync();
        ViewBag.Citas = await db.CitasMedicas.CountAsync();
        ViewBag.Horarios = await db.Horarios.CountAsync();
        return View();
    }

    public async Task<IActionResult> Schedules()
    {
        var schedules = await db.Horarios.Include(h => h.Doctor)!.ThenInclude(d => d!.Usuario)
            .OrderBy(h => h.Fecha).ThenBy(h => h.HoraInicio).ToListAsync();
        return View(schedules);
    }

    [HttpGet]
    public async Task<IActionResult> CreateSchedule()
    {
        await LoadDoctors();
        return View(new ScheduleViewModel { Fecha = DateTime.Today.AddDays(1) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSchedule(ScheduleViewModel model)
    {
        if (model.Fecha.Date < DateTime.Today)
            ModelState.AddModelError(nameof(model.Fecha), "La fecha no puede estar en el pasado.");
        if (model.HoraFin <= model.HoraInicio)
            ModelState.AddModelError(nameof(model.HoraFin), "La hora final debe ser posterior a la hora inicial.");

        var doctorExists = await db.Doctores.AnyAsync(d => d.IdDoctor == model.IdDoctor);
        if (!doctorExists)
            ModelState.AddModelError(nameof(model.IdDoctor), "Selecciona un doctor válido.");

        var overlaps = await db.Horarios.AnyAsync(h =>
            h.IdDoctor == model.IdDoctor &&
            h.Fecha == model.Fecha.Date &&
            h.Estado != "Cancelado" &&
            model.HoraInicio < h.HoraFin &&
            model.HoraFin > h.HoraInicio);
        if (overlaps)
            ModelState.AddModelError("", "El horario se cruza con otro horario del doctor.");

        if (!ModelState.IsValid)
        {
            await LoadDoctors();
            return View(model);
        }

        db.Horarios.Add(new Horario
        {
            IdDoctor = model.IdDoctor,
            Fecha = model.Fecha.Date,
            HoraInicio = model.HoraInicio,
            HoraFin = model.HoraFin,
            Estado = "Disponible"
        });
        await db.SaveChangesAsync();
        TempData["Mensaje"] = "Horario creado correctamente.";
        return RedirectToAction(nameof(Schedules));
    }

    private async Task LoadDoctors()
    {
        var doctors = await db.Doctores.Include(d => d.Usuario)
            .OrderBy(d => d.Usuario!.Nombre).ToListAsync();
        ViewBag.Doctores = doctors.Select(d => new SelectListItem(d.Usuario?.Nombre ?? $"Doctor {d.IdDoctor}", d.IdDoctor.ToString())).ToList();
    }
}

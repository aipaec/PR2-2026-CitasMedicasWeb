using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Data;
using OSCORP.Web.Models;
using OSCORP.Web.Services;
using OSCORP.Web.ViewModels;

namespace OSCORP.Web.Controllers;

[Authorize(Roles = "Paciente")]
public class AppointmentsController(OscorpDbContext db, AppointmentService appointments) : Controller
{
    public async Task<IActionResult> Index()
    {
        var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var list = await db.CitasMedicas
            .Include(c => c.Horario)!.ThenInclude(h => h!.Doctor)!.ThenInclude(d => d!.Usuario)
            .Where(c => c.IdPaciente == patientId)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();
        return View(list);
    }

    public async Task<IActionResult> Book()
    {
        ViewBag.Horarios = await AvailableSchedules();
        return View(new ReservaViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(ReservaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Horarios = await AvailableSchedules();
            return View(model);
        }

        var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await appointments.ReserveAsync(patientId, model.IdHorario, model.MotivoConsulta);
        if (!result.Ok)
        {
            ModelState.AddModelError("", result.Message);
            ViewBag.Horarios = await AvailableSchedules();
            return View(model);
        }

        TempData["Mensaje"] = $"{result.Message} Número de cita: {result.AppointmentId}.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Receipt(int id)
    {
        var patientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var appointment = await db.CitasMedicas
            .Include(c => c.Horario)!.ThenInclude(h => h!.Doctor)!.ThenInclude(d => d!.Usuario)
            .Include(c => c.Horario)!.ThenInclude(h => h!.Doctor)!.ThenInclude(d => d!.Especialidad)
            .FirstOrDefaultAsync(c => c.IdCita == id && c.IdPaciente == patientId);
        if (appointment is null) return NotFound();
        return View(appointment);
    }

    private Task<List<Horario>> AvailableSchedules() =>
        db.Horarios.Include(h => h.Doctor)!.ThenInclude(d => d!.Usuario)
            .Include(h => h.Doctor)!.ThenInclude(d => d!.Especialidad)
            .Where(h => h.Estado == "Disponible" && h.Fecha >= DateTime.Today)
            .OrderBy(h => h.Fecha).ThenBy(h => h.HoraInicio).ToListAsync();
}

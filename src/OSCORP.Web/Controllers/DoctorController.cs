using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Data;

namespace OSCORP.Web.Controllers;

[Authorize(Roles = "Doctor")]
public class DoctorController(OscorpDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var citas = await db.CitasMedicas
            .Include(c => c.Paciente)!.ThenInclude(p => p!.Usuario)
            .Include(c => c.Horario)
            .Where(c => c.Horario!.IdDoctor == doctorId)
            .OrderBy(c => c.Fecha).ThenBy(c => c.Hora)
            .ToListAsync();
        return View(citas);
    }
}

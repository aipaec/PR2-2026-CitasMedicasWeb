using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Data;
using OSCORP.Web.Models;

namespace OSCORP.Web.Services;

public class AppointmentService(OscorpDbContext db)
{
    public async Task<(bool Ok, string Message, int? AppointmentId)> ReserveAsync(
        int patientId, int scheduleId, string reason)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(
            System.Data.IsolationLevel.Serializable);

        var schedule = await db.Horarios
            .FirstOrDefaultAsync(h => h.IdHorario == scheduleId);

        if (schedule is null || schedule.Estado != "Disponible" || schedule.Fecha.Date < DateTime.Today)
            return (false, "Ese horario ya no está disponible. Selecciona otro.", null);

        var alreadyReserved = await db.CitasMedicas.AnyAsync(c => c.IdHorario == scheduleId);
        if (alreadyReserved)
            return (false, "Ese horario ya está ocupado. Selecciona otro.", null);

        schedule.Estado = "Ocupado";
        var appointment = new CitaMedica
        {
            IdPaciente = patientId,
            IdHorario = schedule.IdHorario,
            Fecha = schedule.Fecha.Date,
            Hora = schedule.HoraInicio,
            MotivoConsulta = reason.Trim(),
            Estado = "Pendiente"
        };

        db.CitasMedicas.Add(appointment);
        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return (true, "La cita fue reservada correctamente.", appointment.IdCita);
    }
}

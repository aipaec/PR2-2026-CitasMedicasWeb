namespace OSCORP.Web.Models;

public class CitaMedica
{
    public int IdCita { get; set; }
    public int IdPaciente { get; set; }
    public int IdHorario { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string MotivoConsulta { get; set; } = "";
    public string Estado { get; set; } = "Pendiente";
    public Paciente? Paciente { get; set; }
    public Horario? Horario { get; set; }
}

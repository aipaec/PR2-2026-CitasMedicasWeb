namespace OSCORP.Web.Models;

public class Horario
{
    public int IdHorario { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public string Estado { get; set; } = "Disponible";
    public Doctor? Doctor { get; set; }
}

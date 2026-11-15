using System.ComponentModel.DataAnnotations;

namespace OSCORP.Web.ViewModels;

public class ScheduleViewModel
{
    [Required]
    public int IdDoctor { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime Fecha { get; set; } = DateTime.Today.AddDays(1);

    [Required]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    public TimeSpan HoraFin { get; set; }
}

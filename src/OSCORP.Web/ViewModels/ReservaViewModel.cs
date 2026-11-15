using System.ComponentModel.DataAnnotations;

namespace OSCORP.Web.ViewModels;

public class ReservaViewModel
{
    [Required]
    public int IdHorario { get; set; }

    [Required, StringLength(255)]
    public string MotivoConsulta { get; set; } = "";
}

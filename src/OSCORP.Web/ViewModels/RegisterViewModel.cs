using System.ComponentModel.DataAnnotations;

namespace OSCORP.Web.ViewModels;

public class RegisterViewModel
{
    [Required, StringLength(100)]
    public string Nombre { get; set; } = "";

    [Required, EmailAddress, StringLength(100)]
    public string Correo { get; set; } = "";

    [Required, MinLength(8), DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";

    [Required, Compare(nameof(Contrasena)), DataType(DataType.Password)]
    public string ConfirmarContrasena { get; set; } = "";
}

using System.ComponentModel.DataAnnotations;

namespace OSCORP.Web.ViewModels;

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Correo { get; set; } = "";

    [Required, DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";
}

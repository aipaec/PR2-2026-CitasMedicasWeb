namespace OSCORP.Web.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = "";
    public string Correo { get; set; } = "";
    public string Contrasena { get; set; } = "";
    public string TipoUsuario { get; set; } = "Paciente";
}

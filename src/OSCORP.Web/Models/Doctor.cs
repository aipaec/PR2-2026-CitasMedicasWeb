namespace OSCORP.Web.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public int IdEspecialidad { get; set; }
    public Usuario? Usuario { get; set; }
    public Especialidad? Especialidad { get; set; }
}

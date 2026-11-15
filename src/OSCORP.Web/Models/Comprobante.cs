namespace OSCORP.Web.Models;

public class Comprobante
{
    public int IdComprobante { get; set; }
    public int IdCita { get; set; }
    public DateTime FechaEmision { get; set; }
    public string Tipo { get; set; } = "Recibo";
    public decimal Monto { get; set; }
}

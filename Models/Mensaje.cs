#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using El_Muro.Models;


public class Mensaje
{
    [Key]
    public int MensajeId { get; set; }
    [Required(ErrorMessage = "El Mensaje es obligatorio.")]
    [MinLength(2, ErrorMessage = "El Mensaje debe tener al menos 2 caracteres.")]
    [StringLength(45, ErrorMessage = "El Mensaje debe tener como máximo 45 caracteres.")]
    public string MensajeTexto { get; set; } = null!;
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Modificacion { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public List<Comentario> ListaComentarios {get;set;}

    public Mensaje()
    {
        Fecha_Creacion = DateTime.Now;
        Fecha_Modificacion = DateTime.Now;
        FormatearFechas();
    }
    private void FormatearFechas()
    {
        string formatoChileno = "dd-MM-yyyy HH:mm:ss";
        Fecha_Creacion = DateTime.ParseExact(Fecha_Creacion.ToString(formatoChileno), formatoChileno, CultureInfo.InvariantCulture);
        Fecha_Modificacion = DateTime.ParseExact(Fecha_Modificacion.ToString(formatoChileno), formatoChileno, CultureInfo.InvariantCulture);
    }
}
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

public class Comentario
{
    [Key]
    public int ComentarioId { get; set;}
    [Required(ErrorMessage = "El campo \"Comentario\" es obligatorio.")]
    [StringLength(45, MinimumLength=3, ErrorMessage ="El Comentario debe tener mas de 3 y menos de 45 caracteres.")]
    public string ComentarioTexto {get;set;}
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Modificacion { get; set; }
    public int UsuarioId { get; set; }
    public int MensajeId { get; set; }
    public Usuario Usuario { get; set; }
    public Mensaje Mensaje { get; set; }
    public List<Comentario> ListaComentarios {get;set;}

    public Comentario()
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
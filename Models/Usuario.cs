#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using El_Muro.Models;

public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    [StringLength(45, MinimumLength=3, ErrorMessage ="El Nombre debe tener mas de 3 y menos de 45 caracteres.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El Apellido es obligatorio.")]
    [StringLength(45, MinimumLength=3, ErrorMessage ="El Apellido debe tener mas de 3 y menos de 45 caracteres.")]
    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "El Correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El Correo no es válido.")]
    [StringLength(45, ErrorMessage ="El Correo debe tener menos de 45 caracteres.")]
    public string Correo { get; set; } = null!;
    [Required(ErrorMessage = "La Contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[~`!@#$%^&*()_+{}|:;<=>?/]).{8,}$", ErrorMessage = "La contraseña debe contener al menos 1 número, 1 letra y un carácter especial.")]
    public string Contrasena { get; set; } = null!;
    [NotMapped]
    [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
    public string Rep_Contrasena { get; set; } = null!;
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Modificacion { get; set; }

    public List<Mensaje> ListaMensajes { get; set; } = new List<Mensaje>();

    public Usuario()
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
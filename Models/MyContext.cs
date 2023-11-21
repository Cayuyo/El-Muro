#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace El_Muro.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
}
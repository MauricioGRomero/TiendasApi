using Microsoft.EntityFrameworkCore;
using TiendasAPI.Models; // Asegúrate de que el namespace sea correcto

namespace TiendasAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
    }
}

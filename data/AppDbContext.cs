using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Clasificacion> Clasificaciones { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<EventoPartido> EventosPartido { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre Clasificacion y Grupo
            modelBuilder.Entity<Clasificacion>()
                .HasOne(c => c.Grupo)
                .WithMany(g => g.Clasificaciones)
                .HasForeignKey(c => c.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Grupo y Categoria
            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.Categoria) // Un Grupo tiene una Categoria
                .WithMany(c => c.Grupos) // Una Categoria tiene muchos Grupos
                .HasForeignKey(g => g.CategoriaId) // La clave foránea es CategoriaId
                .OnDelete(DeleteBehavior.Cascade); // Configurar el comportamiento al eliminar (opcional)

            // Relación entre Grupo y sus entidades
            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Equipos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Jugadores)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Clasificaciones)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Partidos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

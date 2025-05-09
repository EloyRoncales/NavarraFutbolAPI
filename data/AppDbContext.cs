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

            modelBuilder.Entity<Clasificacion>()
                .HasOne(c => c.Grupo) // Una Clasificacion tiene un Grupo
                .WithMany(g => g.Clasificaciones) // Un Grupo tiene muchas Clasificaciones
                .HasForeignKey(c => c.GrupoId) // La clave foránea es GrupoId
                .OnDelete(DeleteBehavior.Cascade); // Configurar el comportamiento al eliminar (opcional)
            // Relación entre Categoria y Grupo
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Grupos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

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

            base.OnModelCreating(modelBuilder);
        }
    }
}

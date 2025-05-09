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
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Grupos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Jugador y Grupo
            modelBuilder.Entity<Jugador>()
                .HasOne(j => j.Grupo) // Un Jugador tiene un Grupo
                .WithMany(g => g.Jugadores) // Un Grupo tiene muchos Jugadores
                .HasForeignKey(j => j.GrupoId) // La clave foránea es GrupoId
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
        }
    }
}

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

            // Relación entre Clasificacion y Equipo
            modelBuilder.Entity<Clasificacion>()
                .HasOne(c => c.Equipo)
                .WithMany(e => e.Clasificaciones)
                .HasForeignKey(c => c.EquipoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Grupo y Categoria
            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Grupos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Jugador y Grupo
            modelBuilder.Entity<Jugador>()
                .HasOne(j => j.Grupo)
                .WithMany(g => g.Jugadores)
                .HasForeignKey(j => j.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Jugador y Equipo
            modelBuilder.Entity<Jugador>()
                .HasOne(j => j.Equipo)
                .WithMany(e => e.Jugadores)
                .HasForeignKey(j => j.EquipoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Partido y Grupo
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Grupo)
                .WithMany(g => g.Partidos)
                .HasForeignKey(p => p.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Partido y Equipo (Local)
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Local)
                .WithMany(e => e.PartidosLocal)
                .HasForeignKey(p => p.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Partido y Equipo (Visitante)
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Visitante)
                .WithMany(e => e.PartidosVisitante)
                .HasForeignKey(p => p.VisitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre EventoPartido y Partido
            modelBuilder.Entity<EventoPartido>()
                .HasOne(ep => ep.Partido)
                .WithMany(p => p.EventosPartido) // Relación con la colección en Partido
                .HasForeignKey(ep => ep.PartidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre EventoPartido y Jugador
            modelBuilder.Entity<EventoPartido>()
                .HasOne(ep => ep.Jugador)
                .WithMany(j => j.EventosPartido) // Relación con la colección en Jugador
                .HasForeignKey(ep => ep.JugadorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

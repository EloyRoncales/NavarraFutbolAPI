namespace NavarraFutbolAPI.Models;

public class Partido
{
    public int Id { get; set; }

    // Claves foráneas
    public int GrupoId { get; set; }
    public int LocalId { get; set; }
    public int VisitanteId { get; set; }

    // Propiedades de navegación
    public Grupo Grupo { get; set; } = null!;
    public Equipo Local { get; set; } = null!;
    public Equipo Visitante { get; set; } = null!;

    // Resultado
    public int GolesLocal { get; set; }
    public int GolesVisitante { get; set; }
    public DateTime Fecha { get; set; }

    // Propiedad de navegación para EventoPartido
    public List<EventoPartido> EventosPartido { get; set; } = new();
}


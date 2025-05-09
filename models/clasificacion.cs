namespace NavarraFutbolAPI.Models;

public class Clasificacion
{
    public int Id { get; set; }

    // Claves foráneas
    public int EquipoId { get; set; }
    public int GrupoId { get; set; }

    // Propiedades de navegación
    public Equipo Equipo { get; set; } = null!;
    public Grupo Grupo { get; set; } = null!;

    // Datos
    public int Puntos { get; set; } = 0;
    public int PartidosJugados { get; set; } = 0;
    public int PartidosGanados { get; set; } = 0;
    public int PartidosEmpatados { get; set; } = 0;
    public int PartidosPerdidos { get; set; } = 0;
    public int GolesFavor { get; set; } = 0;
    public int GolesContra { get; set; } = 0;
}
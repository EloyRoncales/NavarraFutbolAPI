namespace NavarraFutbolAPI.Models;

public class Jugador
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Posicion { get; set; } = string.Empty;
    public int EquipoId { get; set; }
    public int GrupoId { get; set; }
    public Equipo Equipo { get; set; } = null!;
    public Grupo Grupo { get; set; } = null!;
    public int Goles { get; set; } = 0;
    public int TarjetasAmarillas { get; set; } = 0;
    public int TarjetasRojas { get; set; } = 0;

    // Propiedad de navegación para EventoPartido
    public List<EventoPartido> EventosPartido { get; set; } = new();
}


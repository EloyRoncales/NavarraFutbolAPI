namespace NavarraFutbolAPI.Models;

public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string EscudoUrl { get; set; } = string.Empty;
    public string Estadio { get; set; } = string.Empty;

    public List<Jugador> Jugadores { get; set; } = new();
    public List<Clasificacion> Clasificaciones { get; set; } = new();
    public List<Partido> PartidosLocal { get; set; } = new();
    public List<Partido> PartidosVisitante { get; set; } = new();
}




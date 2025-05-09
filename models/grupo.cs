namespace NavarraFutbolAPI.Models;

public class Grupo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public List<Equipo> Equipos { get; set; } = new();
    public List<Jugador> Jugadores { get; set; } = new();
    public List<Clasificacion> Clasificaciones { get; set; } = new();
    public List<Partido> Partidos { get; set; } = new();
}

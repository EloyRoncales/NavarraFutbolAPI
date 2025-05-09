namespace NavarraFutbolAPI.Models;

public class EventoPartido
{
    public int Id { get; set; }

    // Claves foráneas
    public int PartidoId { get; set; }
    public int JugadorId { get; set; }

    // Propiedades de navegación
    public Partido Partido { get; set; } = null!;
    public Jugador Jugador { get; set; } = null!;

    // Datos del evento
    public string Tipo { get; set; } = string.Empty;
    public int Minuto { get; set; }
}


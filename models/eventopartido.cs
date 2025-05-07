namespace NavarraFutbolAPI.Models;

public class EventoPartido
{
    public int Id { get; set; }
    public int PartidoId { get; set; }
    public int JugadorId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int Minuto { get; set; }
}

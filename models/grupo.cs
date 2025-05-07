namespace NavarraFutbolAPI.Models;

public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<Equipo> Equipos { get; set; } = new List<Equipo>();
        public List<Jugador> Jugadores { get; set; } = new List<Jugador>();
        public List<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        public List<Partido> Partidos { get; set; } = new List<Partido>();
    }
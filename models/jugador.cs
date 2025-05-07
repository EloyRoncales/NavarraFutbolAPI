namespace NavarraFutbolAPI.Models;

public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Posicion { get; set; } = string.Empty;
        public int EquipoId { get; set; }
        public int Goles { get; set; } = 0;
        public int TarjetasAmarillas { get; set; } = 0;
        public int TarjetasRojas { get; set; } = 0;
    }

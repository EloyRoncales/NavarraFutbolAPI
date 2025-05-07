namespace NavarraFutbolAPI.Models;

public class Partido
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int VisitanteId { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public DateTime Fecha { get; set; }
    }

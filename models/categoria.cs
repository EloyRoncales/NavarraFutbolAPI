namespace NavarraFutbolAPI.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}

namespace NavarraFutbolAPI.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<Grupo> Grupos { get; set; } = new List<Grupo>();
}

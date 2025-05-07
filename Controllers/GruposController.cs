using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;

using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GruposController : ControllerBase
{
    private readonly AppDbContext _context;

    public GruposController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() =>
        Ok(_context.Grupos
            .Include(g => g.Equipos)
            .Include(g => g.Jugadores)
            .Include(g => g.Clasificaciones)
            .Include(g => g.Partidos)
            .ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var grupo = _context.Grupos
            .Include(g => g.Equipos)
            .Include(g => g.Jugadores)
            .Include(g => g.Clasificaciones)
            .Include(g => g.Partidos)
            .FirstOrDefault(g => g.Id == id);

        if (grupo == null) return NotFound();
        return Ok(grupo);
    }

    [HttpPost]
    public IActionResult Post(Grupo grupo)
    {
        _context.Grupos.Add(grupo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = grupo.Id }, grupo);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var grupo = _context.Grupos.Find(id);
        if (grupo == null) return NotFound();

        _context.Grupos.Remove(grupo);
        _context.SaveChanges();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PartidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Partidos.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        Ok(_context.Partidos.Find(id));

    [HttpPost]
    public IActionResult Post(Partido partido)
    {
        _context.Partidos.Add(partido);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = partido.Id }, partido);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var partido = _context.Partidos.Find(id);
        if (partido == null) return NotFound();

        _context.Partidos.Remove(partido);
        _context.SaveChanges();
        return NoContent();
    }
}


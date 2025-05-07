using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosPartidoController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventosPartidoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() =>
        Ok(_context.EventosPartido.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var evento = _context.EventosPartido.Find(id);
        if (evento == null) return NotFound();
        return Ok(evento);
    }

    [HttpPost]
    public IActionResult Post(EventoPartido evento)
    {
        if (!_context.Partidos.Any(p => p.Id == evento.PartidoId))
            return BadRequest($"No existe el partido con ID {evento.PartidoId}");

        if (!_context.Jugadores.Any(j => j.Id == evento.JugadorId))
            return BadRequest($"No existe el jugador con ID {evento.JugadorId}");

        _context.EventosPartido.Add(evento);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = evento.Id }, evento);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var evento = _context.EventosPartido.Find(id);
        if (evento == null) return NotFound();

        _context.EventosPartido.Remove(evento);
        _context.SaveChanges();
        return NoContent();
    }
}

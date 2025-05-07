using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JugadoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public JugadoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Jugadores.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        Ok(_context.Jugadores.Find(id));

    [HttpPost]
    public IActionResult Post(Jugador jugador)
    {
        _context.Jugadores.Add(jugador);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = jugador.Id }, jugador);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var jugador = _context.Jugadores.Find(id);
        if (jugador == null) return NotFound();

        _context.Jugadores.Remove(jugador);
        _context.SaveChanges();
        return NoContent();
    }
}


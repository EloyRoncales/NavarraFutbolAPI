using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;

using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquiposController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Equipos.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        Ok(_context.Equipos.Find(id));

    [HttpPost]
    public IActionResult Post(Equipo equipo)
    {
        _context.Equipos.Add(equipo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = equipo.Id }, equipo);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var equipo = _context.Equipos.Find(id);
        if (equipo == null) return NotFound();

        _context.Equipos.Remove(equipo);
        _context.SaveChanges();
        return NoContent();
    }
}


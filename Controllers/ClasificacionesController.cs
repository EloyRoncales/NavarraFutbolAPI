using Microsoft.AspNetCore.Mvc;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClasificacionesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClasificacionesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Clasificaciones.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        Ok(_context.Clasificaciones.Find(id));

    [HttpPost]
    public IActionResult Post(Clasificacion clasificacion)
    {
        _context.Clasificaciones.Add(clasificacion);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = clasificacion.Id }, clasificacion);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var clasificacion = _context.Clasificaciones.Find(id);
        if (clasificacion == null) return NotFound();

        _context.Clasificaciones.Remove(clasificacion);
        _context.SaveChanges();
        return NoContent();
    }
}


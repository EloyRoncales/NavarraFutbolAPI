using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var categorias = _context.Categorias
            .Include(c => c.Grupos)
                .ThenInclude(g => g.Equipos)
            .Include(c => c.Grupos)
                .ThenInclude(g => g.Jugadores)
            .Include(c => c.Grupos)
                .ThenInclude(g => g.Clasificaciones)
            .Include(c => c.Grupos)
                .ThenInclude(g => g.Partidos)
            .ToList();

        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var categoria = _context.Categorias
            .Include(c => c.Grupos)
                .ThenInclude(g => g.Equipos)
            .FirstOrDefault(c => c.Id == id);

        if (categoria == null) return NotFound();

        return Ok(categoria);
    }

    [HttpPost]
    public IActionResult Post(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = categoria.Id }, categoria);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);
        if (categoria == null) return NotFound();

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }
}

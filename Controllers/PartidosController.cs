using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Partidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidos()
        {
            return await _context.Partidos.ToListAsync();
        }

        // GET: api/Partidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partido>> GetPartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);

            if (partido == null)
            {
                return NotFound();
            }

            return partido;
        }

        // POST: api/Partidos
        [HttpPost]
        public async Task<ActionResult<Partido>> PostPartido(Partido partido)
        {
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartido), new { id = partido.Id }, partido);
        }

        // PUT: api/Partidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartido(int id, Partido partido)
        {
            if (id != partido.Id)
            {
                return BadRequest();
            }

            _context.Entry(partido).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Partidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Obtener los partidos de una categoría
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidosByCategoria(int categoriaId)
        {
            var categoria = await _context.Categorias
                                          .Include(c => c.Grupos)
                                          .ThenInclude(g => g.Partidos)
                                          .FirstOrDefaultAsync(c => c.Id == categoriaId);

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            var partidos = categoria.Grupos.SelectMany(g => g.Partidos).ToList();

            if (!partidos.Any())
            {
                return NotFound(new { message = "No se encontraron partidos para esta categoría." });
            }

            return Ok(partidos);
        }
    }
}

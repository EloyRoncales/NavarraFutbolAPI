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

        [HttpGet("categoria/{categoriaId}/partidos")]
        public async Task<ActionResult> GetPartidosByCategoria(int categoriaId)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Grupos)
                    .ThenInclude(g => g.Partidos)
                        .ThenInclude(p => p.Local)
                .Include(c => c.Grupos)
                    .ThenInclude(g => g.Partidos)
                        .ThenInclude(p => p.Visitante)
                .FirstOrDefaultAsync(c => c.Id == categoriaId);

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            var response = new
            {
                Categoria = categoria.Nombre ?? $"Categoría {categoria.Id}",
                Grupos = categoria.Grupos.Select(grupo => new
                {
                    Grupo = grupo.Nombre ?? $"Grupo {grupo.Id}",
                    Partidos = grupo.Partidos.Select(partido => new
                    {
                        partido.Id,
                        partido.Fecha,
                        EquipoLocal = partido.Local?.Nombre ?? "Desconocido",
                        EquipoVisitante = partido.Visitante?.Nombre ?? "Desconocido",
                        GolesLocal = partido.GolesLocal,
                        GolesVisitante = partido.GolesVisitante
                    })
                })
            };

            return Ok(response);
        }

    }
}



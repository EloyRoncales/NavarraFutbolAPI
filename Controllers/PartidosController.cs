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
                .Where(c => c.Id == categoriaId) // Usamos Where para mayor claridad
                .Select(c => new // Proyectamos directamente a un objeto anónimo
                {
                    Categoria = c.Nombre ?? $"Categoría {c.Id}",
                    Grupos = c.Grupos.Select(g => new // Proyectamos los Grupos
                    {
                        Grupo = g.Nombre ?? $"Grupo {g.Id}",
                        Partidos = g.Partidos.Select(p => new // Proyectamos los Partidos
                        {
                            p.Id,
                            p.Fecha,
                            EquipoLocal = p.Local != null ? p.Local.Nombre : "Desconocido",
                            EquipoVisitante = p.Visitante != null ? p.Visitante.Nombre : "Desconocido",
                            GolesLocal = p.GolesLocal,
                            GolesVisitante = p.GolesVisitante
                        }).ToList() // Aseguramos que Partidos sea una lista serializable
                    }).ToList() // Aseguramos que Grupos sea una lista serializable
                })
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            return Ok(categoria); // Devolvemos directamente el objeto anónimo creado
        }

    }
}



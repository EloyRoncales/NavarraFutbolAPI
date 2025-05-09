using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Obtener los grupos de una categoría (Nuevo método)
        [HttpGet("{id}/grupos")]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetGruposByCategoria(int id)
        {
            var categoria = await _context.Categorias
                                        .Include(c => c.Grupos)  // Incluir los grupos relacionados
                                        .FirstOrDefaultAsync(c => c.Id == id);
            
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            if (categoria.Grupos == null || !categoria.Grupos.Any())
            {
                return NotFound(new { message = "No se encontraron grupos para esta categoría." });
            }

            return Ok(categoria.Grupos);  // Retorna los grupos en formato correcto
        }


        // Obtener los partidos de un grupo (Nuevo método)
        [HttpGet("grupos/{id}/partidos")]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidosByGrupo(int id)
        {
            var grupo = await _context.Grupos.Include(g => g.Partidos)
                                              .FirstOrDefaultAsync(g => g.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return grupo.Partidos;
        }
    }
}

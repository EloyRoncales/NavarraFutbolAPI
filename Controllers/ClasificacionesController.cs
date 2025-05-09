using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionController : ControllerBase
    {
        private readonly DbContext _context;

        public ClasificacionController(DbContext context)
        {
            _context = context;
        }

        // GET: api/Clasificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clasificacion>>> GetClasificaciones()
        {
            return await _context.Set<Clasificacion>().ToListAsync();
        }

        // GET: api/Clasificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clasificacion>> GetClasificacion(int id)
        {
            var clasificacion = await _context.Set<Clasificacion>().FindAsync(id);

            if (clasificacion == null)
            {
                return NotFound();
            }

            return clasificacion;
        }

        // POST: api/Clasificacion
        [HttpPost]
        public async Task<ActionResult<Clasificacion>> PostClasificacion(Clasificacion clasificacion)
        {
            _context.Set<Clasificacion>().Add(clasificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasificacion), new { id = clasificacion.Id }, clasificacion);
        }

        // PUT: api/Clasificacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasificacion(int id, Clasificacion clasificacion)
        {
            if (id != clasificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(clasificacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clasificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasificacion(int id)
        {
            var clasificacion = await _context.Set<Clasificacion>().FindAsync(id);
            if (clasificacion == null)
            {
                return NotFound();
            }

            _context.Set<Clasificacion>().Remove(clasificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

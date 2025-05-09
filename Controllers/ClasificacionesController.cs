using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClasificacionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clasificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clasificacion>>> GetClasificaciones()
        {
            return await _context.Clasificaciones.ToListAsync();
        }

        // GET: api/Clasificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clasificacion>> GetClasificacion(int id)
        {
            var clasificacion = await _context.Clasificaciones.FindAsync(id);

            if (clasificacion == null)
            {
                return NotFound();
            }

            return clasificacion;
        }

        // POST: api/Clasificaciones
        [HttpPost]
        public async Task<ActionResult<Clasificacion>> PostClasificacion(Clasificacion clasificacion)
        {
            _context.Clasificaciones.Add(clasificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasificacion), new { id = clasificacion.Id }, clasificacion);
        }

        // PUT: api/Clasificaciones/5
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

        // DELETE: api/Clasificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasificacion(int id)
        {
            var clasificacion = await _context.Clasificaciones.FindAsync(id);
            if (clasificacion == null)
            {
                return NotFound();
            }

            _context.Clasificaciones.Remove(clasificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

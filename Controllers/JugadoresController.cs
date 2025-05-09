using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly DbContext _context;

        public JugadorController(DbContext context)
        {
            _context = context;
        }

        // GET: api/Jugador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetJugadores()
        {
            return await _context.Set<Jugador>().ToListAsync();
        }

        // GET: api/Jugador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugador>> GetJugador(int id)
        {
            var jugador = await _context.Set<Jugador>().FindAsync(id);

            if (jugador == null)
            {
                return NotFound();
            }

            return jugador;
        }

        // POST: api/Jugador
        [HttpPost]
        public async Task<ActionResult<Jugador>> PostJugador(Jugador jugador)
        {
            _context.Set<Jugador>().Add(jugador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugador), new { id = jugador.Id }, jugador);
        }

        // PUT: api/Jugador/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJugador(int id, Jugador jugador)
        {
            if (id != jugador.Id)
            {
                return BadRequest();
            }

            _context.Entry(jugador).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Jugador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJugador(int id)
        {
            var jugador = await _context.Set<Jugador>().FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }

            _context.Set<Jugador>().Remove(jugador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

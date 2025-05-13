using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JugadoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Jugadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetJugadores()
        {
            return await _context.Jugadores.ToListAsync();
        }

        // GET: api/Jugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugador>> GetJugador(int id)
        {
            var jugador = await _context.Jugadores.FindAsync(id);

            if (jugador == null)
            {
                return NotFound();
            }

            return jugador;
        }

        // POST: api/Jugadores
        [HttpPost]
        public async Task<ActionResult<Jugador>> PostJugador(Jugador jugador)
        {
            _context.Jugadores.Add(jugador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugador), new { id = jugador.Id }, jugador);
        }

        // PUT: api/Jugadores/5
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

        // DELETE: api/Jugadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJugador(int id)
        {
            var jugador = await _context.Jugadores.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }

            _context.Jugadores.Remove(jugador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Jugadores/equipo/5
        [HttpGet("equipo/{equipoId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetJugadoresPorEquipo(int equipoId)
        {
            var jugadores = await _context.Jugadores
                .Where(j => j.EquipoId == equipoId)
                .Select(j => new
                {
                    j.Id,
                    j.Nombre,
                    j.Posicion,
                    j.Edad,
                    j.Goles
                })
                .ToListAsync();

            if (jugadores == null || jugadores.Count == 0)
            {
                return NotFound();
            }

            return Ok(jugadores);
        }
    }
}

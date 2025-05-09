using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoPartidoController : ControllerBase
    {
        private readonly DbContext _context;

        public EventoPartidoController(DbContext context)
        {
            _context = context;
        }

        // GET: api/EventoPartido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoPartido>>> GetEventoPartidos()
        {
            return await _context.Set<EventoPartido>().ToListAsync();
        }

        // GET: api/EventoPartido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoPartido>> GetEventoPartido(int id)
        {
            var eventoPartido = await _context.Set<EventoPartido>().FindAsync(id);

            if (eventoPartido == null)
            {
                return NotFound();
            }

            return eventoPartido;
        }

        // POST: api/EventoPartido
        [HttpPost]
        public async Task<ActionResult<EventoPartido>> PostEventoPartido(EventoPartido eventoPartido)
        {
            _context.Set<EventoPartido>().Add(eventoPartido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventoPartido), new { id = eventoPartido.Id }, eventoPartido);
        }

        // PUT: api/EventoPartido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoPartido(int id, EventoPartido eventoPartido)
        {
            if (id != eventoPartido.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventoPartido).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/EventoPartido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoPartido(int id)
        {
            var eventoPartido = await _context.Set<EventoPartido>().FindAsync(id);
            if (eventoPartido == null)
            {
                return NotFound();
            }

            _context.Set<EventoPartido>().Remove(eventoPartido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

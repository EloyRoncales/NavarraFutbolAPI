using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;

namespace NavarraFutbolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosPartidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventosPartidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventosPartido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoPartido>>> GetEventosPartido()
        {
            return await _context.EventosPartido.ToListAsync();
        }

        // GET: api/EventosPartido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoPartido>> GetEventoPartido(int id)
        {
            var eventoPartido = await _context.EventosPartido.FindAsync(id);

            if (eventoPartido == null)
            {
                return NotFound();
            }

            return eventoPartido;
        }

        // POST: api/EventosPartido
        [HttpPost]
        public async Task<ActionResult<EventoPartido>> PostEventoPartido(EventoPartido eventoPartido)
        {
            _context.EventosPartido.Add(eventoPartido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventoPartido), new { id = eventoPartido.Id }, eventoPartido);
        }

        // PUT: api/EventosPartido/5
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

        // DELETE: api/EventosPartido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoPartido(int id)
        {
            var eventoPartido = await _context.EventosPartido.FindAsync(id);
            if (eventoPartido == null)
            {
                return NotFound();
            }

            _context.EventosPartido.Remove(eventoPartido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

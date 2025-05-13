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
            var clasificaciones = await _context.Clasificaciones
                .Include(c => c.Equipo)   // 👈 Esto asegura el equipo
                .Include(c => c.Grupo)
                .ToListAsync();

            // Esto forzará la serialización evitando ciclos
            var resultado = clasificaciones.Select(c => new {
                c.Id,
                c.EquipoId,
                c.GrupoId,
                c.Puntos,
                c.PartidosJugados,
                c.PartidosGanados,
                c.PartidosEmpatados,
                c.PartidosPerdidos,
                c.GolesFavor,
                c.GolesContra,
                Equipo = c.Equipo != null ? new {
                    c.Equipo.Id,
                    c.Equipo.Nombre,
                    c.Equipo.EscudoUrl,
                    c.Equipo.Estadio
                } : null
            });

            return Ok(resultado);
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


        [HttpGet("{categoriaId}/clasificacion")]
        public async Task<IActionResult> GetClasificacionPorCategoria(int categoriaId)
        {
            var grupos = await _context.Grupos
                .Where(g => g.CategoriaId == categoriaId)
                .Include(g => g.Clasificaciones)
                    .ThenInclude(c => c.Equipo)
                .ToListAsync();

            if (!grupos.Any())
                return NotFound("No hay grupos para esta categoría.");

            var resultado = grupos.Select(g => new
            {
                id = g.Id,
                grupo = g.Nombre,
                clasificaciones = g.Clasificaciones
                    .OrderByDescending(c => c.Puntos)
                    .Select(c => new
                    {
                        id = c.Id,
                        equipoId = c.EquipoId,
                        grupoId = c.GrupoId,
                        puntos = c.Puntos,
                        partidosJugados = c.PartidosJugados,
                        partidosGanados = c.PartidosGanados,
                        partidosEmpatados = c.PartidosEmpatados,
                        partidosPerdidos = c.PartidosPerdidos,
                        golesFavor = c.GolesFavor,
                        golesContra = c.GolesContra,
                        equipo = new
                        {
                            id = c.Equipo.Id,
                            nombre = c.Equipo.Nombre,
                            escudoUrl = c.Equipo.EscudoUrl,
                            estadio = c.Equipo.Estadio
                        }
                    })
            });

            return Ok(resultado);
        }

    }
}

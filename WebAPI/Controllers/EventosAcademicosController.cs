using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.WebAPI.DataAccessLayer;
using Eventos.WebAPI.Models;

namespace Eventos.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosAcademicosController : ControllerBase
    {
        private readonly EventosAcademicosDbContext _context;

        public EventosAcademicosController(EventosAcademicosDbContext context)
        {
            _context = context;
        }

        // GET: api/EventosAcademicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoAcademico>>> GetEventosAcademicos()
        {
            return await _context.EventosAcademicos.ToListAsync();
        }

        //GET: api/InscricoesEventosAcademicos
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<InscricaoEventoAcademico>>> GetInscricoesEventosAcademicos()
        {
            return await _context.InscricoesEventosAcademicos.ToListAsync();
        }


        // GET: api/EventosAcademicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoAcademico>> GetEventoAcademico(int id)
        {
            var eventoAcademico = await _context.EventosAcademicos.FindAsync(id);

            if (eventoAcademico == null)
            {
                return NotFound();
            }

            return eventoAcademico;
        }

        // PUT: api/EventosAcademicos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoAcademico(int id, EventoAcademico eventoAcademico)
        {
            if (id != eventoAcademico.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventoAcademico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoAcademicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EventosAcademicos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EventoAcademico>> PostEventoAcademico(EventoAcademico eventoAcademico)
        {
            _context.EventosAcademicos.Add(eventoAcademico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventoAcademico", new { id = eventoAcademico.Id }, eventoAcademico);
        }

        // DELETE: api/EventosAcademicos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventoAcademico>> DeleteEventoAcademico(int id)
        {
            var eventoAcademico = await _context.EventosAcademicos.FindAsync(id);
            if (eventoAcademico == null)
            {
                return NotFound();
            }

            _context.EventosAcademicos.Remove(eventoAcademico);
            await _context.SaveChangesAsync();

            return eventoAcademico;
        }

        private bool EventoAcademicoExists(int id)
        {
            return _context.EventosAcademicos.Any(e => e.Id == id);
        }
    }
}

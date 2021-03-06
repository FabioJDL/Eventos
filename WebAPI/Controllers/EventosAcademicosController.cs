﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.WebAPI.DataAccessLayer;
using Eventos.WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        //Get: api/EventosAcademicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoAcademico>>> GetEventosAcademicos()
        {                                                  
            return await _context.EventosAcademicos.ToListAsync();
        }


        //GET: api/EventosAcademicos/GetEventosAcademicosByTitulo
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<EventoAcademico>>> GetEventosAcademicosByTitulo(string titulo)
        {
            var eventos = await _context.EventosAcademicos.Where(evento => evento.Titulo == titulo).ToListAsync();

            if (eventos.Count == 0)
            {
                return NotFound();
            }

            return eventos;
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


        //GET: api/EventosAcademicos/InscricoesEventosAcademicos/GetInscricoesEventosAcademicos
        [HttpGet]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<ActionResult<IEnumerable<InscricaoEventoAcademico>>> GetInscricoesEventosAcademicos()
        {
            return await _context.InscricoesEventosAcademicos.ToListAsync();
        }


        // GET: api/EventosAcademicos/InscricoesEventosAcademicos/GetInscricoesEventosAcademicosByUsuarioCpf
        [HttpGet]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<ActionResult<IEnumerable<InscricaoEventoAcademico>>> GetInscricoesEventosAcademicosByUsuarioCpf(string cpf)
        {
            var inscricoes = await _context.InscricoesEventosAcademicos.Where(i => i.UsuarioCPF == cpf).ToListAsync();

            if (inscricoes.Count() == 0)
            {
                return NotFound();
            }

            return inscricoes;
        }


        // GET: api/EventosAcademicos/InscricoesEventosAcademicos/GetInscricoesEventosAcademicosByEventoId
        [HttpGet]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<ActionResult<IEnumerable<InscricaoEventoAcademico>>> GetInscricoesEventosAcademicosByEventoId(int id)
        {
            var inscricoes = await _context.InscricoesEventosAcademicos.Where(i => i.EventoAcademicoId == id).ToListAsync();

            if (inscricoes.Count() == 0)
            {
                return NotFound();
            }

            return inscricoes;

        }

        // Get: api/EventosAcademicos/GerarCertificado
        [HttpGet]
        [Route("[action]")]
        public string GerarCertificado(int eventoId, string usuarioCpf)
        {
            // PROGRAMAÇÃO ORIENTADA A GAMBIARRA!!!
            if (_context.InscricoesEventosAcademicos.Where(i => i.EventoAcademicoId == eventoId).Where(i => i.UsuarioCPF == usuarioCpf).Where(i => i.ListaDePresenca == true).Count() != 0)
                return "imprimindo certificado...";
            
            return "Certificado não encontrado!!!";
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


        // POST: api/EventosAcademicos/InscricoesEventosAcademicos/PostInscricaoEventoAcademico
        [HttpPost]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<ActionResult<InscricaoEventoAcademico>> PostInscricaoEventoAcademico(InscricaoEventoAcademico inscricao)
        {
            //Verifica se tem vaga para se inscrever no evento, relacionando o número de inscritos no evento com sua Capacidade
            if (_context.InscricoesEventosAcademicos.Where(i => i.EventoAcademicoId == inscricao.EventoAcademicoId).Count()
               < _context.EventosAcademicos.Find(inscricao.EventoAcademicoId).Capacidade)

            {
                _context.InscricoesEventosAcademicos.Add(inscricao);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetInscricoesEventosAcademicos", new { id = inscricao.Id }, inscricao);
            }

            return BadRequest();
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


        //PUT: api/EventosAcademicos/InscricoesEventosAcademicos/PutInscricaoEventoAcademico
        [HttpPut]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<IActionResult> PutInscricoesEventoAcademico(int id, InscricaoEventoAcademico inscricao)
        {
            if (id != inscricao.Id)
            {
                return BadRequest();
            }

            _context.Entry(inscricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoEventoAcademicoExists(id))
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


        // DELETE: api/EventosAcademicos/InscricoesEventosAcademicos/DeleteInscricaoEventoAcademico
        [HttpDelete]
        [Route("InscricoesEventosAcademicos/[action]")]
        public async Task<ActionResult<InscricaoEventoAcademico>> DeleteInscricaoEventoAcademico(int id)
        {
            var inscricaoEventoAcademico = await _context.InscricoesEventosAcademicos.FindAsync(id);
            if (inscricaoEventoAcademico == null)
            {
                return NotFound();
            }

            _context.InscricoesEventosAcademicos.Remove(inscricaoEventoAcademico);
            await _context.SaveChangesAsync();

            return inscricaoEventoAcademico;
        }


        private bool EventoAcademicoExists(int id)
        {
            return _context.EventosAcademicos.Any(e => e.Id == id);
        }


        private bool InscricaoEventoAcademicoExists(int id)
        {
            return _context.InscricoesEventosAcademicos.Any(i => i.Id == id);
        }


    }
}

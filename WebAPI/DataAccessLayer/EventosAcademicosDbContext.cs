using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventos.WebAPI.Models;


namespace Eventos.WebAPI.DataAccessLayer
{
    public class EventosAcademicosDbContext : DbContext
    {
        public DbSet<EventoAcademico> EventosAcademicos { get; set; }

        public DbSet<InscricaoEventoAcademico> InscricoesEventosAcademicos { get; set; }


        public EventosAcademicosDbContext(DbContextOptions<EventosAcademicosDbContext> options) : base(options)
        {
        }
    }
}

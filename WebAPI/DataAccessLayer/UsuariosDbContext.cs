using Eventos.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventos.WebAPI.DataAccessLayer
{
    public class UsuariosDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }


        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
        {

        }
    }
}

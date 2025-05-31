using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CitasContext : DbContext
    {
        public CitasContext(DbContextOptions<CitasContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<AgendaCitas> AgendaCitas { get; set; }

        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
    }
}

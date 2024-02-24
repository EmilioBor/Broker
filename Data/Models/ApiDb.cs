using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApiDb : DbContext
    {
        public ApiDb(DbContextOptions<ApiDb> options) : base(options)
        {

        }

        public DbSet<Aceptadoestado> Aceptadoestado => Set<Aceptadoestado>();
        public DbSet<Cuenta> Cuenta => Set<Cuenta>();
        public DbSet<Transaccion> Transaccion => Set<Transaccion>();
        public DbSet<Bancoestado> Bancoestado => Set<Bancoestado>();
        public DbSet<Registroestado> Registroestado => Set<Registroestado>();
        public DbSet<Tipo> Tipo => Set<Tipo>();
        public DbSet<Banco> Banco => Set<Banco>();
        public DbSet<Validacionestado> Validacionestado => Set<Validacionestado>();



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LojaImpacta.Models;

namespace LojaImpacta.Data
{
    public class LojaImpactaContext : DbContext
    {
        public LojaImpactaContext (DbContextOptions<LojaImpactaContext> options)
            : base(options)
        {
        }

        public DbSet<LojaImpacta.Models.User> User { get; set; } = default!;
        public DbSet<LojaImpacta.Models.Product> Product { get; set; } = default!;
        public DbSet<LojaImpacta.Models.AccessLevel> AccessLevel { get; set; } = default!;
        public DbSet<LojaImpacta.Models.Sale> Sale { get; set; } = default!;
    }
}

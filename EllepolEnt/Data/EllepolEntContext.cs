using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EllepolEnt.Models;

namespace EllepolEnt.Data
{
    public class EllepolEntContext : DbContext
    {
        public EllepolEntContext (DbContextOptions<EllepolEntContext> options)
            : base(options)
        {
        }

        public DbSet<EllepolEnt.Models.Login> Login { get; set; } = default!;

        public DbSet<EllepolEnt.Models.UserDetails> UserDetails { get; set; }

        public DbSet<EllepolEnt.Models.Role> Role { get; set; }

        public DbSet<EllepolEnt.Models.GRN> GRN { get; set; }

        public DbSet<EllepolEnt.Models.ItemReg> ItemReg { get; set; }

        public DbSet<EllepolEnt.Models.Stock> Stock { get; set; }

        public DbSet<EllepolEnt.Models.Invoice> Invoice { get; set; }

        public DbSet<EllepolEnt.Models.Pumpregistration> Pumpregistration { get; set; }

        public DbSet<EllepolEnt.Models.PumpManagement> PumpManagement { get; set; }

        public DbSet<EllepolEnt.Models.Stock_Out> Stock_Out { get; set; }

    }
}

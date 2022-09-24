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

        public DbSet<EllepolEnt.Models.userdetail> userdetail { get; set; }
    }
}

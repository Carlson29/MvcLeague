using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcLeague.Models;

namespace MvcLeague.Data
{
    public class MvcLeagueContext : DbContext
    {
        public MvcLeagueContext (DbContextOptions<MvcLeagueContext> options)
            : base(options)
        {
        }

        public DbSet<MvcLeague.Models.Player> Player { get; set; } = default!;
    }
}

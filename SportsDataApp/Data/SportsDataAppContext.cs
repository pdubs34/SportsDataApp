using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsDataApp.Models;

namespace SportsDataApp.Data
{
    public class SportsDataAppContext : DbContext
    {
        public SportsDataAppContext (DbContextOptions<SportsDataAppContext> options)
            : base(options)
        {
        }
        public DbSet<SportsDataApp.Models.BaseballGame>? BaseballGame { get; set; }
        public DbSet<SportsDataApp.Models.Season> Season { get; set; } = default!;

        public DbSet<SportsDataApp.Models.Team>? Team { get; set; }

        public DbSet<SportsDataApp.Models.Sport>? Sport { get; set; }

        public DbSet<SportsDataApp.Models.User>? User { get; set; }
    }
}

using BoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Data
{
    public class BGContext : DbContext
    {
        public BGContext (DbContextOptions<BGContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }

        public DbSet<Publisher> Publisher { get; set; }
    }
}

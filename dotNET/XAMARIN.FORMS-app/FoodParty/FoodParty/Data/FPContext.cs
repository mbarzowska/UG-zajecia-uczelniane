using System;
using System.Collections.Generic;
using System.Text;
using FoodParty.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodParty.Data 
{
    public class FPContext : DbContext 
    {
        public DbSet<Pizzeria> Pizzerias { get; set; }

        public DbSet<PizzaSize> Sizes { get; set; }

        private readonly string _databasePath;

        public FPContext(string databasePath) 
        {
            _databasePath = databasePath;
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite($"Filename = {_databasePath}");
        }
    }
}

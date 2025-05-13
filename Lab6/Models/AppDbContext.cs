using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Models
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RestaurantGuideDb;Trusted_Connection=True;");
        }
    }
}

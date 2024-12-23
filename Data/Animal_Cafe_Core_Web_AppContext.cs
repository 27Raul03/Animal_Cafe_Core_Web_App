using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Models;

namespace Animal_Cafe_Core_Web_App.Data
{
    public class Animal_Cafe_Core_Web_AppContext : DbContext
    {
        public Animal_Cafe_Core_Web_AppContext (DbContextOptions<Animal_Cafe_Core_Web_AppContext> options)
            : base(options)
        {
        }

        public DbSet<Animal_Cafe_Core_Web_App.Models.Animal> Animal { get; set; } = default!;
        public DbSet<Animal_Cafe_Core_Web_App.Models.Client> Client { get; set; } = default!;
        public DbSet<Animal_Cafe_Core_Web_App.Models.Product> Product { get; set; } = default!;
        public DbSet<Animal_Cafe_Core_Web_App.Models.Reservation> Reservation { get; set; } = default!;
        public DbSet<Animal_Cafe_Core_Web_App.Models.Review> Review { get; set; } = default!;
    }
}

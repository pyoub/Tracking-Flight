using Microsoft.EntityFrameworkCore;
using vol.Models.Entity;

namespace vol.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { 
            
        }

        public DbSet<Flight> FlightDbSet { get; set; }

        public DbSet<Plane> PlaneDbSet { get; set; }
        
        public DbSet<Aeroport> AeroportDbSet { get; set; }
    }
}
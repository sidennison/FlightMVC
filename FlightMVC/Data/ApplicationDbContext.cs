using FlightMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FlightMVC.Areas.FlightsInfo.Models;

namespace FlightMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flight> Flights { get; set; }

        public DbSet<FlightMVC.Areas.FlightsInfo.Models.Plane> Plane { get; set; }
    }
}
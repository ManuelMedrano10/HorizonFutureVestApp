using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Contexts
{
    public class HorizonDbContext : DbContext
    {
        public HorizonDbContext(DbContextOptions<HorizonDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Macroindicator> Macroindicators { get; set; }
        public DbSet<ReturnRate> ReturnRates { get; set; }
        public DbSet<IndicatorByCountry> IndicatorByCountries{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

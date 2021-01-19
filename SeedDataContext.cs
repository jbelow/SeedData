using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace SeedData
{
    public class SeedDataContext : DbContext
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<SeedEvent> SeedEvent { get; set; }

        public void AddLocation(Location location)
        {
            this.Location.Add(location);
            this.SaveChanges();
        }

        public void AddSeedEvent(SeedEvent seedEvent)
        {
            this.SeedEvent.Add(seedEvent);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["SeedDataContext:ConnectionString"]);
        }
    }
}
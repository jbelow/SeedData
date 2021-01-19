using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace SeedData
{
    public class SeedDataContext : DbContext
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<SeedEvent> SeedEvents { get; set; }

        public void AddLocation(Location location)
        {
            this.Location.Add(location);
            this.SaveChanges();
        }

        public void AddSeedEvent(SeedEvent seedEvent)
        {
            this.SeedEvent.Add(seedevent);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // IConfiguration config = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json", true, true)
            //     .Build();
            optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu;Database=SeededData_21_JEB;User ID=jbelow3;Password=000538646");
        }
    }
}
using Fhi.Kompetanse.Telemetri.Domene;
using Microsoft.EntityFrameworkCore;

namespace Fhi.Kompetanse.Telemetri.Backend.PersonWebApi.Persistence.Context
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Address { get; set; }

        public PersonContext(DbContextOptions<PersonContext> options, ILogger<PersonContext> logger) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            modelBuilder.Entity<Address>().HasKey(p => p.Id);
        }
    }
}

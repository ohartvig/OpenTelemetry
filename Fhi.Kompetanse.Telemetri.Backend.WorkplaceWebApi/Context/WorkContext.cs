using Fhi.Kompetanse.Telemetri.Domene;
using Microsoft.EntityFrameworkCore;

namespace Fhi.Kompetanse.Telemetri.Backend.WorkplaceWebApi.Context
{
    public class WorkContext : DbContext
    {
        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {
        }

        public DbSet<Workplace> Workplaces { get; set; }
    }
}

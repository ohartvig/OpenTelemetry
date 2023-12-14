using Bogus;
using Fhi.Kompetanse.Telemetri.Backend.WorkplaceWebApi.Context;
using Fhi.Kompetanse.Telemetri.Domene;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fhi.Kompetanse.Telemetri.Backend.WorkplaceWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkplaceController : ControllerBase
    {
        private readonly WorkContext context;

        public WorkplaceController(WorkContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "GetAllWorkplaces")]
        public async Task<ActionResult<IList<Workplace>>> GetAllWorkplaces()
        {
            var workplaceList = await context.Workplaces.ToListAsync();
            return Ok(workplaceList);
        }

        [HttpGet("{id}", Name = "GetAllWorkplacesForPerson")]
        public async Task<ActionResult<IList<Workplace>>> GetAllWorkplacesForPerson(int id)
        {
            var workplacesList = await context.Workplaces.Where(w => w.PersonId == id).ToListAsync();
            return Ok(workplacesList);
        }

        [HttpPost(Name = "CreateWorkplace")]
        public async Task<ActionResult<Workplace>> CreateWorkplace(int personId)
        {
            var faker = new Faker();

            var workplace = new Workplace()
            {
                CompanyName = faker.Company.CompanyName(),
                Street = faker.Address.StreetAddress(),
                City = faker.Address.City(),
                PersonId = personId
            };

            await context.Workplaces.AddAsync(workplace);
            await context.SaveChangesAsync();

            return Ok(workplace);
        }
    }
}

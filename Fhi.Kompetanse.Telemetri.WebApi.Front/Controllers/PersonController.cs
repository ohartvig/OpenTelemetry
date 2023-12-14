using Bogus;
using Person = Fhi.Kompetanse.Telemetri.Domene.Person;
using Workplace = Fhi.Kompetanse.Telemetri.Domene.Workplace;
using Microsoft.AspNetCore.Mvc;
using Fhi.Kompetanse.Telemetri.Contracts;

namespace Fhi.Kompetanse.Telemetri.WebApi.Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonWebApi api;
        private readonly IWorkplaceWebApi workApi;

        public PersonController(ILogger<PersonController> logger, IPersonWebApi api, IWorkplaceWebApi workApi)
        {
            _logger = logger;
            this.api = api;
            this.workApi = workApi;
        }

        [HttpGet(Name = "GetAllPersons")]
        public async Task <IList<Person>> Get()
        {
            var personList = await api.GetAllPersons();

            return personList;  
        }

        [HttpPost(Name = "CreatePerson")]
        public async Task<Person> Post(string? FirstName=null,string? LastName=null)
        {
            var faker = new Faker();

            if (FirstName == null)
                FirstName = faker.Name.FirstName();
            if (LastName == null)
                LastName = faker.Name.LastName();

            var person=  await api.CreatePerson(FirstName, LastName);

            return person;
        }

        [HttpGet("Workplaces", Name = "GetAllWorkplaces")]
        public async Task<IList<Workplace>> GetAllWorkplaces()
        {
            var workplaceList = await workApi.GetAllWorkplaces();

            return workplaceList;
        }
    }
}

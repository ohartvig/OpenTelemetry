using Person = Fhi.Kompetanse.Telemetri.Domene.Person;
using Microsoft.AspNetCore.Mvc;
using Fhi.Kompetanse.Telemetri.Contracts;
using Fhi.Kompetanse.Telemetri.Backend.PersonWebApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fhi.Kompetanse.Telemetri.Backend.PersonWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IAddressWebApi iAddressWebApi;
        private readonly PersonContext personContext;

        public PersonController(ILogger<PersonController> logger, IAddressWebApi IAddressWebApi, PersonContext personContext)
        {
            _logger = logger;
            iAddressWebApi = IAddressWebApi;
            this.personContext = personContext;
        }

        [HttpGet(Name = "GetAllPersons")]
        public async Task<IList<Person>> Get()
        {
            var personList = new List<Person>() { };
            return await personContext.Persons.Include(e => e.Address).ToListAsync();
        }

        [HttpPost(Name = "CreatePerson")]
        public async Task<Person> Post(string FirstName, string LastName)
        {
            await PreCheck();

            var address =await iAddressWebApi.GetAddress(FirstName, LastName);

            var person = new Person()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = address
            };

            personContext.Persons.Add(person);
            await personContext.SaveChangesAsync();


            await PostCheck();

            Telemetry.CountCreatePerson.Add(1);

            return person;
        }


        private async Task<bool> PreCheck()
        {
            using var myActivity = Telemetry.MyActivitySource.StartActivity("PreCheck");

            await Task.Delay(200);

            return true;
        }

        private async Task<bool> PostCheck()
        {
            using var myActivity = Telemetry.MyActivitySource.StartActivity("PostCheck");

            await Task.Delay(100);

            return true;
        }




    }
}
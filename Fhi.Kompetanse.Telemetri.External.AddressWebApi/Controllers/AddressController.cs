using Bogus;
using Fhi.Kompetanse.Telemetri.Domene;
using Microsoft.AspNetCore.Mvc;

namespace Fhi.Kompetanse.Telemetri.External.AddressWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
       

        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAddress")]
        public async Task<Address> Get(string FirstName, string LastName)
        {
            var faker = new Faker();

            var address = new Address()
            {
                Street = faker.Address.StreetName(),
                City = faker.Address.City(),
            };

            return address;
        }
    }
}

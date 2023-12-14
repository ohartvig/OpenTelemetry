using Fhi.Kompetanse.Telemetri.Domene;
using Refit;

namespace Fhi.Kompetanse.Telemetri.Contracts
{
    public interface IPersonWebApi
    {
        [Get(path: "/Person")]
        Task<List<Person>> GetAllPersons();

        [Post(path: "/Person")]
        Task<Person> CreatePerson(string FirstName, string LastName);
    }
}

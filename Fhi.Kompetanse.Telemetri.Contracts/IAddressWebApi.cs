using Fhi.Kompetanse.Telemetri.Domene;
using Refit;

namespace Fhi.Kompetanse.Telemetri.Contracts
{
    public interface IAddressWebApi
    {
        [Get(path: "/Address")]
        Task<Address> GetAddress(string FirstName, string LastName);
    }
}

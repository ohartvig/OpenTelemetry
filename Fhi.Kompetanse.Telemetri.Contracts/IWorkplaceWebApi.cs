using Fhi.Kompetanse.Telemetri.Domene;
using Refit;

namespace Fhi.Kompetanse.Telemetri.Contracts
{
    public interface IWorkplaceWebApi
    {
        [Get(path: "/Workplace")]
        Task<IList<Workplace>> GetAllWorkplaces();

        [Get(path: "/Workplace/{id}")]
        Task<IList<Workplace>> GetAllWorkplacesForPerson(int id);

        [Post(path: "/Workplace")]
        Task<Workplace> CreateWorkplace(int personId);
    }
}

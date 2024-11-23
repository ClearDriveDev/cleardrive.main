using CAS.backend.Models.Datas.Entities;
using CAS.backend.Models.Responses;

namespace CAS.backend.Repos
{
    public interface IAdministratorRepo
    {
        Task<List<Administrator>> GetAll();
        Task<Administrator?> GetBy(Guid id);
        Task<ControllerResponse> UpdateAdminAsync(Administrator administrator);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}

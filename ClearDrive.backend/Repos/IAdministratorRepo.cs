using ClearDrive.backend.Models.Datas.Entities;
using ClearDrive.backend.Models.Responses;

namespace ClearDrive.backend.Repos
{
    public interface IAdministratorRepo
    {
        Task<List<Administrator>> GetAll();
        Task<Administrator?> GetBy(Guid id);
        Task<ControllerResponse> UpdateAdminAsync(Administrator administrator);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}

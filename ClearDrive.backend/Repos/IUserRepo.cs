using ClearDrive.backend.Models.Datas.Entities;
using ClearDrive.shared.Responses;

namespace ClearDrive.backend.Repos
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();
        Task<User?> GetBy(Guid id);
        Task<ControllerResponse> UpdateUserAsync(User user);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;

namespace CAS.backend.Repos
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();
        Task<User?> GetBy(Guid id);
        Task<ControllerResponse> UpdateUserAsync(User user);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
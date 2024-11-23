using CAS.backend.Models.Datas.Entities;
using CAS.backend.Models.Responses;

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
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;

namespace CAS.backend.Repos
{
    public interface IPositionRepo
    {
        Task<List<Position>> GetAll();
        Task<Position?> GetBy(Guid id);
        Task<ControllerResponse> UpdatePositionAsync(Position position);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}

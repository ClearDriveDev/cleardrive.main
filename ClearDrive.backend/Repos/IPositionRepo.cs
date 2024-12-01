using ClearDrive.shared.Models;
using ClearDrive.shared.Responses;

namespace ClearDrive.backend.Repos
{
    public interface IPositionRepo
    {
        Task<List<Position>> GetAll();
        Task<Position?> GetBy(Guid id);
        Task<ControllerResponse> UpdatePositionAsync(Position position);
        Task<ControllerResponse> InsertAsync(Position position);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}

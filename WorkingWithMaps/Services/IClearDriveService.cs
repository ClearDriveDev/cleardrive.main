using CAS.shared.Models.Responses;
using WorkingWithMaps.Models;

namespace WorkingWithMaps.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<ControllerResponse> InsertAsync(Position position);
        public Task<ControllerResponse> DeleteAsync(Guid guid);
    }
}
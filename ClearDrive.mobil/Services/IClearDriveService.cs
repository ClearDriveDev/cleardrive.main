using ClearDrive.mobil.Responses;
using ClearDrive.mobil.Models;

namespace ClearDrive.mobil.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<ControllerResponse> InsertAsync(Position position);
        public Task<ControllerResponse> UpdateAsync(Position position);
        public Task<ControllerResponse> DeleteAsync(Guid guid);
    }
}
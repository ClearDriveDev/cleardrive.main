using ClearDrive.desktop.Responses;
using ClearDrive.desktop.Models;

namespace ClearDrive.desktop.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<ControllerResponse> InsertAsync(Position position);
        public Task<ControllerResponse> DeleteAsync(Guid guid);
        public Task<ControllerResponse> UpdateAsync(Position position);
    }
}
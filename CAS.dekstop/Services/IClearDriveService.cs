using CAS.dekstop.Responses;
using CAS.dekstop.Models;

namespace CAS.dekstop.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<ControllerResponse> InsertAsync(Position position);
        public Task<ControllerResponse> DeleteAsync(Guid guid);
    }
}
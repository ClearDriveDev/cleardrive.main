using WorkingWithMaps.Models;

namespace WorkingWithMaps.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
    }
}

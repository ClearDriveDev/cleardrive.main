using System.Threading.Tasks;

namespace ClearDrive.mobil.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

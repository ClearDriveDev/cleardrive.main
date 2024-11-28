using System.Threading.Tasks;

namespace ClearDrive.desktop.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

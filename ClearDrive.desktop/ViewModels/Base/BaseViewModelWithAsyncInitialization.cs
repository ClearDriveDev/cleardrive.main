using System.Threading.Tasks;

namespace ClearDrive.desktop.ViewModels.Base
{
    public class BaseViewModelWithAsyncInitialization : BaseViewModel, IAsyncInitialization
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

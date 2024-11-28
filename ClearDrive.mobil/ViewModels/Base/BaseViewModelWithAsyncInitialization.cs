using System.Threading.Tasks;

namespace ClearDrive.mobil.ViewModels.Base
{
    public class BaseViewModelWithAsyncInitialization : BaseViewModel, IAsyncInitialization
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

using System.Threading.Tasks;

namespace WorkingWithMaps.ViewModels.Base
{
    public class BaseViewModelWithAsyncInitialization : BaseViewModel, IAsyncInitialization
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

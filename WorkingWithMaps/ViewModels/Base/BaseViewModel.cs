using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkingWithMaps.ViewModels.Base
{
    public class BaseViewModel : ObservableObject, IAsyncInitialization
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

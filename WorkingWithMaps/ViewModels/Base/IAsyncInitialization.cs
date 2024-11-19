using System.Threading.Tasks;

namespace WorkingWithMaps.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

using System.Threading.Tasks;

namespace CAS.desktop.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

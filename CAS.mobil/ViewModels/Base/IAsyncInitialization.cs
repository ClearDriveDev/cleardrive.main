using System.Threading.Tasks;

namespace CAS.mobil.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

using System.Threading.Tasks;

namespace CAS.dekstop.ViewModels.Base
{
    public interface IAsyncInitialization
    {
        public Task InitializeAsync();
    }
}

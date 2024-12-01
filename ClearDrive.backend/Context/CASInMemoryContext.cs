using Microsoft.EntityFrameworkCore;

namespace ClearDrive.backend.Context
{
    public class CASInMemoryContext : CASContext
    {
        public CASInMemoryContext(DbContextOptions<CASContext> options) : base(options) { }
    }
}

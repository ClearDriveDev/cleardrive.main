using CAS.backend.Context;
using Microsoft.EntityFrameworkCore;

namespace CAS.backend.Context
{
    public class CASInMemoryContext : CASContext
    {
        public CASInMemoryContext(DbContextOptions<CASContext> options) : base(options) { }
    }
}

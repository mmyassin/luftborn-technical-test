using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.TechnicalTest.EntityFrameworkCore
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        Task<TDbContext> GetDbContextAsync();

        TDbContext GetDbContext();
    }

    public sealed class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext { get; }

        public DbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return DbContext;
        }

        public Task<TDbContext> GetDbContextAsync()
        {
            return Task.FromResult(DbContext);
        }
    }
}

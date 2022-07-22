using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.TechnicalTest.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
        
        Task<DbContext> GetDbContextAsync();
    }
}

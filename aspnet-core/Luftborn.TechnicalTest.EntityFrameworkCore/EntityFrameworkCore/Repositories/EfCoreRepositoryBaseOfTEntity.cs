using Luftborn.TechnicalTest.Domain.Entities;
using Luftborn.TechnicalTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.TechnicalTest.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public EfCoreRepositoryBase(IDbContextProvider<TechnicalTestDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
using Luftborn.TechnicalTest.Authorization.Users;
using Luftborn.TechnicalTest.Products;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.TechnicalTest.EntityFrameworkCore
{
    public class TechnicalTestDbContext : DbContext
    {
        public TechnicalTestDbContext(DbContextOptions<TechnicalTestDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

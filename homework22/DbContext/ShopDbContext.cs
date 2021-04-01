using homework22.Models;
using Microsoft.EntityFrameworkCore;

namespace homework22.DbContext
{
    public class ShopDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ShopDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
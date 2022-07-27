using GeekBurguer.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer.Products.Repository
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}

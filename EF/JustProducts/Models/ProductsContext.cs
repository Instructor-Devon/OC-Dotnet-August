using Microsoft.EntityFrameworkCore;

namespace JustProducts.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions options) : base(options) {}
        public DbSet<Product> Products {get;set;}
    }
}
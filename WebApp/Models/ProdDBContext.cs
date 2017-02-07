using System.Data.Entity;

namespace WebApp.Models
{
    public class ProdDBContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Person> Person { get; set; }
    }
}
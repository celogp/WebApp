using System.Linq;

namespace WebApp.Models
{
    public class Category : ICategory
    {
        ProdDBContext db = new ProdDBContext();

        public int Id { get; set; }
        public string Name { get; set; }

        public IQueryable<Category> GetAll()
        {
            return db.Categories;
        }

    }
}
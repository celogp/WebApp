using System.Linq;

namespace WebApp.Models
{
    public interface ICategory
    {
        IQueryable<Category> GetAll();
    }
}

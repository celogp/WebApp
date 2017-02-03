using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp.Models
{
    public interface IProduct
    {
        void Add(Product item);
        IQueryable<ProductSearch> GetAll(string name);
        Product Find(int key);
        void Remove(Product item);
        void Update(Product item);

    }
}

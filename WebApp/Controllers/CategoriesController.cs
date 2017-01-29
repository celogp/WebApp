using System.Linq;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Categorias")]
    public class CategoriesController : ApiController
    {
        private Category _Category = new Category();

        // GET: api/Products
        [AcceptVerbs("GET")]
        [Route("ConsultarCategorias")]
        public IQueryable<Category> GetCategories()
        {
            return _Category.GetAll();
        }

    }
}

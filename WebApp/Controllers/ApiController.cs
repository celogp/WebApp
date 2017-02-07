using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api")]
    public class ApiController : System.Web.Http.ApiController
    {
        //================================
        //Classes
        private Product _Product = new Product();
        private Category _Category = new Category();
        private Person _Person = new Person();

        //================================
        // GET: api/metodo
        [AcceptVerbs("GET")]
        [Route("AllPerson")]
        public IQueryable<Person> GetPerson(string name)
        {
            return _Person.GetAll(name);
        }

        //================================
        // GET: api/metodo
        [AcceptVerbs("GET")]
        [Route("AllCategories")]
        public IQueryable<Category> GetCategories()
        {
            return _Category.GetAll();
        }


        //================================
        // GET: api/metodo
        [AcceptVerbs("GET")]
        [Route("AllProducts")]
        public IQueryable<ProductSearch> GetProducts()
        {
            return _Product.GetAll(null);
        }

        // GET: api/metodo/namexxx
        [AcceptVerbs("GET")]
        [Route("AllProductsForNome/{name:alpha}")]
        public IQueryable<ProductSearch> GetProducts(string name)
        {
            return _Product.GetAll(name);
        }

        // GET: api/metodo/5
        [AcceptVerbs("GET")]
        [Route("ProductForId/{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT: api/metodo/5
        [AcceptVerbs("PUT")]
        [Route("SaveProduct/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "Product invalid !" });
            }

            if ((id != product.Id) || (id == 0))
            {
                return Json(new { Message = "Product not found !" });
            }

            try
            {
                _Product.Update(product);
            }
            catch
            {
                throw;
            }
            return Json(new { Message = "Edit to sucess!" });
        }

        // POST: api/metodo
        [AcceptVerbs("POST")]
        [Route("AddProduct")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "Product invalid." });
            }
            try
            {
                _Product.Add(product);
            }
            catch
            {
                throw;
            }
            return Json(new { Message = "Product add to Sucess!" });
        }

        // DELETE: api/metodo/5
        [AcceptVerbs("DELETE")]
        [Route("EraseProduct/{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = _Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _Product.Remove(product);

            return Json(new { Message = "Erase to Sucess" });
        }

    }
}

using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private Product _Product = new Product();

        // GET: api/Products
        [AcceptVerbs("GET")]
        [Route("AllProducts")]
        public IQueryable<ProductSearch> GetProducts()
        {
            return _Product.GetAll(null);
        }

        // GET: api/Products/namexxx
        [AcceptVerbs("GET")]
        [Route("AllProductsForNome/{name:alpha}")]
        public IQueryable<ProductSearch> GetProducts(string name)
        {
            return _Product.GetAll(name);
        }

        // GET: api/Products/5
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

        // PUT: api/Products/5
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

        // POST: api/Products
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

        // DELETE: api/Products/5
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp.Models
{
    public class Product : IProduct
    {
        ProdDBContext db = new ProdDBContext();
        private string NameCat;

        public int CategoryId { get; set; }
        public int Id { get;  set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //public class LstProduct : Product
        //{
        //    public string NameCat { get; set; } //não vai para o banco de dados, mas não funciona a consulta.
        //}

        public IQueryable<Product> GetAll(string name)
        {
            IQueryable<Product> lista;
            if (name == null)
            {
                lista = db.Products.AsQueryable().OrderByDescending(Product => Product.Id).Take(5);
            }
            else
            {
                lista = (from product in db.Products where product.Name.Contains(name) select product)
                .OrderByDescending(Product => Product.Id);
            }
            //lista = (from cate in db.Categories
            //         join prod in db.Products on cate.Id equals prod.CategoryId
            //         select new { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, NameCat = cate.Name }
            //        ).AsQueryable()
            //        .Select(x => new Product() { Id = x.Id, Name = x.Name, CategoryId = x.CategoryId, Price = x.Price, NameCat = x.Name });

            try
            {
                return lista;
            }
            catch ( Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public void Add(Product item)
        {
            try
            {
                if (item.Id == 0)
                {
                    var idMax = (from p in db.Products
                                 select p.Id).Max();
                    item.Id = idMax + 1;
                }

                db.Products.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product Find(int key)
        {
            try
            {
                return db.Products.Find(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Product item)
        {
            try
            {
                db.Products.Remove(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class Product : IProduct
    {
        ProdDBContext db = new ProdDBContext();

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public IQueryable<ProductSearch> GetAll(string name)
        {
            IQueryable<ProductSearch> lstFull;

            if (name == null)
            {
                lstFull = (from cate in db.Categories
                           join prod in db.Products on cate.Id equals prod.CategoryId
                           select new ProductSearch() { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, NameCategory = cate.Name }
                        )
                        .OrderByDescending(Product => Product.Id).Take(5)
                        .Select(x => new ProductSearch() { Id = x.Id, Name = x.Name, CategoryId = x.CategoryId, Price = x.Price, NameCategory = x.NameCategory }).AsQueryable();
            }
            else
            {
                lstFull = (from cate in db.Categories
                           join prod in db.Products on cate.Id equals prod.CategoryId
                           where prod.Name.Contains(name)
                           select new ProductSearch() { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, NameCategory = cate.Name }
                        )
                        .OrderByDescending(Product => Product.Id).Take(5)
                        .Select(x => new ProductSearch() { Id = x.Id, Name = x.Name, CategoryId = x.CategoryId, Price = x.Price, NameCategory = x.NameCategory }).AsQueryable();
            }
            try
            {
                return lstFull;
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
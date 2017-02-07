using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Person : IPerson
    {
        ProdDBContext db = new ProdDBContext();

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string email { get; set; }
        [StringLength(100)]
        public string namePerson { get; set; }
        [StringLength(100)]
        public string Skype { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string LinkeDin { get; set; }

        [StringLength(10)]
        public string State { get; set; }

        [StringLength(20)]
        public string City { get; set; }
        [StringLength(100)]
        public string Portifolio { get; set; }
        public int HoursWork { get; set; }
        public int TimeWork { get; set; }
        public decimal SalaryHours { get; set; }
        public string NameBank { get; set; }
        public string linkTeste { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression("^[0-9]{8}$")]

        public void Add(Person item)
        {
            throw new NotImplementedException();
        }

        public Person Find(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> GetAll(string name)
        {
            IQueryable<Person> lstFull;
            if (name == null)
            {
                lstFull = (from pers in db.Person
                           select new Person() { Id = pers.Id, namePerson = pers.namePerson }
                        )
                        .Take(5)
                        .OrderByDescending(Person => Person.Id);
            }
            else
            {
                lstFull = (from pers in db.Person
                           where pers.namePerson.Contains(name)
                           select new Person() { Id = pers.Id, namePerson = pers.namePerson }
                        )
                        .Take(5)
                        .OrderByDescending(Person => Person.Id);
            }
            try
            {
                return lstFull;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Person item)
        {
            throw new NotImplementedException();
        }

        public void Update(Person item)
        {
            throw new NotImplementedException();
        }
    }
}
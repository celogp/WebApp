using System.Linq;

namespace WebApp.Models
{
    interface IPerson
    {
        void Add(Person item);
        IQueryable<Person> GetAll(string name);
        Person Find(int key);
        void Remove(Person item);
        void Update(Person item);
    }
}

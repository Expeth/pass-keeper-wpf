using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public class FileRepository<T> : IRepository<T>
                                     where T : class
    {
        private IList<T> items;

        public FileRepository()
        {
            items = new List<T>();
        }

        public void Create(T item)
        {
            items.Add(item);
        }

        public void Delete(T item)
        {
            items.Remove(item);
        }

        public T GetById(int id)
        {
            return items[id];
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}

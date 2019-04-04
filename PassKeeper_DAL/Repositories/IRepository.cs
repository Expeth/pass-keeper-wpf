using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_DAL.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        void CreateOrUpdate(T obj);
        void Delete(T obj);
        void Save();
    }
}

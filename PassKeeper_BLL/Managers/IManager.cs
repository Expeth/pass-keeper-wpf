using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Managers
{
    public interface IManager<T>
    {
        IEnumerable<T> GetAll();
        void CreateOrUpdate(T obj);
        void Delete(T obj);
        void Save();
    }
}

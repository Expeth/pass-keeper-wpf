using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public interface IRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(User item);
        void Update(User item);
        void Delete(User item);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Infrastructure
{
    public interface IFactory<T>
    {
        T GetInstance();
    }
}

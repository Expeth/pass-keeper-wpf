using Caliburn.Micro;
using PassKeeper_BLL.Infrastructure;
using PassKeeper_DAL;
using PassKeeper_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Caliburn
{
    public class BLL_Container : SimpleContainer
    {
        public BLL_Container()
        {
            this.PerRequest<IRepository<Record>, RecordRepository>();
            this.PerRequest<IRepository<User>, UserRepository>();
        }
    }
}

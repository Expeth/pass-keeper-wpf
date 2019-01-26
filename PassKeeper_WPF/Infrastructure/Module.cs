using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRepository<User>>().To<FileRepository<User>>();
        }
    }
}

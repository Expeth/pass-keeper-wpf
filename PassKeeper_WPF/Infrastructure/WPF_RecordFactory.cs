using PassKeeper_BLL.Infrastructure;
using PassKeeper_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF.Infrastructure
{
    public class WPF_RecordFactory : IFactory<IRecord>
    {
        public IRecord GetInstance()
        {
            return new WPF_Record();
        }
    }
}

using PassKeeper_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Infrastructure
{
    public class RecordDTOFactory : IFactory<IRecord>
    {
        public IRecord GetInstance()
        {
            return new RecordDTO();
        }
    }
}

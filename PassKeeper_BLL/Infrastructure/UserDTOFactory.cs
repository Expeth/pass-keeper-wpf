using PassKeeper_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Infrastructure
{
    public class UserDTOFactory : IFactory<IUser>
    {
        public IUser GetInstance()
        {
            return new UserDTO();
        }
    }
}

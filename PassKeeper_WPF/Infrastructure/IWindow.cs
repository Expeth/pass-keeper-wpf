using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public interface IWindow
    {
        void CloseWindow();
        LoginWindowViewModel LoginViewModel { set; }
    }
}

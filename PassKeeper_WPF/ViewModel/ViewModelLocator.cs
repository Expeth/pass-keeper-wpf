using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace PassKeeper_WPF
{
    public class ViewModelLocator
    {
        private IKernel kernel;
        
        public ViewModelLocator()
        {
            kernel = new StandardKernel(new Module());
        }

        public LoginWindowViewModel LoginWindowViewModel
        {
            get => kernel.Get<LoginWindowViewModel>();
        }
    }
}

using Caliburn.Micro;
using PassKeeper_BLL.Caliburn;
using PassKeeper_BLL.DTO;
using PassKeeper_BLL.Infrastructure;
using PassKeeper_BLL.Managers;
using PassKeeper_WPF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassKeeper_WPF
{
    class MyBootstrapper:BootstrapperBase
    {
        private SimpleContainer _simpleContainer;
        public MyBootstrapper()
        {
            Initialize();
        }
        protected override void Configure()
        {
            _simpleContainer = new BLL_Container();
            _simpleContainer.Singleton<IWindowManager, WindowManager>();
            _simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            _simpleContainer.PerRequest<LoginWindowViewModel, LoginWindowViewModel>();
            
            _simpleContainer.PerRequest<IManager<IUser>, UserManager>();
            _simpleContainer.PerRequest<IManager<IRecord>, RecordManager>("RecordManager");

            _simpleContainer.PerRequest<IFactory<IRecord>, WPF_RecordFactory>("RecordFactory");
            _simpleContainer.PerRequest<IFactory<IUser>, UserDTOFactory>();
        }
        protected override void BuildUp(object instance)
        {
            _simpleContainer.BuildUp(instance);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _simpleContainer.GetAllInstances(service);
        }
        public object Get(Type service, string key)
        {
            return _simpleContainer.GetInstance(service, key);
        }
        protected override object GetInstance(Type service, string key)
        {
            return _simpleContainer.GetInstance(service, key);
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoginWindowViewModel>();
        }
    }
}

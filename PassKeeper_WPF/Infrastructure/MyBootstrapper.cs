using Caliburn.Micro;
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
            _simpleContainer = new SimpleContainer();
            _simpleContainer.Singleton<IWindowManager, WindowManager>();
            _simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            _simpleContainer.PerRequest<LoginWindowViewModel, LoginWindowViewModel>();
            _simpleContainer.Singleton<IRepository, FileRepository>();
        }
        protected override void BuildUp(object instance)
        {
            _simpleContainer.BuildUp(instance);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _simpleContainer.GetAllInstances(service);
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

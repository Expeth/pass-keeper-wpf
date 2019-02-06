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
        private SimpleContainer simpleContainer;
        public MyBootstrapper()
        {
            Initialize();
        }
        protected override void Configure()
        {
            simpleContainer = new SimpleContainer();
            simpleContainer.Singleton<IWindowManager, WindowManager>();
            simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            simpleContainer.PerRequest<LoginWindowViewModel, LoginWindowViewModel>();
            simpleContainer.Singleton<IRepository, FileRepository>();
        }
        protected override void BuildUp(object instance)
        {
            simpleContainer.BuildUp(instance);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return simpleContainer.GetAllInstances(service);
        }
        protected override object GetInstance(Type service, string key)
        {
            return simpleContainer.GetInstance(service, key);
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoginWindowViewModel>();
        }
    }
}

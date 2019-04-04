using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;
using PassKeeper_WPF.Infrastructure;
using PassKeeper_BLL.DTO;
using PassKeeper_BLL.Managers;
using PassKeeper_BLL.Infrastructure;

namespace PassKeeper_WPF
{
    public class LoginWindowViewModel : PropertyChangedBase
    {
        #region Private Fields
        private IFactory<IUser> _userFactory;
        private MyBootstrapper _bootstrapper;
        private NotifyCollection<IUser> _users;
        private ConfigSaver _configSaver;
        private IWindowManager _windowManager;
        private string _info;
        #endregion

        #region Public Fields
        public string InformationString
        {
            get => _info;
            set
            {
                _info = value;
                NotifyOfPropertyChange(() => InformationString);
            }
        }
        public string Username { get; set; }
        #endregion

        public LoginWindowViewModel(IWindowManager windowManager, IManager<IUser> userManager, IFactory<IUser> userFactory)
        {
            _bootstrapper = (Application.Current.Resources["bootstrapper"] as MyBootstrapper);
            _configSaver = new ConfigSaver("ApplicationConfiguration.json");
            _users = new NotifyCollection<IUser>(userManager);
            _configSaver.Config.Configure();
            _windowManager = windowManager;
            _info = "";
            _userFactory = userFactory;
        }

        public void SignUpMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = _userFactory.GetInstance();
            user.Username = Username;
            user.Password = (obj as PasswordBox).Password;
            

            bool res = _users.ToList().Exists(x => x.Username == user.Username);
            if (res)
            {
                InformationString = "User already exists!";
                return;
            }

            _users.Add(user);
            InformationString = "Signed up";
        }

        public void LogInMethod(object obj, IWindow wnd)
        {
            if (string.IsNullOrEmpty(Username) || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = _userFactory.GetInstance();
            user.Username = Username;
            user.Password = (obj as PasswordBox).Password;

            var res = _users.ToList().Find(x => x.Username == user.Username && x.Password == user.Password);
            if (res == null)
            {
                InformationString = "Incorrect login or password!";
                return;
            }
            InformationString = "Logged in";

            _windowManager.ShowWindow(new MainWindowViewModel(res, _configSaver,
                                     (IManager<IRecord>)_bootstrapper.Get(typeof(IManager<IRecord>), "RecordManager"),
                                     (IFactory<IRecord>)_bootstrapper.Get(typeof(IFactory<IRecord>), "RecordFactory")));
            wnd.CloseWindow();
        }

        public void CloseWindow(IWindow wnd)
        {
            wnd.CloseWindow();
        }

        public void MinimizeWindow(IWindow wnd)
        {
            wnd.MinimizeWindow();
        }
    }
}
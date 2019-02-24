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

namespace PassKeeper_WPF
{
    public class LoginWindowViewModel : PropertyChangedBase
    {
        private ConfigSaver _configSaver;
        private IRepository _users;
        private IWindowManager _windowManager;
        private string _info;

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

        public LoginWindowViewModel(IRepository repository, IWindowManager windowManager)
        {
            _configSaver = new ConfigSaver("ApplicationConfiguration.json");
            _configSaver.Config.Configure();
            InformationString = "";
            _users = repository;
            this._windowManager = windowManager;
        }

        public void SignUpMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = new User(Username, (obj as PasswordBox).Password);
            bool res = (_users.GetAll() as List<User>).Exists(x => x.Username == user.Username);
            if (res)
            {
                InformationString = "User already exists!";
                return;
            }

            _users.Create(user);
            InformationString = "Signed up";
        }

        public void LogInMethod(object obj, IWindow wnd)
        {
            if (string.IsNullOrEmpty(Username) || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = new User(Username, (obj as PasswordBox).Password);
            var res = (_users.GetAll() as List<User>).Find(x => x.Equals(user));
            if (res == null)
            {
                InformationString = "Incorrect login or password!";
                return;
            }
            InformationString = "Logged in";

            _windowManager.ShowWindow(new MainWindowViewModel(res, _users, _configSaver));
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
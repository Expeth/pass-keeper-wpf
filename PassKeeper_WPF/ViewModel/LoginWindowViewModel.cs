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
        private ConfigSaver configSaver;
        private IRepository users;
        private IWindowManager windowManager;
        private string info;

        public string InformationString
        {
            get => info;
            set
            {
                info = value;
                NotifyOfPropertyChange(() => InformationString);
            }
        }
        public string Username { get; set; }

        public LoginWindowViewModel(IRepository repository, IWindowManager windowManager)
        {
            configSaver = new ConfigSaver("ApplicationConfiguration.json");
            configSaver.Config.Configure();
            InformationString = "";
            users = repository;
            this.windowManager = windowManager;
        }

        public void SignUpMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = new User(Username, (obj as PasswordBox).Password);
            bool res = (users.GetAll() as List<User>).Exists(x => x.Username == user.Username);
            if (res)
            {
                InformationString = "User already exists!";
                return;
            }

            users.Create(user);
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
            var res = (users.GetAll() as List<User>).Find(x => x.Equals(user));
            if (res == null)
            {
                InformationString = "Incorrect login or password!";
                return;
            }
            InformationString = "Logged in";

            windowManager.ShowWindow(new MainWindowViewModel(res, users, configSaver));
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
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

namespace PassKeeper_WPF
{
    public class LoginWindowViewModel : PropertyChangedBase
    {
        private IRepository<User> users;
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

        public LoginWindowViewModel(IRepository<User> repository, IWindowManager windowManager)
        {
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
            bool res = (users.GetAll() as List<User>).Exists(x => x.Equals(user));
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

            windowManager.ShowWindow(new MainWindowViewModel(user));
            wnd.CloseWindow();
        }
    }
}
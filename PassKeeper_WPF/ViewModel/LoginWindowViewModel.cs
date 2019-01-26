using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PassKeeper_WPF
{
    public class LoginWindowViewModel
    {
        public string InformationString { get; set; }
        private IRepository<User> users;
        private string username;

        public LoginWindowViewModel(FileRepository<User> repository)
        {
            InformationString = "";
            users = repository;

            LogInCommand = new RelayCommand(LogInMethod);
            SignUpCommand = new RelayCommand(SignUpMethod);
        }

        private void SignUpMethod(object obj)
        {
            bool res = (users.GetAll() as List<User>).Exists(x => x == new User(username, obj as string));
            if (res)
            {
                InformationString = "User already exists!";
                return;
            }

            InformationString = "Signed up";
        }

        private void LogInMethod(object obj)
        {
            bool res = (users.GetAll() as List<User>).Exists(x => x == new User(username, obj as string));
            if (!res)
            {
                InformationString = "Incorrect login or password!";
                return;
            }

            InformationString = "Logged in";
            //TODO: open mainwindow
        }

        #region Commands
        public ICommand LogInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion
    }
}

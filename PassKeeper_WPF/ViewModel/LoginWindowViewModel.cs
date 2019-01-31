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
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        private LoginWindow loginWindow;
        private MainWindow mainWindow;
        private IRepository<User> users;
        private string info;

        public string InformationString
        {
            get => info;
            set
            {
                info = value;
                Notify();
            }
        }
        public string Username { get; set; }

        public LoginWindowViewModel(IRepository<User> repository/*, LoginWindow wnd*/)
        {
            InformationString = "";
            users = repository;
            //loginWindow = wnd;

            LogInCommand = new RelayCommand(LogInMethod);
            SignUpCommand = new RelayCommand(SignUpMethod);
        }

        private void SignUpMethod(object obj)
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

        private void LogInMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
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
            mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.DataContext = new MainWindowViewModel(res);
            //TODO: close login window
            //loginWindow.CloseWindow();
        }

        #region Commands
        public ICommand LogInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region PropertyChanged
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}

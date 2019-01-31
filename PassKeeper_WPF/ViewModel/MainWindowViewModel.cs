using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PassKeeper_WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region private fields
        private string username;
        private string title;
        private string password;
        private string note;
        private string websiteName;
        #endregion

        #region public properties
        public string Username
        {
            get => username;
            set
            {
                username = value;
                Notify();
            }
        }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                Notify();
            }
        }
        public string Note
        {
            get => note;
            set
            {
                note = value;
                Notify();
            }
        }
        public string WebsiteName
        {
            get => websiteName;
            set
            {
                websiteName = value;
                Notify();
            }
        }
        #endregion

        public User User { get; set; }

        public MainWindowViewModel(User user)
        {
            User = user;

            AddRecordCommand = new RelayCommand(AddRecordMethod);
            SortCommand = new RelayCommand(SortMethod);
            PopupCloseCommand = new RelayCommand(
                x =>
                {
                    Title = Note = Password = Username = WebsiteName = "";
                });
        }

        private void SortMethod(object obj)
        {
            switch (obj as String)
            {
                case "Title":
                    {
                        User.Records = new ObservableCollection<IRecord>(User.Records.OrderBy(x => x.Title));
                    }
                    break;
                case "WebsiteName":
                    {
                        User.Records = new ObservableCollection<IRecord>(User.Records.OrderBy(x => x.WebsiteName));
                    }
                    break;
                case "Date":
                    {
                        User.Records = new ObservableCollection<IRecord>(User.Records.OrderBy(x => x.CreationDate));
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddRecordMethod(object obj)
        {
            User.Records.Add(new Account(Title, Note, WebsiteName, Username, Password));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #region Commands
        public ICommand AddRecordCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand PopupCloseCommand { get; set; }
        #endregion
    }
}

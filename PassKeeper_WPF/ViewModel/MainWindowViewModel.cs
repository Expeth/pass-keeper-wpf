using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        public IRecord SelectedRecord { get; set; }
        public string SelectedTheme { get; set; }
        public string SelectedLanguage { get; set; }

        public MainWindowViewModel(User user)
        {
            User = user;

            AddRecordCommand = new RelayCommand(AddRecordMethod);
            DeleteRecordCommand = new RelayCommand(DeleteRecordMethod);
            SortCommand = new RelayCommand(SortMethod);
            CloseAddRecordPopupCommand = new RelayCommand(
                x =>
                {
                    Title = Note = Password = Username = WebsiteName = "";
                });
        }

        public void OpenPopup(Border wnd)
        {
            wnd.Visibility = System.Windows.Visibility.Visible;
        }

        public void ClosePopup(Border wnd)
        {
            wnd.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DeleteRecordMethod(object obj)
        {
            User.Records.Remove(SelectedRecord);
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
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand CloseAddRecordPopupCommand { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<IRecord> userRecords;
        public ObservableCollection<IRecord> UserRecords
        {
            get => userRecords;
            set
            {
                userRecords = value;
                Notify();
            }
        }

        public MainWindowViewModel(User user)
        {
            User = user;

            User.Records.Add(new Account("google", "1", "1", "1", "1"));
            User.Records.Add(new Account("drive", "1", "1", "1", "1"));
            User.Records.Add(new Account("instagram", "1", "1", "1", "1"));
            User.Records.Add(new Account("vk", "1", "1", "1", "1"));
            User.Records.Add(new Account("youtube", "1", "1", "1", "1"));
            User.Records.Add(new Account("kinozal", "1", "1", "1", "1"));
            User.Records.Add(new Account("mystat", "1", "1", "1", "1"));
            User.Records.Add(new Account("telegram", "1", "1", "1", "1"));
            User.Records.Add(new Account("vk", "1", "1", "1", "1"));
            User.Records.Add(new Account("youtube", "1", "1", "1", "1"));
            User.Records.Add(new Account("google", "1", "1", "1", "1"));
            User.Records.Add(new Account("drive", "1", "1", "1", "1"));
            User.Records.Add(new Account("flikr", "1", "1", "1", "1"));
            User.Records.Add(new Account("messanger", "1", "1", "1", "1"));
            User.Records.Add(new Account("facebook", "1", "1", "1", "1"));
            User.Records.Add(new Account("linked.in", "1", "1", "1", "1"));
            User.Records.Add(new Account("mystat", "1", "1", "1", "1"));
            User.Records.Add(new Account("torrent", "1", "1", "1", "1"));
            User.Records.Add(new Account("2ch", "1", "1", "1", "1"));
            User.Records.Add(new Account("twitch", "1", "1", "1", "1"));
            User.Records.Add(new Account("twitch", "1", "1", "1", "1"));
            User.Records.Add(new Account("privat24", "1", "1", "1", "1"));
            User.Records.Add(new Account("viber", "1", "1", "1", "1"));
            User.Records.Add(new Account("watsapp", "1", "1", "1", "1"));
            User.Records.Add(new Account("viber", "1", "1", "1", "1"));
            User.Records.Add(new Account("google2", "1", "1", "1", "1"));
            User.Records.Add(new Account("google3", "1", "1", "1", "1"));
            User.Records.Add(new Account("viber", "1", "1", "1", "1"));
            User.Records.Add(new Account("steam", "1", "1", "1", "1"));
            User.Records.Add(new Account("games", "1", "1", "1", "1"));

            UserRecords = new ObservableCollection<IRecord>(User.Records);

            AddRecordCommand = new RelayCommand(AddRecordMethod);
            DeleteRecordCommand = new RelayCommand(DeleteRecordMethod);
            SortCommand = new RelayCommand(SortMethod);
            SearchCommand = new RelayCommand(SearchMethod);
            CloseAddRecordPopupCommand = new RelayCommand(
                x =>
                {
                    Title = Note = Password = Username = WebsiteName = "";
                });
        }

        private void SearchMethod(object obj)
        {
            if (string.IsNullOrEmpty(obj as string))
            {
                UserRecords = new ObservableCollection<IRecord>(User.Records);
                return;
            }

            string searchString = obj as string;
            UserRecords = new ObservableCollection<IRecord>(User.Records.Where(x=>x.Title.Contains(searchString)));
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
        public ICommand SearchCommand { get; set; }
        #endregion
    }
}

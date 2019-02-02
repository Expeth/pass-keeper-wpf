using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private string category;
        private string selectedCategory;
        private ObservableCollection<IRecord> userRecords;
        #endregion

        #region public properties with Notify()
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
        public string Category
        {
            get => category;
            set
            {
                category = value;
                Notify();
            }
        }
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                Notify();
            }
        }
        public ObservableCollection<IRecord> UserRecords
        {
            get => userRecords;
            set
            {
                userRecords = value;
                Notify();
            }
        }
        #endregion

        #region public properties
        public User User { get; set; }
        public IRecord SelectedRecord { get; set; }
        public string SelectedTheme { get; set; }
        public string SelectedLanguage { get; set; }
        #endregion

        public MainWindowViewModel(User user)
        {
            SelectedCategory            = "All Passwords";
            User                        = user;
            UserRecords                 = new ObservableCollection<IRecord>(User.Records);
            AddRecordCommand            = new RelayCommand(AddRecordMethod);
            DeleteRecordCommand         = new RelayCommand(DeleteRecordMethod);
            SortCommand                 = new RelayCommand(SortMethod);
            SearchCommand               = new RelayCommand(SearchMethod);
            ChangeCategoryCommand       = new RelayCommand(ChangeCategoryMethod);
            CloseAddRecordPopupCommand  = new RelayCommand(CleanProperties);
        }

        private void CleanProperties(object obj)
        {
            Title = Note = Password = Username = WebsiteName = "";
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
                        UserRecords = new ObservableCollection<IRecord>(UserRecords.OrderBy(x => x.Title));
                    }
                    break;
                case "WebsiteName":
                    {
                        UserRecords = new ObservableCollection<IRecord>(UserRecords.OrderBy(x => x.WebsiteName));
                    }
                    break;
                case "Date":
                    {
                        UserRecords = new ObservableCollection<IRecord>(UserRecords.OrderBy(x => x.CreationDate));
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeCategoryMethod(object obj)
        {
            SelectedCategory = obj as string;
            switch (obj as String)
            {
                case "All Passwords":
                    {
                        UserRecords = new ObservableCollection<IRecord>(User.Records);
                    }
                    break;
                case "Website Accounts":
                    {
                        UserRecords = new ObservableCollection<IRecord>(User.Records.Where(x => x.Category == "Website Account"));
                    }
                    break;
                case "Email Accounts":
                    {
                        UserRecords = new ObservableCollection<IRecord>(User.Records.Where(x => x.Category == "Email Account"));
                    }
                    break;
                case "Credit Cards":
                    {
                        UserRecords = new ObservableCollection<IRecord>(User.Records.Where(x => x.Category == "Credit Card"));
                    }
                    break;
                case "Favorites":
                    {
                        UserRecords = new ObservableCollection<IRecord>(User.Records.Where(x => x.IsFavorite));
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddRecordMethod(object obj)
        {
            Category = String.IsNullOrEmpty(Category) ? "Website Account" : Category.Remove(0, 38);  

            var account = new Account(Title, Note, WebsiteName, Username, Password, Category);
            User.Records.Add(account);
            UserRecords.Add(account);
        }

        #region Commands
        public ICommand AddRecordCommand            { get; set; }
        public ICommand DeleteRecordCommand         { get; set; }
        public ICommand SortCommand                 { get; set; }
        public ICommand CloseAddRecordPopupCommand  { get; set; }
        public ICommand SearchCommand               { get; set; }
        public ICommand ChangeCategoryCommand       { get; set; }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}

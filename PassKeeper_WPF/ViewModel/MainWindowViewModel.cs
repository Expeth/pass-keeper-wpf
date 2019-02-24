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
using System.Windows.Media.Animation;
using CodeBits;

namespace PassKeeper_WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region private fields
        private string _username;
        private string _title;
        private string _password;
        private string _note;
        private string _websiteName;
        private ObservableCollection<Account> _userRecords;
        private IRepository _usersRepository;
        private Account _lastSelectedRecord;
        private ConfigSaver _configSaver;
        #endregion

        #region public properties with Notify()
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                Notify();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Notify();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Notify();
            }
        }
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                Notify();
            }
        }
        public string WebsiteName
        {
            get => _websiteName;
            set
            {
                _websiteName = value;
                Notify();
            }
        }
        public ObservableCollection<Account> UserRecords
        {
            get => _userRecords;
            set
            {
                _userRecords = value;
                Notify();
            }
        }
        #endregion

        #region public properties
        public User User { get; set; }
        public Account SelectedRecord
        {
            get => _lastSelectedRecord;
            set
            {
                if (value == null)
                    return;
                _lastSelectedRecord = value;
            }
        }
        public string SelectedTheme { get; set; }
        public string SelectedLanguage { get; set; }
        #endregion

        public MainWindowViewModel(User user, IRepository users, ConfigSaver cs)
        {
            _configSaver = cs;
            User = user;
            _usersRepository = users;

            SaveDataCommand = new RelayCommand(x => _usersRepository.Update(null));
            UserRecords = new ObservableCollection<Account>(User.Records);
            AddRecordCommand = new RelayCommand(AddRecordMethod);
            DeleteRecordCommand = new RelayCommand(DeleteRecordMethod);
            SortCommand = new RelayCommand(SortMethod);
            SearchCommand = new RelayCommand(SearchMethod);
            ChangeCategoryCommand = new RelayCommand(ChangeCategoryMethod);
            GeneratePasswordCommand = new RelayCommand(GeneratePasswordMethod);
            CopyUsernameCommand = new RelayCommand(x => Clipboard.SetText(SelectedRecord.Username));
            CopyPasswordCommand = new RelayCommand(x => Clipboard.SetText(SelectedRecord.Password));
        }

        private void GeneratePasswordMethod(object obj)
        {
            if ((obj as string) == "AddRecord")
            {
                Password = PasswordGenerator.Generate(new Random().Next(8, 20), PasswordCharacters.AllLetters | PasswordCharacters.Numbers);
                return;
            }
            SelectedRecord.Password = PasswordGenerator.Generate(new Random().Next(8, 20), PasswordCharacters.AllLetters | PasswordCharacters.Numbers);
        }

        public void CloseWindow(IWindow wnd)
        {
            wnd.CloseWindow();
        }

        public void MaximizeWindow(IWindow wnd)
        {
            wnd.MaximizeWindow();
        }

        public void MinimizeWindow(IWindow wnd)
        {
            wnd.MinimizeWindow();
        }

        public void ChangeLanguage(string language)
        {
            _configSaver.Config.Language = language;
            _configSaver.Save();
        }

        public void ChangeTheme(string theme)
        {
            _configSaver.Config.Theme = theme;
            _configSaver.Save();
        }

        private void CleanProperties()
        {
            Title = Note = Password = Username = WebsiteName = "";
        }

        private void SearchMethod(object obj)
        {
            string searchString = obj as string;
            UserRecords = new ObservableCollection<Account>(User.Records.Where(x => x.Title.Contains(searchString) ||
                                                                                    x.WebsiteName.Contains(searchString)));
        }

        private void DeleteRecordMethod(object obj)
        {
            User.Records.Remove(SelectedRecord);
            UserRecords.Remove(SelectedRecord);
            _usersRepository.Update(User);
        }

        private void SortMethod(object obj)
        {
            switch (obj as String)
            {
                case "Title":
                    {
                        UserRecords = new ObservableCollection<Account>(UserRecords.OrderBy(x => x.Title));
                    }
                    break;
                case "WebsiteName":
                    {
                        UserRecords = new ObservableCollection<Account>(UserRecords.OrderBy(x => x.WebsiteName));
                    }
                    break;
                case "Date":
                    {
                        UserRecords = new ObservableCollection<Account>(UserRecords.OrderBy(x => x.CreationDate));
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeCategoryMethod(object obj)
        {
            switch (obj as String)
            {
                case "All Passwords":
                    {
                        UserRecords = new ObservableCollection<Account>(User.Records);
                    }
                    break;
                case "Website Accounts":
                    {
                        UserRecords = new ObservableCollection<Account>(User.Records.Where(x => x.Category == 0));
                    }
                    break;
                case "Email Accounts":
                    {
                        UserRecords = new ObservableCollection<Account>(User.Records.Where(x => x.Category == 1));
                    }
                    break;
                case "Credit Cards":
                    {
                        UserRecords = new ObservableCollection<Account>(User.Records.Where(x => x.Category == 2));
                    }
                    break;
                case "Favorites":
                    {
                        UserRecords = new ObservableCollection<Account>(User.Records.Where(x => x.IsFavorite));
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddRecordMethod(object obj)
        {
            var account = new Account(Title, Note, WebsiteName, Username, Password, (int)obj);
            User.Records.Add(account);
            UserRecords.Add(account);
            _usersRepository.Update(User);
            CleanProperties();
        }

        #region Commands
        public ICommand AddRecordCommand { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangeCategoryCommand { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }
        public ICommand SaveDataCommand { get; set; }
        public ICommand CopyUsernameCommand { get; set; }
        public ICommand CopyPasswordCommand { get; set; }
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

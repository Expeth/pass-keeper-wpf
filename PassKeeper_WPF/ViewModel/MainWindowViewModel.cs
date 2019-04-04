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
using PassKeeper_BLL.DTO;
using PassKeeper_BLL.Infrastructure;
using PassKeeper_BLL.Managers;
using PassKeeper_WPF.Infrastructure;

namespace PassKeeper_WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private IFactory<IRecord> _recordFactory;
        private string _username;
        private string _title;
        private string _password;
        private string _note;
        private string _websiteName;
        private ObservableCollection<IRecord> _userRecords;
        private NotifyCollection<IRecord> _records;
        private IRecord _lastSelectedRecord;
        private ConfigSaver _configSaver;
        #endregion

        #region Public Properties with Notify()
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
        public ObservableCollection<IRecord> UserRecords
        {
            get => _userRecords;
            set
            {
                _userRecords = value;
                Notify();
            }
        }
        #endregion

        #region Public Properties
        public IUser User { get; set; }
        public IRecord SelectedRecord
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

        public MainWindowViewModel(IUser user, ConfigSaver configSaver, IManager<IRecord> recordManager, IFactory<IRecord> recordFactory)
        {
            User = user;
            _recordFactory = recordFactory;
            _configSaver = configSaver;
            _records = new NotifyCollection<IRecord>(recordManager);
            UserRecords = new ObservableCollection<IRecord>(_records.Where(x => x.UserId == User.Id));

            InitializeCommands();
        }

        private void GeneratePasswordMethod(object obj)
        {
            if ((obj as string) == "AddRecord")
            {
                Password = PasswordGenerator.Generate(new Random().Next(8, 20), PasswordCharacters.AllLetters | PasswordCharacters.Numbers);
                return;
            }
            SelectedRecord.Password = PasswordGenerator.Generate(new Random().Next(8, 20), PasswordCharacters.AllLetters | PasswordCharacters.Numbers);
            Console.WriteLine(SelectedRecord.Password);
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
            var userRecords = _records.Where(x => x.UserId == User.Id);
            string searchString = obj as string;
            UserRecords = new ObservableCollection<IRecord>(userRecords.Where(x => x.Title.Contains(searchString) ||
                                                                                    x.WebsiteName.Contains(searchString)));
        }

        private void DeleteRecordMethod(object obj)
        {
            _records.Remove(SelectedRecord);
            UserRecords.Remove(SelectedRecord);
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
            var userRecords = _records.Where(x => x.UserId == User.Id);
            switch (obj as String)
            {
                case "All Passwords":
                    {
                        UserRecords = new ObservableCollection<IRecord>(userRecords);
                    }
                    break;
                case "Website Accounts":
                    {
                        UserRecords = new ObservableCollection<IRecord>(userRecords.Where(x => x.Category == 0));
                    }
                    break;
                case "Email Accounts":
                    {
                        UserRecords = new ObservableCollection<IRecord>(userRecords.Where(x => x.Category == 1));
                    }
                    break;
                case "Credit Cards":
                    {
                        UserRecords = new ObservableCollection<IRecord>(userRecords.Where(x => x.Category == 2));
                    }
                    break;
                case "Favorites":
                    {
                        UserRecords = new ObservableCollection<IRecord>(userRecords.Where(x => (bool)x.IsFavorite));
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddRecordMethod(object obj)
        {
            var account = _recordFactory.GetInstance();
            account.Title = Title;
            account.Note = Note;
            account.WebsiteName = WebsiteName;
            account.Username = Username;
            account.Password = Password;
            account.Category = (int)obj;
            account.UserId = User.Id;
            account.CreationDate = DateTime.Now;
            account.IsFavorite = false;
            
            _records.Add(account);
            _records.UpdateCollection();
            UserRecords = new ObservableCollection<IRecord>(_records.Where(x => x.UserId == User.Id));

            CleanProperties();
        }

        private void InitializeCommands()
        {
            UpdateRecordCommand = new RelayCommand(x => _records.Update(UserRecords.Contains(SelectedRecord) ? SelectedRecord : null));
            AddRecordCommand = new RelayCommand(AddRecordMethod);
            DeleteRecordCommand = new RelayCommand(DeleteRecordMethod);
            SortCommand = new RelayCommand(SortMethod);
            SearchCommand = new RelayCommand(SearchMethod);
            ChangeCategoryCommand = new RelayCommand(ChangeCategoryMethod);
            GeneratePasswordCommand = new RelayCommand(GeneratePasswordMethod);
            CopyUsernameCommand = new RelayCommand(x => Clipboard.SetText(SelectedRecord.Username));
            CopyPasswordCommand = new RelayCommand(x => Clipboard.SetText(SelectedRecord.Password));
        }

        #region Commands
        public ICommand UpdateRecordCommand { get; set; }
        public ICommand AddRecordCommand { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangeCategoryCommand { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }
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

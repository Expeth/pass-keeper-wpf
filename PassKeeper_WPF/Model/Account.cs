using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    [Serializable]
    public class Account : IRecord, INotifyPropertyChanged
    {
        private string _title;
        private string _note;
        private string _websiteName;
        private string _username;
        private string _password;
        private int _category;
        private bool _isFavorite;

        public Account()
        {
            Title =
            Note =
            WebsiteName =
            Username =
            Password = "";
            CreationDate = DateTime.Now;
            _category = 0;
        }

        public Account(string title, string note, string websiteName, string username, string password, int category)
        {
            Title = title == null ? "" : title;
            Note = note == null ? "" : note;
            WebsiteName = websiteName == null ? "" : websiteName;
            Username = username == null ? "" : username;
            Password = password == null ? "" : password;
            Category = category;
            CreationDate = DateTime.Now;
        }

        #region Properties
        [JsonConverter(typeof(EncryptingJsonConverter), "title_decryptkey")]
        public string Title
        {
            get => _title == null ? "" : _title;
            set
            {
                _title = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "note_decryptkey")]
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "websitename_decryptkey")]
        public string WebsiteName
        {
            get => _websiteName == null ? "" : _websiteName;
            set
            {
                _websiteName = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "username_decryptkey")]
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "password_decryptkey")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Notify();
            }
        }
        public int Category
        {
            get => _category;
            set
            {
                _category = value;
                Notify();
            }
        }
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                Notify();
            }
        }
        #endregion

        public DateTime CreationDate { get; set; }

        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

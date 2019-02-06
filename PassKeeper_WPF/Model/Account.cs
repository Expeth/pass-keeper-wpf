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
        private string title;
        private string note;
        private string websiteName;
        private string username;
        private string password;
        private int category;
        private bool isFavorite;

        public Account()
        {
            Title =
            Note =
            WebsiteName =
            Username =
            Password = "";
            CreationDate = DateTime.Now;
            category = 0;
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
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "note_decryptkey")]
        public string Note
        {
            get => note;
            set
            {
                note = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "websitename_decryptkey")]
        public string WebsiteName
        {
            get => websiteName;
            set
            {
                websiteName = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "username_decryptkey")]
        public string Username
        {
            get => username;
            set
            {
                username = value;
                Notify();
            }
        }
        [JsonConverter(typeof(EncryptingJsonConverter), "password_decryptkey")]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                Notify();
            }
        }
        public int Category
        {
            get => category;
            set
            {
                category = value;
                Notify();
            }
        }
        public bool IsFavorite
        {
            get => isFavorite;
            set
            {
                isFavorite = value;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public class Account : IRecord, INotifyPropertyChanged
    {
        private string title;
        private string note;
        private string websiteName;
        private string username;
        private string password;
        private string category;
        private bool isFavorite;

        public Account(string title, string note, string websiteName, string username, string password, string category)
        {
            Title = title;
            Note = note;
            WebsiteName = websiteName;
            Username = username;
            Password = password;
            Category = category;
            CreationDate = DateTime.Now;
        }

        #region Properties
        public string Title
        {
            get => title;
            set
            {
                title = value;
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
        public string Username
        {
            get => username;
            set
            {
                username = value;
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
        public string Category
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

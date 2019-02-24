using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        private string _name;
        private string _username;
        private string _password;
        private ObservableCollection<Account> _records;

        #region public properties
        public ObservableCollection<Account> Records
        {
            get => _records;
            set
            {
                _records = value;
                Notify();
            }
        }

        [JsonConverter(typeof(EncryptingJsonConverter), "userpassword_decryptkey")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
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

        [JsonConverter(typeof(EncryptingJsonConverter), "name_decryptkey")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Notify();
            }
        }
        #endregion

        public User(string username, string password)
        {
            Records = new ObservableCollection<Account>();
            this._username = Name = username;
            this._password = password;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   _username == user._username &&
                   _password == user._password;
        }

        public override int GetHashCode()
        {
            var hashCode = 1710835385;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_password);
            return hashCode;
        }

        private void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
        private string name;
        private string username;
        private string password;
        private ObservableCollection<Account> records;

        #region public properties
        public ObservableCollection<Account> Records
        {
            get => records;
            set
            {
                records = value;
                Notify();
            }
        }

        [JsonConverter(typeof(EncryptingJsonConverter), "userpassword_decryptkey")]
        public string Password
        {
            get => password;
            set
            {
                password = value;
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

        [JsonConverter(typeof(EncryptingJsonConverter), "name_decryptkey")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Notify();
            }
        }
        #endregion

        public User(string username, string password)
        {
            Records = new ObservableCollection<Account>();
            this.username = Name = username;
            this.password = password;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   username == user.username &&
                   password == user.password;
        }

        public override int GetHashCode()
        {
            var hashCode = 1710835385;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            return hashCode;
        }

        private void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

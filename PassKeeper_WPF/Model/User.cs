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

        private ObservableCollection<IRecord> records;

        public ObservableCollection<IRecord> Records
        {
            get => records;
            set
            {
                records = value;
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

        public string Name
        {
            get => name;
            set
            {
                name = value;
                Notify();
            }
        }

        public User(string username, string password)
        {
            Records = new ObservableCollection<IRecord>();
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

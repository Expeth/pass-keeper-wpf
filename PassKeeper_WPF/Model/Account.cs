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

        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

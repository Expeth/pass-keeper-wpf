using PassKeeper_BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF.Model
{
    public class WPF_Record : IRecord, INotifyPropertyChanged
    {
        private string _password;
        private string _title;
        private string _websiteName;

        public int Id { get; set; }
        public string Note { get; set; }
        public string Username { get; set; }
        public int? Category { get; set; }
        public bool? IsFavorite { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? UserId { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
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
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
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

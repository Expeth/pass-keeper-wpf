using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PassKeeper_WPF
{
    public class MainWindowViewModel
    {
        public User User { get; set; }

        public MainWindowViewModel(User user)
        {
            User = user;
            AddRecordCommand = new RelayCommand(AddRecordMethod);
        }

        private void AddRecordMethod(object obj)
        {
            User.Records.Add(new Account("title", "", "", "", ""));
        }

        #region Commands
        public ICommand AddRecordCommand { get; set; }
        #endregion
    }
}

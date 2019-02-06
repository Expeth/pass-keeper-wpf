using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF
{
    public interface IRecord
    {
        string Title { get; set; }
        string Note { get; set; }
        string WebsiteName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        int Category { get; set; }
        bool IsFavorite { get; set; }
        DateTime CreationDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Infrastructure
{
    public interface IRecord
    {
        int Id { get; set; }
        string Title { get; set; }
        string Note { get; set; }
        string WebsiteName { get; set; }
        string Username { get; set; }
        int? Category { get; set; }
        bool? IsFavorite { get; set; }
        DateTime? CreationDate { get; set; }
        int? UserId { get; set; }
        string Password { get; set; }
    }
}

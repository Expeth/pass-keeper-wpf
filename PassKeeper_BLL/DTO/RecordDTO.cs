using PassKeeper_BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.DTO
{
    public class RecordDTO : IRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string WebsiteName { get; set; }
        public string Username { get; set; }
        public int? Category { get; set; }
        public bool? IsFavorite { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? UserId { get; set; }
        public string Password { get; set; }
    }
}

namespace PassKeeper_DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Record")]
    public partial class Record
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        [StringLength(255)]
        public string WebsiteName { get; set; }

        [StringLength(255)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public int? Category { get; set; }

        public bool? IsFavorite { get; set; }

        public DateTime? CreationDate { get; set; }

        public virtual User User { get; set; }
    }
}

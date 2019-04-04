namespace PassKeeper_DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PassKeeperDB : DbContext
    {
        public PassKeeperDB()
            : base("name=PassKeeperDB")
        {
        }

        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

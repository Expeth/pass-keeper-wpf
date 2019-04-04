using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private PassKeeperDB _context = new PassKeeperDB();

        public void CreateOrUpdate(User obj)
        {
            _context.User.AddOrUpdate(obj);
        }

        public void Delete(User obj)
        {
            _context.User.Remove(obj);
        }

        public User Get(int Id)
        {
            return _context.User.Find(Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User;
        }

        ~UserRepository()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

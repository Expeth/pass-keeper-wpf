using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_DAL.Repositories
{
    public class RecordRepository : IRepository<Record>
    {
        private PassKeeperDB _context = new PassKeeperDB();

        public void CreateOrUpdate(Record obj)
        {
            _context.Record.AddOrUpdate(obj);
        }

        public void Delete(Record obj)
        {
            _context.Record.Remove(obj);
        }

        public Record Get(int Id)
        {
            return _context.Record.Find(Id);
        }

        public IEnumerable<Record> GetAll()
        {
            return _context.Record;
        }

        ~RecordRepository()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

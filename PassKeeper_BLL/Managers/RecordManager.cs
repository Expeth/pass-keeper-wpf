using PassKeeper_BLL.DTO;
using PassKeeper_BLL.Infrastructure;
using PassKeeper_DAL;
using PassKeeper_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_BLL.Managers
{
    public class RecordManager : IManager<IRecord>
    {
        private IRepository<Record> _repository;
        private IFactory<IRecord> _factory;

        public RecordManager(IRepository<Record> repository, IFactory<IRecord> factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public void CreateOrUpdate(IRecord obj)
        {
            if (obj == null)
                return;

            _repository.CreateOrUpdate(new Record
            {
                Id = obj.Id,
                Category = obj.Category,
                CreationDate = obj.CreationDate,
                IsFavorite = obj.IsFavorite,
                Note = obj.Note,
                Password = obj.Password,
                Title = obj.Title,
                UserId = obj.UserId,
                Username = obj.Username,
                WebsiteName = obj.WebsiteName
            });
            _repository.Save();
        }

        public void Delete(IRecord obj)
        {
            _repository.Delete(_repository.Get(obj.Id));
            _repository.Save();
        }

        public IEnumerable<IRecord> GetAll()
        {
            return _repository.GetAll().Select(x => 
            {
                var instance = _factory.GetInstance();
                    instance.Id = x.Id;
                    instance.Category = x.Category;
                    instance.CreationDate = x.CreationDate;
                    instance.IsFavorite = x.IsFavorite;
                    instance.Note = x.Note;
                    instance.Password = x.Password;
                    instance.Title = x.Title;
                    instance.UserId = x.UserId;
                    instance.Username = x.Username;
                    instance.WebsiteName = x.WebsiteName;
                return instance;
            });
        }

        public void Save()
        {
            _repository.Save();
        }
    }
}

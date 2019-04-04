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
    public class UserManager : IManager<IUser>
    {
        private IRepository<User> _repository;
        private IFactory<IUser> _userFactory;
        private IFactory<IRecord> _recordFactory;

        public UserManager(IRepository<User> repository, IFactory<IUser> userFactory, IFactory<IRecord> recordFactory)
        {
            _repository = repository;
            _userFactory = userFactory;
            _recordFactory = recordFactory;
        }

        public void CreateOrUpdate(IUser obj)
        {
            _repository.CreateOrUpdate(new User
            {
                Id = obj.Id,
                FullName = obj.Name,
                Password = obj.Password,
                Username = obj.Username,
                Record = obj.Records?.Select(x => new Record
                {
                    Id = x.Id,
                    Category = x.Category,
                    CreationDate = x.CreationDate,
                    IsFavorite = x.IsFavorite,
                    Note = x.Note,
                    Password = x.Password,
                    Title = x.Title,
                    UserId = x.UserId,
                    Username = x.Username,
                    WebsiteName = x.WebsiteName
                }).ToList()
            });
            _repository.Save();
        }

        public void Delete(IUser obj)
        {
            _repository.Delete(_repository.Get(obj.Id));
            _repository.Save();
        }

        public IEnumerable<IUser> GetAll()
        {
            return _repository.GetAll()?.Select(x => 
            {
                var user = _userFactory.GetInstance();

                user.Id = x.Id;
                user.Name = x.FullName;
                user.Username = x.Username;
                user.Password = x.Password;
                user.Records =
                x.Record?.Select(y =>
                {
                    var record = _recordFactory.GetInstance();

                    record.Id = y.Id;
                    record.Category = y.Category;
                    record.CreationDate = y.CreationDate;
                    record.IsFavorite = y.IsFavorite;
                    record.Note = y.Note;
                    record.Password = y.Password;
                    record.Title = y.Title;
                    record.UserId = y.UserId;
                    record.Username = y.Username;
                    record.WebsiteName = y.WebsiteName;

                    return record;
                }) ?? new List<RecordDTO>();

                return user;
            });
        }

        public void Save()
        {
            _repository.Save();
        }
    }
}

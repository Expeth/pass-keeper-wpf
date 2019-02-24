using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;

namespace PassKeeper_WPF
{
    public class FileRepository : IRepository
    {
        private string _fileName;
        private IList<User> _items;

        public FileRepository()
        {
            this._fileName = "users.json";
            Load();
        }

        public void Create(User item)
        {
            _items.Add(item);
            Save();
        }

        public void Delete(User item)
        {
            Save();
        }

        public User GetById(int id)
        {
            return _items[id];
        }

        public IEnumerable<User> GetAll()
        {
            return _items;
        }

        private void Load()
        {
            if (!File.Exists(_fileName) || File.ReadAllText(_fileName).Length == 0)
            {
                _items = new List<User>();
                return;
            }
            string json = File.ReadAllText("users.json");
            _items = (JsonConvert.DeserializeObject<List<User>>(json));
        }

        private void Save()
        {
            string json;
            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                json = JsonConvert.SerializeObject(_items);
                sw.Write(json);
            }
        }

        public void Update(User item)
        {
            Save();
        }
    }
}

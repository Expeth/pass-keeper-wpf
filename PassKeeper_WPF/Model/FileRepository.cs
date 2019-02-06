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
        private string fileName;
        private IList<User> items;

        public FileRepository()
        {
            this.fileName = "users.json";
            Load();
        }

        public void Create(User item)
        {
            items.Add(item);
            Save();
        }

        public void Delete(User item)
        {
            Save();
        }

        public User GetById(int id)
        {
            return items[id];
        }

        public IEnumerable<User> GetAll()
        {
            return items;
        }

        private void Load()
        {
            if (!File.Exists(fileName) || File.ReadAllText(fileName).Length == 0)
            {
                items = new List<User>();
                return;
            }
            string json = File.ReadAllText("users.json");
            items = (JsonConvert.DeserializeObject<List<User>>(json));
        }

        private void Save()
        {
            string json;
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                json = JsonConvert.SerializeObject(items);
                sw.Write(json);
            }
        }

        public void Update(User item)
        {
            Save();
        }
    }
}

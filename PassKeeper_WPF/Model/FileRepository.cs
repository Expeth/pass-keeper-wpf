using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PassKeeper_WPF
{
    public class FileRepository<T> : IRepository<T>
                                     where T : class
    {
        private string fileName;
        private IList<T> items;

        public FileRepository()
        {
            this.fileName = "users.bin";
            Load();
        }

        public void Create(T item)
        {
            items.Add(item);
            Save();
        }

        public void Delete(T item)
        {
            items.Remove(item);
            Save();
        }

        public T GetById(int id)
        {
            return items[id];
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        private void Load()
        {
            BinaryFormatter bn = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    items = (List<T>)bn.Deserialize(fs);
                }
                catch (Exception)
                {
                    items = new List<T>();
                }
            }
        }

        private void Save()
        {
            BinaryFormatter bn = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                bn.Serialize(fs, items);
            }
        }

        public void Update(T item)
        {
            Save();
        }
    }
}

using PassKeeper_BLL.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassKeeper_WPF.Infrastructure
{
    public class NotifyCollection<T> : ObservableCollection<T>
    {
        private IManager<T> _manager;

        public NotifyCollection(IManager<T> manager)
        {
            _manager = manager;
            foreach (var i in _manager.GetAll())
            {
                base.Add(i);
            }
        }

        public new void Remove(T obj)
        {
            base.Remove(obj);
            _manager.Delete(obj);
        }

        public void Update(T obj)
        {
            _manager.CreateOrUpdate(obj);
            _manager.Save();
        }

        public void UpdateCollection()
        {
            base.Clear();
            foreach (var i in _manager.GetAll())
            {
                base.Add(i);
            }
        }

        public new void Add(T obj)
        {
            base.Add(obj);
            _manager.CreateOrUpdate(obj);
        }
    }
}

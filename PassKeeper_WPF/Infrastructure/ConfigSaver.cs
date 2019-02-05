using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PassKeeper_WPF
{
    public class ConfigSaver
    {
        private Config config;
        public Config Config
        {
            get => config;
            set
            {
                config = value;
                Save();
            }
        }
        public string fileName;

        public ConfigSaver(string path)
        {
            fileName = path;
            Load();
        }

        private void Load()
        {
            if (!File.Exists(fileName))
            {
                config = new Config();
                return;
            }

            JsonSerializer js = new JsonSerializer();
            using (var sr = new StreamReader(fileName))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    config = js.Deserialize<Config>(jr);
                }
            }
        }

        public void Save()
        {
            JsonSerializer js = new JsonSerializer();
            using (var sw = new StreamWriter(fileName))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    js.Serialize(jw, config);
                }
            }
        }
    }
}

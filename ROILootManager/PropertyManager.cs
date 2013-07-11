using System;
using System.Collections.Generic;
using System.IO;

namespace ROILootManager {
    class PropertyManager {
        public static PropertyManager singleton;

        public static string propFileName = "settings.ini";

        public static string LAST_TIER_SELECTED = "lastTierSelected";

        public static string INCLUDE_ROTS = "includeRots";

        private Dictionary<string, string> props;

        public PropertyManager() {
            FileStream file = File.Open(propFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            props = new Dictionary<string, string>();

            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream) {
                string[] line = reader.ReadLine().Split('=');
                if (line.Length >= 2) {
                    props.Add(line[0].Trim(), line[1].Trim());

                }
            }
            reader.Close();
            file.Close();

        }

        public string getProperty(string name) {
            if (props.ContainsKey(name))
                return props[name];
            else
                return null;
        }

        public void setProperty(string key, string value) {
            props[key] = value;
        }

        public void close() {
            if (File.Exists(propFileName)) {
                File.Delete(propFileName);
            }

            FileStream file = File.Open(propFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter writer = new StreamWriter(file);

            foreach (KeyValuePair<string, string> pair in props) {
                writer.WriteLine(String.Format("{0}={1}", pair.Key, pair.Value));
            }

            writer.Close();
            file.Close();
        }

        public static PropertyManager getManager() {
            if (singleton == null) {
                singleton = new PropertyManager();
            }

            return singleton;
        }

    }
}

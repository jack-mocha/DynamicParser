using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class Configuration
    {
        // UnknownKeyException is checked when a key is accessed.
        private readonly IDictionary<string, dynamic> config;
        public Configuration(IDictionary<string, dynamic> config)
        {
            this.config = config;
        }

        public dynamic this[string key]
        {
            get
            {
                if (!config.ContainsKey(key))
                    throw new UnknownKeyException();

                return config[key];
            }
        }
    }
}

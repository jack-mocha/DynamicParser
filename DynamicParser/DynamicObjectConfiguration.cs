using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class DynamicObjectConfiguration : DynamicObject
    {
        private readonly Dictionary<string, dynamic> properties = new Dictionary<string, dynamic>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string key = binder.Name;
            if (properties.ContainsKey(key))
            {
                result = properties[key];
                return true;
            }

            throw new UnknownKeyException();
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            string key = binder.Name;
            properties[key] = value;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            var key = indexes[0].ToString();
            return properties.TryGetValue(key, out result);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            var key = indexes[0].ToString();

            if (properties.ContainsKey(key))
                properties[key] = value;
            else
                properties.Add(key, value);
            return true;
        }

        public bool ContainsKey(string key)
        {
            return properties.ContainsKey(key);
        }
    }
}

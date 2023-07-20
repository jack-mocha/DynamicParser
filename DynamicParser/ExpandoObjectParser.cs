using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class ExpandoObjectParser : IParser
    {
        // ExpandoObject allows us to use parsed.FirstName when consuming Parse();
        // Since we are returning dynamic, the type of the returned object is evaluated at run time.
        // Thus the object returned is ExpandoObject instead of IDictionary.
        // However, in this implementation, there is no place we can check for UnknownKeyException, when the key is evaluated.
        private IDictionary<string, dynamic> parsed;
        public dynamic Parse(string configuration)
        {
            this.parsed = new ExpandoObject() as IDictionary<string, dynamic>;
            var lines = configuration.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var pair = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

                var key = pair[0].Trim();
                var value = pair[1].Trim().TrimEnd(';').Trim();

                if (!IsValidKey(key))
                    throw new InvalidKeyException();

                if (Boolean.TryParse(value, out bool boolValue))
                    parsed[key] = boolValue;
                else if (Int32.TryParse(value, out int intValue))
                    parsed[key] = intValue;
                else
                    parsed[key] = value;
            }

            return parsed;
        }

        private bool IsValidKey(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new EmptyKeyException();

            if (parsed.ContainsKey(key))
                throw new DuplicateKeyException();

            if (!Char.IsLetter(key[0]))
                return false;

            foreach (var c in key)
            {
                if (!Char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class DictionaryParser : IParser
    {
        // The parsed data can be accessed through parsed["FirstName"], but not parsed.FirstName.
        private IDictionary<string, dynamic> parsed;
        public dynamic Parse(string configuration)
        {
            this.parsed = new Dictionary<string, dynamic>();
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

            return new Configuration(parsed);
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

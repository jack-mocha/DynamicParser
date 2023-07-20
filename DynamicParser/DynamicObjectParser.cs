using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class DynamicObjectParser : IParser
    {
        private readonly dynamic parsedConfig = new DynamicObjectConfiguration();
        public dynamic Parse(string configuration)
        {
            // Split the configuration into lines
            string[] lines = configuration.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split each line into key-value pairs
                string[] parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    throw new FormatException("Invalid configuration format.");

                string key = parts[0].Trim();
                var value = parts[1].Trim().TrimEnd(';').Trim();

                // Check if the key is empty
                if (string.IsNullOrEmpty(key))
                    throw new EmptyKeyException();

                // Validate the key format
                if (!IsValidKeyFormat(key))
                    throw new InvalidKeyException();

                // Parse boolean values
                if (bool.TryParse(value, out bool boolValue))
                {
                    parsedConfig[key] = boolValue;
                }
                // Parse integer values
                else if (int.TryParse(value, out int intValue))
                {
                    parsedConfig[key] = intValue;
                }
                // Treat everything else as a string
                else
                {
                    parsedConfig[key] = value;
                }
            }
            return parsedConfig;
        }

        private bool IsValidKeyFormat(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new EmptyKeyException();

            if (parsedConfig.ContainsKey(key))
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

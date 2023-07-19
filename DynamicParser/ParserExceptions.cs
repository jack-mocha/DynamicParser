using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public class UnknownKeyException : Exception
    {
        public UnknownKeyException() : base("Unknown key.") { }
    }

    public class EmptyKeyException : Exception
    {
        public EmptyKeyException() : base("Empty key.") { }
    }

    public class InvalidKeyException : Exception
    {
        public InvalidKeyException() : base("Invalid key format.") { }
    }

    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException() : base("Duplicate key.") { }
    }
}

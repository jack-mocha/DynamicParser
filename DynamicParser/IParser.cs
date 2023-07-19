using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicParser
{
    public interface IParser
    {
        dynamic Parse(string configuration);
    }
}

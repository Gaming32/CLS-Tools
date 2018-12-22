using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLSTools.CSInterpreter
{
    class Class
    {
        public Dictionary<string, Method> Methods { get; set; }
        public Dictionary<string, Property> Properties { get; set; }

        Class() { }
    }
}

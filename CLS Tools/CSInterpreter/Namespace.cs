using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLSTools.CSInterpreter
{
    class Namespace
    {
        public Dictionary<string, Namespace> Namespaces { get; set; }
        public Dictionary<string, Class> Classes { get; set; }

        Namespace() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBed.Model
{
    public class Method
    {
        public string Signature { get; set; }
        public List<string> Body { get; set; }

        public Method()
        {
            Body = new List<string>();
        }
    }
}

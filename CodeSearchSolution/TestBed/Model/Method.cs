using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBed.Model
{
    public class Method
    {
        public string ReturnType { get; set; }
        public string MethodName { get; set; }
        public string Parameters { get; set; }
        public string Signature { get; set; }
        public List<string> Body { get; set; }
        public string ClassName { get; set; }
        public string PackageName { get; set; }

        public Method()
        {
            Body = new List<string>();
            ReturnType = "";
            MethodName = "";
            Parameters = "";
            Signature = "";
        }
    }
}

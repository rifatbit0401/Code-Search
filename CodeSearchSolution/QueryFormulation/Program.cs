using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFormulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var booleanQueryGenerator = new BooleanQueryGenerator();
            booleanQueryGenerator.GetBooleanQuery("hello", "(str1 AND str2 OR (str3 AND str4))");
            Console.ReadLine();
        }
    }
}

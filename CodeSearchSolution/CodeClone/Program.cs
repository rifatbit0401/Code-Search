using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed;

namespace CodeClone
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new DirectoryBasedMethodParser();
            var methods = parser.ParseAllMethods(@"E:\BSSE Program\3rd semister BIT program\OOP-2\workspace\Calculator2\src");
            var codeCloneDetector = new GenericModelConstructor();
            var models = new List<List<string>>();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);
                var model = codeCloneDetector.CreateModel(method.Body);
                models.Add(model);
                Console.WriteLine("***********************");
            }

                
            

            Console.ReadLine();
        }
    }
}

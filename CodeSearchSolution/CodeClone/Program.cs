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
            var methods = parser.ParseAllMethods(@"E:\BSSE Program\3rd semister BIT program\OOP-2\workspace\");
            var codeCloneDetector = new GenericModelConstructor();
            var models = new List<List<string>>();
            foreach (var method in methods)
            {
                // Console.WriteLine(method.Signature);
                var model = codeCloneDetector.CreateModel(method.Body);
                models.Add(model);
                // Console.WriteLine("***********************");
            }
            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);
                var currentModel = codeCloneDetector.CreateModel(method.Body);
                foreach (var model in models )
                {
                    if(codeCloneDetector.IsSimilar(currentModel.ToArray(), model.ToArray()))
                        Console.WriteLine("Similar to {0}", models.IndexOf(model));
                }
                Console.WriteLine("***********************");

            }

            /*var codePreProcessor = new CodePreProcessor();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);

                foreach (var line in codePreProcessor.PreProcess(method.Body))
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine("***********************");

            }*/

            Console.ReadLine();
        }
    }
}

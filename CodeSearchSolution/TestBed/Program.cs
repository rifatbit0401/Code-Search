using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryBasedMethodParser = new DirectoryBasedMethodParser();
            var methods = directoryBasedMethodParser.ParseAllMethods(@"I:\SF110-20130704-src\SF110-20130704-src\1_tullibee\src\main\java\com\ib\client");
            /*var writer = new StreamWriter(@"I:\Arafat -pc\NTM 16\test.txt");

            foreach (var method in methods)
            {
                writer.WriteLine(method.Signature);
                foreach (var line in method.Body)
                {
                    writer.WriteLine(line);
                }

                writer.WriteLine("###################################################");
            }

            writer.Close();*/

            var methodUtility = new MethodUtility();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);
                var m = methodUtility.ConstructMethod(method.Signature, method.Body);
                Console.WriteLine("ReturnType:{0}, Name:{1}, Parameters:{2}", m.ReturnType, m.MethodName, m.Parameters);
            }
            Console.ReadLine();
        }
    }
}

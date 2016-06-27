using Newtonsoft.Json;
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
            /*    //JObject jObject = Json (File.ReadAllText("E:\\MSSE Program\\thesis dev\\methods.json"));
             //   var x = JsonConvert.DeserializeObject<List<MethodModel>>(File.ReadAllText("E:\\MSSE Program\\thesis dev\\methods.json"));*/

            var directoryBasedMethodParser = new DirectoryBasedMethodParser();
            var methods = directoryBasedMethodParser.ParseAllMethods(@"E:\MSSE Program\thesis dev\methods.json");
            var y = methods.Where(m => !m.MethodName.Contains("est") && !m.MethodName.Contains("init")).ToList();
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

            /*var methodUtility = new MethodUtility();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);
                var m = methodUtility.ConstructMethod(method.Signature, method.Body);
                Console.WriteLine("ReturnType:{0}, Name:{1}, Parameters:{2}", m.ReturnType, m.MethodName, m.Parameters);
            }*/
            Console.ReadLine();
        }
    }
}

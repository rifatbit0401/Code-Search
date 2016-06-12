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
           var methods = directoryBasedMethodParser.ParseAllMethods(@"F:\Ananda DU\android projects");
           var writer = new StreamWriter(@"I:\Arafat -pc\NTM 16\test.txt");
            
            foreach (var method in methods)
            {
                writer.WriteLine(method.Signature);
                foreach (var line in method.Body)
                {
                    writer.WriteLine(line);
                }

                writer.WriteLine("###################################################");
            }

            writer.Close();
        }
    }
}

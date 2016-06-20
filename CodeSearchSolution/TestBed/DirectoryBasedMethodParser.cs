using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed.Model;

namespace TestBed
{
    public class DirectoryBasedMethodParser
    {
        private MethodParser _methodParser;

        public DirectoryBasedMethodParser()
        {
            _methodParser = new MethodParser();
        }

        public List<Method> ParseAllMethods(string directoryPath)
        {
            var methods = new List<Method>();
            Browse(new DirectoryInfo(directoryPath), methods);
            return methods;
        }


        private void Browse(DirectoryInfo directory, List<Method> methods)
        {
            foreach (var javaFile in directory.GetFiles("*.java").ToList())
            {
               // Console.WriteLine("Prcessing..{0}",javaFile.FullName);
                methods.AddRange(_methodParser.GetMethods(javaFile.FullName));
            }

            foreach (var directoryInfo in directory.GetDirectories())
            {
                Browse(directoryInfo, methods);
            }
        }
    }
}

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
        private IMethodParser _methodParser;

        public DirectoryBasedMethodParser()
        {
            _methodParser = new JsonBasedMethodParser();
        }

        //for json only
        public List<Method> ParseAllMethods(string directoryPath)
        {
            var methods = new List<Method>();
            methods = _methodParser.GetMethods(directoryPath);
            return methods;
        }

        //uncomment if json file does not used for method parsing
        /*public List<Method> ParseAllMethods(string directoryPath)
        {
            var methods = new List<Method>();
            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                Browse(new DirectoryInfo(directory), methods);
            }
            //  Browse(new DirectoryInfo(directoryPath), methods);
            return methods;
        }*/

        public List<Method> ParseAllMethodsUsingParallelProcessing(string directoryPath)
        {
            var methods = new List<Method>();
            Browse(new DirectoryInfo(directoryPath), methods);
            return methods;
        }


        private void Browse(DirectoryInfo directory, List<Method> methods)
        {

            foreach (var javaFile in directory.GetFiles("*.java").ToList())
            {
                if (javaFile.FullName.Contains("test"))
                    continue;
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

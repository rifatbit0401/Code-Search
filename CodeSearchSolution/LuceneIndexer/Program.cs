using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed;

namespace LuceneIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var luceneService = new LuceneService();
            var methodParser = new DirectoryBasedMethodParser();
            var utility = new Utility();
            var projectDir = @"F:\Ananda DU\android projects\Test Smell Finder";
            luceneService.BuildIndex(utility.RefineMethodsUsingCamelCase(methodParser.ParseAllMethods(projectDir)));
*/
            new WordNetService().ConstructSynDictionary();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneIndexer;
using QueryFormulation;
using TestBed;
using TestBed.Model;

namespace KBCS
{
    public abstract class ATechnique
    {
        public BooleanQueryGenerator _booleanQueryGenerator;
        public LuceneService _luceneService;
        public const string DataPath = @"I:\SF110-20130704-src\SF110-20130704-src";

        public ATechnique()
        {
            _booleanQueryGenerator = new BooleanQueryGenerator();
            _luceneService = new LuceneService();
        }

        public void Index()
        {
            var directoryBasedMethodParser = new DirectoryBasedMethodParser();
            var methods = directoryBasedMethodParser.ParseAllMethods(DataPath).ToArray();

            Console.WriteLine("Parsing complete");
            _luceneService.BuildIndex(methods.ToList());
            Console.WriteLine("indexing done");
        }

        //public abstract List<Method> Search(string queryStr);
    }
}

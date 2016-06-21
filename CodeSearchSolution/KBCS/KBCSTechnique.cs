using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneIndexer;
using QueryFormulation;
using TestBed;
using TestBed.Model;
using Version = Lucene.Net.Util.Version;

namespace KBCS
{
    public class KbcsTechnique
    {
        private const string DataPath = @"I:\SF110-20130704-src\SF110-20130704-src";
        private readonly BooleanQueryGenerator _booleanQueryGenerator;
        private readonly LuceneService _luceneService;

        public KbcsTechnique()
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

        public List<Method> Search(string queryStr)
        {
            var methods = _luceneService.LuceneSearch(_booleanQueryGenerator.GetBooleanQuery("whole_method", queryStr));//LuceneSearch(_booleanQueryGenerator.GetBooleanQuery("whole_method", queryStr));
            return methods;
        }

    }
}

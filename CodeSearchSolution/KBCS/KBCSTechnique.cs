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
using TestBed;
using TestBed.Model;
using Version = Lucene.Net.Util.Version;

namespace KBCS
{
    public class KbcsTechnique
    {
        private string _indexPath = @"E:\MSSE Program\thesis dev\techniques\KBCS\lucene";
        private FSDirectory _luceneIndexDirectory;
        private IndexWriter _indexWriter;
        private string _dataPath = @"I:\SF110-20130704-src\SF110-20130704-src";

        public KbcsTechnique()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            _luceneIndexDirectory = FSDirectory.Open(new DirectoryInfo(_indexPath));
            _indexWriter = new IndexWriter(_luceneIndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

        }

        public void Index()
        {
            var luceneService = new LuceneService();
            var directoryBasedMethodParser = new DirectoryBasedMethodParser();
            var methods = directoryBasedMethodParser.ParseAllMethods(_dataPath).ToArray();

            for (int i = 0; i < methods.Length; i++)
            {
                methods[i].Body.Insert(0, methods[i].Signature);
            }

            Console.WriteLine("Parsing complete");
            luceneService.BuildIndex(methods.ToList());
            Console.WriteLine("indexing done");
        }

        public List<Method> Search(string queryStr)
        {
            var methods = new List<Method>();

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            _luceneIndexDirectory = FSDirectory.Open(new DirectoryInfo(_indexPath));
            if (IndexWriter.IsLocked(_luceneIndexDirectory))
                IndexWriter.Unlock(_luceneIndexDirectory);

            var locFilePath = Path.Combine(_indexPath, "write.lock");
            if (File.Exists(locFilePath))
            {
             //   File.Delete(locFilePath);
            }
            //_indexWriter = new IndexWriter(_luceneIndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            var indexSearcher = new IndexSearcher(_luceneIndexDirectory);
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "signature", new StandardAnalyzer(Version.LUCENE_30));
            var query = queryParser.Parse(queryStr);
            var hits = indexSearcher.Search(query, 1000000);

            IEnumerable<Document> docs = hits.ScoreDocs.Select(hit => indexSearcher.Doc(hit.Doc));
            //var x = docs.First().Get("id");

            foreach (var document in docs)
            {
                var method = new Method {Signature = document.Get("signature")};
                methods.Add(method);
            }

            return methods;
        }

    }
}

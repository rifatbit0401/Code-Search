using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Documents;
using TestBed.Model;

namespace LuceneIndexer
{
    public class LuceneService
    {
        private string _indexPath = @"E:\MSSE Program\thesis dev\techniques\KBCS\lucene4";
        private FSDirectory _luceneIndexDirectory;
        private IndexWriter _indexWriter;

        public LuceneService()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            _luceneIndexDirectory = FSDirectory.Open(new DirectoryInfo(_indexPath));
            _indexWriter = new IndexWriter(_luceneIndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public void BuildIndex(List<Method> methods)
        {

            int i = 0;
            foreach (var method in methods)
            {
                var document = new Document();
                var idField = new Field("id", i.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                i++;
                var methodSignatureField = new Field("signature", method.Signature, Field.Store.YES, Field.Index.ANALYZED);
                string body = "";
                foreach (var line in method.Body)
                {
                    body += line+"\n";
                }
                var methodBodyField = new Field("body", body, Field.Store.YES, Field.Index.ANALYZED);
                var methodNameField = new Field("name", method.MethodName, Field.Store.YES, Field.Index.ANALYZED);
                var methodReturnTypeField = new Field("return", method.ReturnType, Field.Store.YES, Field.Index.ANALYZED);
                var methodParameterField = new Field("parameter", method.Parameters, Field.Store.YES,
                                                     Field.Index.ANALYZED);
                var wholeMethodField = new Field("whole_method", method.Signature + body, Field.Store.YES,
                                            Field.Index.ANALYZED);
                document.Add(idField);
                document.Add(methodSignatureField);
                document.Add(methodBodyField);
                document.Add(methodReturnTypeField);
                document.Add(methodNameField);
                document.Add(methodParameterField);
                document.Add(wholeMethodField);
                _indexWriter.AddDocument(document);
            }

            _indexWriter.Optimize();
            _indexWriter.Close();
            _luceneIndexDirectory.Close();
        }

        public List<Method> LuceneSearch(string queryStr)
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
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "signature",
                                              new StandardAnalyzer(Version.LUCENE_30));
            var query = queryParser.Parse(queryStr);
            var hits = indexSearcher.Search(query, 1000000);

            IEnumerable<Document> docs = hits.ScoreDocs.Select(hit => indexSearcher.Doc(hit.Doc));
            //var x = docs.First().Get("id");

            foreach (var document in docs)
            {
                var method = new Method
                {
                    Signature = document.Get("signature"),
                    MethodName = document.Get("name"),
                    ReturnType = document.Get("return"),
                    Parameters = "parameter",
                    Body = document.Get("body").Split('\n').ToList()
                };
                methods.Add(method);
            }
            return methods;
        }

    }
}

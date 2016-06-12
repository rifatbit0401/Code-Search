using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Documents;
using TestBed.Model;

namespace LuceneIndexer
{
    public class LuceneService
    {
        private string _indexPath = @"E:\MSSE Program\thesis dev\lucene";
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
                    body += line;
                }
                var methodBodyField = new Field("body", body, Field.Store.YES, Field.Index.ANALYZED);
                document.Add(idField);
                document.Add(methodSignatureField);
                document.Add(methodBodyField);
                _indexWriter.AddDocument(document);
            }

            _indexWriter.Optimize();
            _indexWriter.Close();
            _luceneIndexDirectory.Close();
        }

    }
}

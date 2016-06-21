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
    public class KbcsTechnique : ATechnique
    {

        public List<Method> Search(string queryStr)
        {
            var methods = _luceneService.LuceneSearch(_booleanQueryGenerator.GetBooleanQuery("whole_method", queryStr));//LuceneSearch(_booleanQueryGenerator.GetBooleanQuery("whole_method", queryStr));
            return methods;
        }

    }
}

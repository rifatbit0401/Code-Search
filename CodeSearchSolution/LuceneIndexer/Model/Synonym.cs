using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneIndexer.Model
{
    public class Synonym
    {
        public String Word { get; set; }
        public List<string> Syn { get; set; }

        public Synonym()
        {
            Syn = new List<string>();
        }
    }

}

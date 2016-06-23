using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LuceneIndexer;
using LuceneIndexer.Model;

namespace QueryFormulation
{
    public class BooleanQueryGenerator
    {
        private WordNetService _wordNetService = new WordNetService();
        private List<Synonym> _dictionary;

        public BooleanQueryGenerator()
        {
            _dictionary = _wordNetService.ConstructSynDictionary();
            
        }


        public string GetBooleanQueryForLucene(string fieldNameWithOutClone, string queryString)
        {
            string luceneBooleanQuery = queryString;
            var matches = Regex.Matches(queryString, @"\w+");

            foreach (var match in matches)
            {
                if (match.ToString().Equals("AND") || match.ToString().Equals("OR"))
                {
                    continue;
                }
                luceneBooleanQuery = Regex.Replace(luceneBooleanQuery, "\\b" + match.ToString() + "\\b",
                                                   fieldNameWithOutClone + ":" + match.ToString());

            }
            return luceneBooleanQuery;
        }

        public string GetExpandedQuery(string queryStr)
        {
            string expandedQueryStr = queryStr;

            foreach (var term in Regex.Matches(queryStr, @"\w+"))
            {
                if(term.ToString()=="OR" || term.ToString()=="AND")
                    continue;
                var expandedTerm = GetExpadedQueryForTerm(_dictionary, term.ToString());
                expandedQueryStr = Regex.Replace(expandedQueryStr, "\\b" + term.ToString() + "\\b", expandedTerm);
            }
            return expandedQueryStr;
        }

        private string GetExpadedQueryForTerm(List<Synonym> dictionary, string term)
        {
            Synonym synonym = dictionary.FirstOrDefault(s => s.Word.Equals(term));
            var vocubularyStr = "(" + term;
            if (synonym != null)
            {
                foreach (var syn in synonym.Syn)
                {
                    if (syn == "")
                        continue;
                    vocubularyStr += " OR " + syn;
                }
            }
            vocubularyStr += ")";
            return vocubularyStr;
        }
    }
}

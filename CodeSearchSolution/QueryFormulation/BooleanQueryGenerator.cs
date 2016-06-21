using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QueryFormulation
{
    public class BooleanQueryGenerator
    {
        public string GetBooleanQuery(string fieldNamewWithOutClone, string queryString)
        {
            string luceneBooleanQuery = queryString;
            var matches = Regex.Matches(queryString, @"\w+");

            foreach (var match in matches)
            {
                if (match.ToString().Equals("AND") || match.ToString().Equals("OR"))
                {
                    continue;
                }
                luceneBooleanQuery = luceneBooleanQuery.Replace(match.ToString(), fieldNamewWithOutClone + ":" + match.ToString());

            }
            return luceneBooleanQuery;
        }
    }
}

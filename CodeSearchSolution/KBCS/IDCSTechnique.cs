using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed.Model;

namespace KBCS
{
    public class IDCSTechnique : ATechnique
    {
        public List<Method> Search(string methodNameQueryStr, string methodReturnTypeQueryStr, string methodParameterQueryStr)
        {
            var luceneBooleanQuery = "(" + _booleanQueryGenerator.GetBooleanQueryForLucene("name", methodNameQueryStr) +
                                     ") AND (" +
                                     _booleanQueryGenerator.GetBooleanQueryForLucene("return", methodReturnTypeQueryStr) +
                                     ") AND (" +
                                     _booleanQueryGenerator.GetBooleanQueryForLucene("parameter", methodParameterQueryStr) + ")";
            return _luceneService.LuceneSearch(luceneBooleanQuery);

        }

        public List<Method> SearchWithQueryExpansion(string methodNameQueryStr, string methodReturnTypeQueryStr, string methodParameterQueryStr)
        {
            var luceneExpandedBooleanQuery = "(" +
                                             _booleanQueryGenerator.GetBooleanQueryForLucene("name",
                                                                                             _booleanQueryGenerator.
                                                                                                 GetExpandedQuery(
                                                                                                     methodNameQueryStr)) +
                                             ") AND (" +
                                             _booleanQueryGenerator.GetBooleanQueryForLucene("return",
                                                                                             _booleanQueryGenerator.
                                                                                                 GetExpandedQuery(
                                                                                                     methodReturnTypeQueryStr)) +
                                             ") AND (" +
                                             _booleanQueryGenerator.GetBooleanQueryForLucene("parameter",
                                                                                             _booleanQueryGenerator.
                                                                                                 GetExpandedQuery(
                                                                                                     methodParameterQueryStr)) +
                                             ")";

            Console.WriteLine(luceneExpandedBooleanQuery);

            return _luceneService.LuceneSearch(luceneExpandedBooleanQuery);
        }
    }
}

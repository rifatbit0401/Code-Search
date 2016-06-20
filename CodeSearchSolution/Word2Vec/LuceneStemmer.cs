using System.IO;
using Lucene.Net.Analysis;
using System;
using System.Collections.Generic;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;
using Version = Lucene.Net.Util.Version;

namespace Word2Vec
{
    public class LuceneStemmer
    {
        public List<String> GetToken(String txt)
        {
            var tokens = new List<string>();
            TokenStream tokenStream = new StandardTokenizer(Version.LUCENE_30, new StringReader(txt));
            var offsetAttribute = tokenStream.GetAttribute<IOffsetAttribute>();
            var termAttribute = tokenStream.GetAttribute<ITermAttribute>();
            while (tokenStream.IncrementToken())
            {
                int startOffset = offsetAttribute.StartOffset;
                int endOffset = offsetAttribute.EndOffset;
                String term = termAttribute.Term;
                tokens.Add(term);
            }
            return tokens;
        }

        public string ConverToTokenString(List<String>tokens)
        {
            string tokenStr = "";
            var tokenArray = tokens.ToArray();
            for(int i=0;i<tokenArray.Length;i++)
            {
                if (i != 0)
                    tokenStr += " ";
                tokenStr += tokenArray[i];
            }

            return tokenStr;
        }
    }

}

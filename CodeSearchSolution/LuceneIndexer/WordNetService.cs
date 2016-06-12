using LuceneIndexer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LuceneIndexer
{
    public class WordNetService
    {
        private string _prologFilePath;
        private List<Synonym> _synonyms;
        public WordNetService()
        {
            _prologFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"resource\wn_s.pl");
            _synonyms = new List<Synonym>();
        }

        public List<Synonym> ConstructSynDictionary()
        {
            Console.WriteLine("Opening Prolog file " + _prologFilePath);
            var fis = new FileStream(_prologFilePath, FileMode.Open, FileAccess.Read);
            var br = new StreamReader(new StreamReader(fis, System.Text.Encoding.Default).BaseStream, new StreamReader(fis, System.Text.Encoding.Default).CurrentEncoding);
            String line;

            // maps a word to all the "groups" it's in
            System.Collections.IDictionary word2Nums = new System.Collections.SortedList();
            // maps a group to all the words in it
            System.Collections.IDictionary num2Words = new System.Collections.SortedList();

            // parse prolog file
            Console.WriteLine("[1/2] Parsing " + _prologFilePath);
            while ((line = br.ReadLine()) != null)
            {
                // syntax check
                if (!line.StartsWith("s("))
                {
                    Console.WriteLine("OUCH: " + line);
                    Environment.Exit(1);
                }

                // parse line
                line = line.Substring(2);
                var comma = line.IndexOf(',');
                var num = line.Substring(0, comma);
                var q1 = line.IndexOf('\'');
                line = line.Substring(q1 + 1);
                var q2 = line.IndexOf('\'');
                var word = line.Substring(0, q2).ToLower().Replace("''", "'");

                // make sure is a normal word
                if (!IsDecent(word))
                    continue; // don't store words w/ spaces

                // 1/2: word2Nums map
                // append to entry or add new one
                var lis = (System.Collections.IList)word2Nums[word];
                if (lis == null)
                {
                    lis = new List<String> { num };
                    word2Nums[word] = lis;
                }
                else
                    lis.Add(num);

                // 2/2: num2Words map
                lis = (System.Collections.IList)num2Words[num];
                if (lis == null)
                {
                    lis = new List<String> { word };
                    num2Words[num] = lis;
                }
                else
                    lis.Add(word);
            }

            // close the streams
            fis.Close();
            br.Close();

            foreach (var key in word2Nums.Keys)
            {
                var synonym = new Synonym();
                synonym.Word = key.ToString();

                foreach (var number in (System.Collections.IList)word2Nums[key])
                {
                    foreach (var word in (System.Collections.IList)num2Words[number])
                    {
                        if (!synonym.Syn.Contains(word) && !synonym.Word.Equals(word))
                            synonym.Syn.Add(word.ToString());
                    }
                }
                if (synonym.Syn.Count > 0)
                    _synonyms.Add(synonym);
            }
            return _synonyms;
        }

        private bool IsDecent(String s)
        {
            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!Char.IsLetter(s[i]))
                {
                    return false;
                }
            }
            return true;
        }


    }
}

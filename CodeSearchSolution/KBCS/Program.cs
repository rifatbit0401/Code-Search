using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneIndexer;
using TestBed;

namespace KBCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var kbcsTechnique = new KbcsTechnique();
          //  kbcsTechnique.Search("zip");

            while (true)
            {
                var queryString = Console.ReadLine();
                int id = 0;
                foreach (var method in kbcsTechnique.Search(queryString))
                {
                    Console.WriteLine("{0}:{1}",id++,method.Signature);
                }
            }
        }
    }
}

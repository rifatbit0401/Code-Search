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
            var technique = new IDCSTechnique();// new KbcsTechnique();
            // kbcsTechnique.Index();
            //  kbcsTechnique.Search("zip");

            Console.WriteLine("Enter Boolean Query:");
            while (true)
            {
                Console.Write("Name:");
                var methodNameQueryString = Console.ReadLine();
                Console.Write("Return Type:");
                var methodReturnTypeQueryString = Console.ReadLine();
                Console.Write("Parameter:");
                var methodParameterQueryString = Console.ReadLine();
                int id = 0;
                foreach (var method in technique.Search(methodNameQueryString, methodReturnTypeQueryString, methodParameterQueryString))
                {
                    Console.WriteLine("{0}:{1}", id++, method.Signature);
                }
            }
        }
    }
}

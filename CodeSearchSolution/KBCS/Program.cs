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
            //TestIDCSTechnique();
            //TestQueryExpForKBCS();

            var technique = new IDCSTechnique(); // new KbcsTechnique();
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
                foreach (
                    var method in
                        technique.SearchWithQueryExpansion(methodNameQueryString, methodReturnTypeQueryString, methodParameterQueryString))
                {
                    Console.WriteLine("{0}:{1}", id++, method.Signature);
                }
            }

        }

        private static void TestQueryExpForKBCS()
        {
            var technique = new KbcsTechnique();
            while (true)
            {
                var queryStr = Console.ReadLine();
                int id = 0;
                foreach (
                    var method in
                        technique.Search(queryStr))
                {
                    Console.WriteLine("{0}:{1}", id++, method.Signature);
                }

                int kbcs = id;

                id = 0;
                foreach (
                    var method in
                        technique.SearchWithQueryExpansion(queryStr))
                {
                    Console.WriteLine("{0}:{1}", id++, method.Signature);
                }
                Console.WriteLine("No Exp:{0}, with exp{1}", kbcs, id);
            }
        }

        private static void TestIDCSTechnique()
        {
            var technique = new IDCSTechnique(); // new KbcsTechnique();
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
                foreach (
                    var method in
                        technique.Search(methodNameQueryString, methodReturnTypeQueryString, methodParameterQueryString))
                {
                    Console.WriteLine("{0}:{1}", id++, method.Signature);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word2Vec
{
    class Program
    {
        static void Main(string[] args)
        {
            var luceneStemmer = new LuceneStemmer();
            var cloneDetector = new CloneDetector();
            // pythonScriptRunner.RunPythonScript();
            string doc1 = "int bubble_sort(int[] a){for(int i=0;i<a.length;i++){for(int j=0;j<a.length;j++){if(a[i]>a[j]) swap(a[i], a[j])}}}";
            string doc2 = "int x(int a[]){for(int i=0;i<a.length;i++){for(int j=0;j<a.length;j++){if(a[i]>a[j]) swap(a[i], a[j])}}}";
            doc1 = luceneStemmer.ConverToTokenString(luceneStemmer.GetToken(doc1));
            doc2 = luceneStemmer.ConverToTokenString(luceneStemmer.GetToken(doc2));
            Console.WriteLine("{0}, {1}", doc1, doc2);
            Console.WriteLine(cloneDetector.GetSimilarityScore(doc1, doc2));
            Console.ReadLine();
        }
    }
}

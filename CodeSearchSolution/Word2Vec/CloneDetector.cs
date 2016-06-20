using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Word2Vec.model;

namespace Word2Vec
{
    public class CloneDetector
    {
        private List<CloneModel> RunPythonScript(string doc1, string doc2)
        {
            var pythonCode = GetPythonScript(doc1, doc2);
            File.WriteAllLines(@"E:\MSSE Program\thesis dev\my.py", pythonCode);

            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"C:\Anaconda2\python.exe";
            processStartInfo.Arguments = "\"" + @"E:\MSSE Program\thesis dev\my.py" + "\"";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            var process = Process.Start(processStartInfo);
            var output = process.StandardOutput;

            var cloneModels = new List<CloneModel>();
            while (!output.EndOfStream)
            {
                string[] splitted = output.ReadLine().Split(',');
                if (splitted.Length == 1)
                    continue;

                var cloneModel = new CloneModel
                                     {
                                         Term1 = splitted[0],
                                         Term2 = splitted[1],
                                         SimilarityScore = Double.Parse(splitted[2])
                                     };
                cloneModels.Add(cloneModel);
            }

            return cloneModels;
            //  Console.WriteLine(output.ReadToEnd());
        }

        private List<String> GetPythonScript(string doc1, string doc2)
        {
            var pythonScript = new List<string>();
            pythonScript.Add("import gensim");
            pythonScript.Add("doc1 = " + GetPythonArrayOfString(doc1.Split(' ').ToArray()));
            pythonScript.Add("doc2 = " + GetPythonArrayOfString(doc2.Split(' ').ToArray()));
            pythonScript.Add("sentences = [];");
            pythonScript.Add("sentences.append(doc1)");
            pythonScript.Add("sentences.append(doc2)");
            pythonScript.Add("model = gensim.models.Word2Vec(sentences, min_count=1)");
            pythonScript.Add("for t1 in doc1:");
            pythonScript.Add(" for f1 in doc2:");
            pythonScript.Add("  print \"%s,%s,%g\\n\" %(t1,f1,model.similarity(t1,f1))");

            return pythonScript;
        }

        private String GetPythonArrayOfString(string[] str)
        {
            string pythonStr = "[";
            for (int i = 0; i < str.Length; i++)
            {
                if (i != 0)
                    pythonStr += ",";
                pythonStr += String.Format("'{0}'", str[i]);
            }
            return pythonStr + "]";
        }

        public double GetSimilarityScore(string doc1, string doc2)
        {
            var cloneModels = RunPythonScript(doc1, doc2).ToArray();
            var matchedCloneModels = new List<CloneModel>();
            cloneModels = cloneModels.OrderByDescending(m => m.SimilarityScore).ToArray();
            var dictionary = new Dictionary<string, string>();

            int count = doc1.Split(' ').Length * doc2.Split(' ').Length;
            while (count-- > 0)
            {

                try
                {
                    var cloneModel = cloneModels.Where(m => !dictionary.ContainsKey(m.Term1) & !dictionary.ContainsValue(m.Term2)).OrderByDescending(m => m.SimilarityScore).First();
                    dictionary.Add(cloneModel.Term1, cloneModel.Term2);
                    matchedCloneModels.Add(cloneModel);

                }
                catch (Exception exception)
                {

//                    Console.WriteLine("sequence contains no element");
                }

            }

            return matchedCloneModels.Average(m => m.SimilarityScore);
        }

    }
}

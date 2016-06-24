using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word2Vec
{
    public class Word2VecService
    {
        private LuceneStemmer _luceneStemmer = new LuceneStemmer();

        public void CreateWord2VecModel(List<String> sentences)
        {
            RunPythonScript(GetPythonScript(sentences));
        }

        private void RunPythonScript(List<string> pythonCode)
        {
            File.WriteAllLines(@"E:\MSSE Program\thesis dev\my.py", pythonCode);

            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"C:\Anaconda2\python.exe";
            processStartInfo.Arguments = "\"" + @"E:\MSSE Program\thesis dev\my.py" + "\"";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            var process = Process.Start(processStartInfo);
            var output = process.StandardOutput;

            while (!output.EndOfStream)
            {
                Console.WriteLine(output.ReadLine());
            }
        }

        private List<String> GetPythonScript(List<string> sentences)
        {
            var pythonScript = new List<string>();
            pythonScript.Add("import gensim");
            pythonScript.Add("sentences = [];");
            foreach (var sentence in sentences)
            {
                var tokenString = _luceneStemmer.ConverToTokenString(_luceneStemmer.GetToken(sentence));
                pythonScript.Add("sentences.append(" + GetPythonArrayOfString(tokenString.Split(' ')) + ")");
            }

            pythonScript.Add("model = gensim.models.Word2Vec(sentences, min_count=1)");
            pythonScript.Add("model.save('myModelll')");
            pythonScript.Add("new_model = gensim.models.Word2Vec.load('myModelll')");
            pythonScript.Add("print new_model.similarity('update','account')");
            pythonScript.Add("print new_model.most_similar('tick', topn=30)");
            pythonScript.Add("print 'Done....'");
           
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
    }
}

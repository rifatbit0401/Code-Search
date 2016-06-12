using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeMetricsCalculator
{
    public class CodeMetricsCalculator
    {
        public int GetNumberOfArgument(string methodSignature)
        {
            return methodSignature.Substring(methodSignature.IndexOf('(')).Split(',').Length;
        }

        public int GetNumberOfFunctionCall(List<String>methodBody)
        {
            string extractFuncRegex = @"(?<func>add|sub|div)\s*\((?<params>[^()]*)\)";
            string extractArgsRegex = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";
            foreach (var line in methodBody)
            {
                var match = Regex.Match(line.Trim(), extractFuncRegex);
                var innerArgs = Regex.Matches(match.Groups[1].Value, extractArgsRegex);
            }

            return 0;
        }


        public int GetLineOfCode(List<String> sourceCode)
        {
            int numOfLine = 0;
            foreach (var line in sourceCode)
            {
                if (IsRealCode(line.Trim()))
                    numOfLine++;
            }
            return numOfLine;
        }

        private bool IsRealCode(string trimmed)
        {
            int inComment = 0;
            if (trimmed.StartsWith("/*") && trimmed.EndsWith("*/"))
                return false;
            else if (trimmed.StartsWith("/*"))
            {
                inComment++;
                return false;
            }
            else if (trimmed.EndsWith("*/"))
            {
                inComment--;
                return false;
            }

            return
                   inComment == 0
                && !trimmed.StartsWith("//")
                && (trimmed.StartsWith("if")
                    || trimmed.StartsWith("else if")
                    || trimmed.StartsWith("using (")
                    || trimmed.StartsWith("else  if")
                    || trimmed.Contains(";")
                    || trimmed.StartsWith("public") //method signature
                    || trimmed.StartsWith("private") //method signature
                    || trimmed.StartsWith("protected") //method signature
                    );
        }

       


    }
}

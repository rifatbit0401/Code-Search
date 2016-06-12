using CodeClone;
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
        public int GetNumberOfConditionalStatements(List<String>methodBody)
        {
            int numOfConditinalStatement = 0;

            foreach (var line in methodBody)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith(CodeKeyword.IF + "(") || trimmed.StartsWith(CodeKeyword.ELSE_IF + "(") || trimmed.Equals(CodeKeyword.ELSE))
                    numOfConditinalStatement++;
            }

            return numOfConditinalStatement;
        }


        public int GetNumberOfArgument(string methodSignature)
        {
            return methodSignature.Substring(methodSignature.IndexOf('(')).Split(',').Length;
        }

        public int GetNumberOfFunctionCall(List<String> methodBody)
        {
            int numOfFunctionCall = 0;
            foreach (var line in methodBody)
            {
                string str = line;

                while (Regex.IsMatch(str, CodeKeyword.FUNC_CALL_REGEX))
                {
                    var match = Regex.Match(str, CodeKeyword.FUNC_CALL_REGEX);
                    var methodName = match.ToString().Split('(')[0];
                    str = str.Substring(str.IndexOf(methodName + "(") + methodName.Length + 1);
                    if (methodName.Equals(CodeKeyword.FOR) || methodName.Equals(CodeKeyword.WHILE) || methodName.Equals(CodeKeyword.SWITCH) ||
                        methodName.Equals(CodeKeyword.IF) || methodName.Equals(CodeKeyword.ELSE_IF) || methodName.Equals(CodeKeyword.ELSE))
                    {
                        continue;
                    }
                    numOfFunctionCall++;
                }

            }

            return numOfFunctionCall;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClone
{
    public class CodePreProcessor
    {
        private Rule _rule;
        public CodePreProcessor()
        {
            _rule = new Rule();
        }

        public List<String> PreProcess(List<String> codeLines)
        {
            var processedCodes = new List<string>();
            processedCodes = SeparateMultipleVariableDeclaration(codeLines);

            return processedCodes;
        }

        private List<String> SeparateMultipleVariableDeclaration(List<string> codeLines)
        {
            var processedCodes = new List<string>();
            foreach (var codeLine in codeLines)
            {
                if (_rule.IsDeclaration(codeLine.Trim()))
                {
                    var splittedCode = codeLine.Trim().Replace(";", "").Replace(",", " ").Split(' ');
                    string type = splittedCode[0];
                    for (int i = 1; i < splittedCode.Length; i++)
                    {
                        processedCodes.Add(type + " " + splittedCode[i] + ";");
                    }
                }
                else
                {
                    processedCodes.Add(codeLine);
                }
            }

            return processedCodes;
        }
    }
}

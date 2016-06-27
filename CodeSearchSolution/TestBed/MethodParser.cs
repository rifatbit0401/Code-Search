using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestBed.Model;

namespace TestBed
{
    public class MethodParser : IMethodParser
    {
        public const string MethodDeclarationExpr =
            @"(public|protected|private|static|\s) +[\w\<\>\[\]]+\s+(\w+) *\([^\)]*\) *(\{?|[^;])";

        //public const string MethodDeclarationExpr2 =
        //    @"\s+(?:<\w+>\s+){0,3}(?:[\w\<\>\[\]])+\s+\w+\s*\([^\)]*\)(?:\w|\s|\{)";
        public const string MethodDeclarationExpr2 =
            @"(public|protected|private|static|\s)+(?:<\w+>\s+){0,3}(?:[\w\<\>\[\]])+\s+\w+\s*\([^\)]*\)(?:\w|\s|\{)";
        private MethodUtility _methodUtility = new MethodUtility();

        public List<Method> GetMethods(string filePath)
        {
            var codeLines = new List<string>();
            codeLines.AddRange(File.ReadAllLines(filePath));

            var methods = new List<Method>();

            try
            {


                for (int currentLineNumber = 0; currentLineNumber < codeLines.Count(); currentLineNumber++)
                {
                    if (Regex.IsMatch(codeLines[currentLineNumber], MethodDeclarationExpr) ||
                        Regex.IsMatch(codeLines[currentLineNumber] + "{", MethodDeclarationExpr2))
                    {
                        var method = new Method();
                        method.Signature = codeLines[currentLineNumber];

                        var stack = new Stack<string>();

                        if (codeLines[currentLineNumber].Contains(";")) //for interface
                            continue;

                        while (!codeLines[currentLineNumber].Contains("{"))
                            currentLineNumber++;

                        if (currentLineNumber == codeLines.Count())
                            break;

                        stack.Push(codeLines[currentLineNumber]);


                        while (stack.Count() > 0)
                        {
                            //  Console.WriteLine(codeLines[currentLineNumber]);
                            method.Body.Add(codeLines[currentLineNumber]);
                            currentLineNumber++;


                            if (codeLines[currentLineNumber].Contains("{"))
                                stack.Push(codeLines[currentLineNumber]);

                            if (codeLines[currentLineNumber].Contains("}"))
                                stack.Pop();

                        }

                        method.Body.Add("}");
                        method.Body.Remove(method.Signature);
                        method = _methodUtility.ConstructMethod(method.Signature, method.Body);
                        methods.Add(method);
                    }

                }
            }
            catch (Exception exception)
            {
               // Console.WriteLine(exception.Message);
            }

            return methods;
        }


    }
}

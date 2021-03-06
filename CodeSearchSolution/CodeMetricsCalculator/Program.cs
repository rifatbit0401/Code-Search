﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed;

namespace CodeMetricsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new DirectoryBasedMethodParser();
            var methods = parser.ParseAllMethods(@"E:\BSSE Program\3rd semister BIT program\OOP-2\workspace\Calculator2\src");

            var sourceCode = new List<String>();
            var codeMetricsCalculator = new CodeMetricsCalculator();

            foreach (var method in methods)
            {
                Console.WriteLine(method.Signature);
                Console.WriteLine("Loc: {0}", codeMetricsCalculator.GetLineOfCode(method.Body));
                Console.WriteLine("NoA: {0}; NoFC: {1}; NoCS: {2}",
                                  codeMetricsCalculator.GetNumberOfArgument(method.Signature),
                                  codeMetricsCalculator.GetNumberOfFunctionCall(method.Body),
                                  codeMetricsCalculator.GetNumberOfConditionalStatements(method.Body));
                Console.WriteLine("NIS: {0}; NRS: {1}; NLV: {2}",
                                  codeMetricsCalculator.GetNumberOfIterationStatements(method.Body),
                                  codeMetricsCalculator.GetNumberOfReturnStatements(method.Body),
                                  codeMetricsCalculator.GetNumberOfLocalVariable(method.Body));
            }

            Console.WriteLine(char.IsPunctuation('_'));
            Console.ReadLine();
        }
    }
}

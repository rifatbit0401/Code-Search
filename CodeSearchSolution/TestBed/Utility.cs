using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBed.Model;

namespace TestBed
{
    public class Utility
    {
        public List<Method> RefineMethodsUsingCamelCase(List<Method> methods)
        {
            var refinedMethods = new List<Method>();
            foreach (var method in methods)
            {
                var refinedMethod = new Method();
                refinedMethod.Signature = CovertCamelCaseToSpaceSeparted(method.Signature);
                foreach (var line in method.Body)
                {
                    refinedMethod.Body.Add(CovertCamelCaseToSpaceSeparted(line));
                }
                refinedMethods.Add(refinedMethod);
            }
            return refinedMethods;
        }

        public string CovertCamelCaseToSpaceSeparted(string str)
        {
            string spaceSeperatedString = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsUpper(str[i]) || !Char.IsLetter(str[i]))
                    spaceSeperatedString += " ";
                spaceSeperatedString += str[i];
            }

            return spaceSeperatedString.ToLower();
        }

    }
}

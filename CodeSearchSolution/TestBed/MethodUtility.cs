using System.Collections.Generic;
using TestBed.Model;

namespace TestBed
{
    public class MethodUtility
    {
        public Method ConstructMethod(string methodSignature, List<string> methodBody)
        {
            var method = new Method();
            method.Signature = methodSignature;
            method.Body = methodBody;

            var splitted = methodSignature.Trim().Split(' ');
            int startIndex = 0;
            if (splitted[startIndex].Equals("public") ||
                splitted[startIndex].Equals("private") ||
                splitted[startIndex].Equals("protected") ||
                splitted[startIndex].Equals("static"))
            {
                startIndex++;
            }

            if (splitted[startIndex].Equals("public") ||
                splitted[startIndex].Equals("private") ||
                splitted[startIndex].Equals("protected") ||
                splitted[startIndex].Equals("static"))
                startIndex++;

            if (!splitted[startIndex].Contains("("))
                method.ReturnType = splitted[startIndex];
            method.MethodName = splitted[startIndex + 1].Split('(')[0];
            method.Parameters = "";

            for (int i = methodSignature.IndexOf('(') + 1; i < methodSignature.IndexOf(')'); i++)
            {
                method.Parameters += methodSignature[i];
            }


            return method;
        }
    }
}

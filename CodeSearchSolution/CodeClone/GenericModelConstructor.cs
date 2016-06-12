using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClone
{
    public class GenericModelConstructor
    {
        private Rule _rule = new Rule();

        public List<String> CreateModel(List<string> sourceCode)
        {
            var codeLines = sourceCode.ToArray();
            var model = new List<string>();
            var stack = new Stack<string>();

            for (int i = 0; i < codeLines.Length; i++)
            {
                var tag = _rule.GetTag(codeLines[i]);

                if (codeLines[i].Contains("{"))
                    stack.Push("{");
                if (codeLines[i].Contains("}"))
                    stack.Pop();
                model.Add(GetFormattedTag(stack.Count(), tag));
            }

            return model;
        }

        public string GetFormattedTag(int stackSize, Tag tag)
        {
            string formattedTag = "";
            if (tag == Tag.NOT_FOUND)
                return formattedTag;

            for (int i = 0; i < stackSize; i++)
            {
                formattedTag += "  ";
            }
            formattedTag += tag.ToString();
            return formattedTag;
        }

        public bool IsSimilar(String[] model1, String[] model2)
        {
            for (int i = 0; i < model1.Length; i++)
            {
                if (model1[i] != model2[i])
                    return false;
            }
            return true;
        }

    }
}

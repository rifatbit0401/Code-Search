using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClone
{
    public class TextMatcher
    {
        public bool IsExactMatch(List<String> codeFragment1, List<String> codeFragment2)
        {
            if (codeFragment1.Count != codeFragment2.Count)
                return false;
            for (int i = 0; i < codeFragment1.Count; i++)
            {
                if (codeFragment1[i].Trim() != codeFragment2[i].Trim())
                    return false;
            }
            return true;
        }
    }
}

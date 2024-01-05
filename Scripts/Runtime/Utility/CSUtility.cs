using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public static class CSUtility
    {
        public static bool Contains(this string[] array, string varContains)
        {
            foreach (var item in array)
            {
                if (item.Equals(varContains))
                    return true;
            }
            return false;
        }
    }
}

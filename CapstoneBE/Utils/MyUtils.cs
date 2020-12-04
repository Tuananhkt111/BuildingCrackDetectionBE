using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Utils
{
    public static class MyUtils
    {
        public static bool IsNumeric(string s)
        {
            return int.TryParse(s, out _);
        }
    }
}
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

        public static ValueTuple<int, int> GetMonthValue(int period)
        {
            return period switch
            {
                1 => (1, 4),
                2 => (5, 8),
                3 => (9, 12),
                _ => (0, 0),
            };
        }
    }
}
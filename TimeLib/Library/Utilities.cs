using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLib.Library
{
    static class Utilities
    {
        public static string FormatValue(byte val)
        {
            if (val < 10)
            {
                return $"0{val}";
            }

            return $"{val}";
        }
        public static string FormatValue(long val)
        {
            if (val < 10)
            {
                return $"0{val}";
            }

            return $"{val}";
        }
    }
}

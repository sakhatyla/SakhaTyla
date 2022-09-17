using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Infrastructure
{
    public static class StringExtensions
    {
        public static string GetShortText(this string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                str = str.Substring(0, maxLength) + "...";
            }
            return str;
        }
    }
}

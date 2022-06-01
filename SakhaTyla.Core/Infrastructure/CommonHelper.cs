using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Infrastructure
{
    public class CommonHelper
    {
        public static string MultiplyString(string str, int times)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < times; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        public static int GetDigitCount(int number)
        {
            return (int)Math.Floor(Math.Log10(number) + 1);
        }

        public static int GetLevel(string path)
        {
            return path != null ? (path.Count(c => c == '/') - 1) : 0;
        }
    }
}

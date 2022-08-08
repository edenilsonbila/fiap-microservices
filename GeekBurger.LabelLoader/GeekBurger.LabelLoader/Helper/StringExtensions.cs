using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.LabelLoader.Helper
{
    public static class StringExtensions
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}

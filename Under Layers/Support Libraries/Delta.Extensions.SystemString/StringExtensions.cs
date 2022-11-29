using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace System
{
    public static class StringExtensions
    {
        //public static bool IsHarfCharacters(this string target)
        //{
        //    return new Regex("^[\u0020-\u007E\uFF66-\uFF9F]+$").IsMatch(target);
        //}

        //public static int WidthSize(this string target)
        //{
        //    return target.ToCharArray().Sum(c => c.ToString().IsHarfCharacters() ? 1 : 2);
        //}

        public static string ToPascalCase(this string target)
        {
            if(target.Length==0) return string.Empty;
            return target.Substring(0, 1).ToUpper() + target.Substring(1).ToLower();
        }
        public static string ToCamelCase(this string target)
        {
            if (target.Length == 0) return string.Empty;
            return target.Substring(0, 1).ToLower() + target.Substring(1);
        }

        public static string ReplacePart(this string original, int startIndex, string replacePart)
        {
            return ReplacePart(original,startIndex,replacePart.Length,replacePart);
        }

        public static string ReplacePart(this string original, int startIndex, int length, string replacePart)
        {
            if(original==null) return (new String(' ',startIndex)) + replacePart;
            if(startIndex + 1 > original.Length) return original + (new String(' ', startIndex + 1 - original.Length)) + replacePart;
            return original.Remove(startIndex, length).Insert(startIndex, replacePart + (new String(' ', length - replacePart.Length)));
        }

        public static string CompressSpacesToSingleSpace(this string original)
        {
            var reg = new Regex(@"\s\s+");
            return reg.Replace(original, " ");
        }
    }
}

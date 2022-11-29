using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public interface IToClassification
    {
        IEnumerable<string> ToClassification();
    }

    public static class IToClassificationExtensions
    {
        public static IEnumerable<string> ToPascalCaseClassification(this IToClassification target)
        {
            return target.ToClassification().Select(n => n.ToPascalCase());
        }

        public static string ClassificationToString(this IToClassification target,string separator)
        {
            return string.Join(separator, target.ToClassification());
        }

        public static string ClassificationToStringWithComma(this IToClassification target)
        {
            return target.ClassificationToString(",");
        }

    }
}

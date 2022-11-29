using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Utilities.Extensions.SystemString
{
    public class TextClipper
    {

        public static string ClipParameter(string originalLine, string parameterName)
        {
            return ClipParameter(originalLine,parameterName, '(', ')');
        }

        public static string ClipParameter(string originalLine, string parameterName, char startMark, char endMark)
        {
            var startOfParameter = originalLine.IndexOf(parameterName);
            if (startOfParameter < 0) return string.Empty;

            var line = originalLine.Substring(startOfParameter);
            var startIndex = line.IndexOf(startMark);
            if (startIndex < 0) return string.Empty;

            var endIndex = getEndIndex(line, startIndex + 1, startMark, endMark);

            return line.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        static int getEndIndex(string line, int startIndex,char startMark,char endMark)
        {

            var nextStart = line.IndexOf(startMark, startIndex);
            var nextEnd = line.IndexOf(endMark, startIndex);
            if (nextStart == -1 || nextEnd <= nextStart)
            {
                return nextEnd;
            }
            else
            {
                var nextEndOfNextStart = line.IndexOf(endMark, nextStart);
                if (nextEnd == nextEndOfNextStart)
                {
                    return getEndIndex(line, nextEnd + 1, startMark, endMark);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public static List<string> List(string line)
        {
            var list = new List<string>();

            if(line.IndexOf('(')==-1)
            {
                list.Add(line.Trim());
                return list;
            }

            int endIndex = 0;
            do
            {
                var startIndex = line.IndexOf('(', endIndex);
                if (startIndex == -1) break;
                endIndex = line.IndexOf(')', startIndex);
                var val = line.Substring(startIndex + 1, endIndex - startIndex - 1);
                list.Add(val);
            } while (true);
            return list;
        }

    }
}

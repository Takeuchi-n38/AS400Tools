using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataTypes.Characters
{
    public class CodePage290Comparator : IComparer<string>
    {

        public int Compare(string Left, string Right)
        {

            char[] leftChars = Left==null?new char[0]: Left.ToCharArray();
            char[] rightChars = Right == null ? new char[0] : Right.ToCharArray();
            for (int i = 0; i < Math.Min(leftChars.Length, rightChars.Length); i++)
            {
                if (leftChars[i] == rightChars[i])
                {
                    continue;
                }

                var leftParsed = CodePage290.TryParse(leftChars[i], out byte cp290byteOfLeft);
                var rightParsed = CodePage290.TryParse(rightChars[i], out byte cp290byteOfRight);
                if (leftParsed)
                {
                    if (rightParsed)
                    {
                        return cp290byteOfLeft - cp290byteOfRight;
                    }
                    return -1;
                }

                if (rightParsed)
                {
                    return 1;
                }

                return leftChars[i].CompareTo(rightChars[i]);
                //CodePage930
                //return CodePage300.Compare(leftChars[i], rightChars[i]);

            }

            return leftChars.Length - rightChars.Length;

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class BytesComparator : IComparer<IEnumerable<byte>>
    {
        public static IComparer<IEnumerable<byte>> Instance =new BytesComparator();
        int IComparer<IEnumerable<byte>>.Compare(IEnumerable<byte> x, IEnumerable<byte> y)
        {
            var xBytes = x.ToArray();
            var yBytes = y.ToArray();
            for (int i = 0; i < Math.Min(xBytes.Length, yBytes.Length); i++)
            {
                var xByte = xBytes[i];
                var yByte = yBytes[i];
                if (xBytes[i] == yBytes[i])
                {
                    continue;
                }
                return xByte.CompareTo(yByte);
            }

            return xBytes.Length - yBytes.Length;
        }
    }
}

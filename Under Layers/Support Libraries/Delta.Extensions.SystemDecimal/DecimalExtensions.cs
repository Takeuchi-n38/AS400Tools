using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class DecimalExtensions
    {
        public static decimal Of(long all, bool isPositive, byte decimalPosition)
        {
            var bytes = BitConverter.GetBytes(all);
            int lo = BitConverter.ToInt32(bytes, 0);//(int)(all % (long)Math.Pow(2, 32));
            int mid = BitConverter.ToInt32(bytes, 4);//32bit
            int high = 0; //BitConverter.ToInt32(bytes, 8);//32bit

            return new decimal(lo, mid, high, !isPositive, decimalPosition);

        }
    }
}

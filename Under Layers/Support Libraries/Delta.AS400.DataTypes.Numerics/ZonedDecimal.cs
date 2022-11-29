using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataTypes.Numerics
{
    public class ZonedDecimal
    {
        //[Obsolete("廃止予定です。　BytesExtensions.ToHexString を使ってください。")]

        //public static void ToUnsignedZonedBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ToUnsignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}

        //[Obsolete("廃止予定です。　BytesExtensions.ToHexString を使ってください。")]

        //public static void ToSignedZonedBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ToSignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}

        public static void SetUnsignedBytes(decimal sourceValue, byte[] destinationArray, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            BytesExtensions.SetBytes(ToUnsignedBytesFrom(sourceValue, aEndPosition - aStartPosition + 1, decimalPosition), destinationArray, aStartPosition - 1);
        }

        public static void SetSignedBytes(decimal sourceValue, byte[] destinationArray, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            BytesExtensions.SetBytes(ToSignedBytesFrom(sourceValue, aEndPosition - aStartPosition + 1, decimalPosition), destinationArray, aStartPosition - 1);
        }

        public static int IntegerToInt(IEnumerable<byte> aZonedBytesByCCSID930)
        {
            var zone = aZonedBytesByCCSID930.ToArray();
            var result = 0;
            for (int i = zone.Length - 1; i >= 0; i--)
            {
                var num = zone[i] & 0x0F;
                result += num * (int)Math.Pow(10, zone.Length - 1 - i);
            }
            return result * (IsNegative(zone.Last()) ? -1 : 1);
        }

        public static int ToIntFrom(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            return (int)ToDecimalFrom(aZoneCCSID930Bytes, aStartPosition, aEndPosition);
        }

        public static decimal ToDecimalFrom(byte[] aZonedBytesByCCSID930, int aStartPosition, int aEndPosition, byte decimalPosition=0)
        {
            var targetZoneCCSID930Bytes = aZonedBytesByCCSID930.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1)).ToArray();

            long all = targetZoneCCSID930Bytes.Last() & 0x0F;
            //int lo = targetZoneCCSID930Bytes.Last() & 0x0F;//32bit
            //int mid = 0;//32bit
            //int high = 0;//32bit
            for (int dig = 2; dig <= targetZoneCCSID930Bytes.Length; dig++)
            {
                var posOfbytes = targetZoneCCSID930Bytes.Length - dig;
                var num = targetZoneCCSID930Bytes[posOfbytes] & 0x0F;
                //var add = (int)(num * Math.Pow(10, (dig - 1) % 4));
                all += (long)(num * Math.Pow(10, (dig - 1)));

                //if (dig <= 4)
                //{
                //    lo += add;
                //}
                //else
                //if (dig <= 8)
                //{
                //    mid += add;
                //}
                //else
                //{
                //    high += add;
                //}
            }

            return DecimalExtensions.Of(all, !IsNegative(targetZoneCCSID930Bytes.Last()), decimalPosition);
        }

        //public static byte[] ToUnsignedBytesFrom(long target, int aStartPosition, int aEndPosition)
        //{
        //    return ToUnsignedBytesFrom(target, aEndPosition - aStartPosition + 1);
        //}

        public static byte[] ToUnsignedBytesFrom(long target, int length)
        {
            return ToBytesFrom(target, length, (byte)(target >= 0 ? 0xF0 : 0xD0));
        }

        //static byte[] ToSignedBytesFrom(long target, int aStartPosition, int aEndPosition)
        //{
        //    return ToSignedBytesFrom(target, aEndPosition - aStartPosition + 1);
        //}

        public static byte[] ToSignedBytesFrom(long target, int length)
        {
            return ToBytesFrom(target, length, (byte)(target >= 0 ? 0xC0 : 0xD0));
        }

        [Obsolete("廃止予定です。　ToUnsignedZonedBytesFrom 或いは　ToSignedZonedBytesFrom。")]
        public static byte[] ToZoneBytesFrom(long target, int length)
        {
            return ToBytesFrom(target, length, (byte)(target >= 0 ? 0xF0 : 0xD0));
        }

        static byte[] ToBytesFrom(long target, int length, byte signByte)
        {
            var zone = new byte[length];
            var numStr = target.ToString($"D{length}").Replace("-", string.Empty);
            //var numLength = packBytesLength * 2 - 1 - decimalPosition;
            //var format = decimalPosition == 0 ? new string('0', numLength) : $"{new string('0', numLength)}.{new string('0', decimalPosition)}";
            //var numStr = target.ToString($"{format};{format};").Replace(".", string.Empty);

            for (int i = zone.Length - 2; i >= 0; i--)
            {
                var num = i < numStr.Length ? int.Parse(numStr[i].ToString()) : 0;
                zone[i] = (byte)(0xF0 | (byte)num);
            }

            zone[length - 1] = (byte)(signByte | (byte)int.Parse(numStr.Last().ToString()));

            return zone;
        }

        public static byte[] ToUnsignedBytesFrom(decimal target, int length, byte decimalPosition=0)
        {
            if (decimalPosition == 0) return ToUnsignedBytesFrom((long)target, length);

            var numLong = (long)Math.Floor(decimal.Multiply(target, (decimal)Math.Pow(10, decimalPosition)));
            return ToUnsignedBytesFrom(numLong, length);
        }

        public static byte[] ToSignedBytesFrom(decimal target, int length, byte decimalPosition=0)
        {
            if (decimalPosition == 0) return ToSignedBytesFrom((long)target, length);

            var numLong = (long)Math.Floor(decimal.Multiply(target, (decimal)Math.Pow(10, decimalPosition)));
            return ToSignedBytesFrom(numLong, length);
        }

        static bool IsNegative(byte lastByte) => (lastByte | 0x0F) == 0xDF;

        public static string ToStringFrom(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            var decVal = ToDecimalFrom(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            if (decimalPosition == 0) return decimal.ToInt64(decVal).ToString("D");

            return (Math.Truncate(decimal.Multiply(decVal, new decimal(Math.Pow(10, decimalPosition)))) / new decimal(Math.Pow(10, decimalPosition))).ToString($"F{decimalPosition}");

        }

        public static string To4sStringFrom(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            if (decimalPosition != 0) throw new NotImplementedException();

            var decVal = ToDecimalFrom(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            return decimal.ToInt64(decVal).ToString($"D{aEndPosition - aStartPosition + 1}");
        }

    }
}

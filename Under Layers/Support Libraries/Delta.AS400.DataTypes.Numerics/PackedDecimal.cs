using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataTypes.Numerics
{
    public class PackedDecimal
    {
        //public static void ToUnsignedPackedBytesFrom(decimal target, byte[] aPackCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var packBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ToUnsignedBytesFrom(target, packBytesLength, decimalPosition);
        //    for (var i = 0; i < packBytesLength; i++) aPackCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}

        //public static void ToSignedPackedBytesFrom(decimal target, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var packBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ToSignedBytesFrom(target, packBytesLength, decimalPosition);
        //    for (var i = 0; i < packBytesLength; i++) aCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}
        public static void SetUnsignedBytes(decimal sourceValue, byte[] destinationArray, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            BytesExtensions.SetBytes(ToUnsignedBytesFrom(sourceValue, aStartPosition, aEndPosition, decimalPosition), destinationArray, aStartPosition - 1);
        }

        public static void SetSignedBytes(decimal sourceValue, byte[] destinationArray, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            BytesExtensions.SetBytes(ToSignedBytesFrom(sourceValue, aStartPosition, aEndPosition, decimalPosition), destinationArray, aStartPosition - 1);
        }

        static byte[] ToUnsignedBytesFrom(decimal target, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            return ToUnsignedBytesFrom(target, aEndPosition - aStartPosition + 1, decimalPosition);
        }

        static byte[] ToSignedBytesFrom(decimal target, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            return ToSignedBytesFrom(target, aEndPosition - aStartPosition + 1, decimalPosition);
        }

        public static byte[] ToUnsignedBytesFrom(decimal target, int packBytesLength, byte decimalPosition=0)
        {
            return ToBytesFrom(target, packBytesLength, decimalPosition, (byte)(target >= 0 ? 0x0F : 0x0D));
        }

        public static byte[] ToSignedBytesFrom(decimal target, int packBytesLength, byte decimalPosition=0)
        {
            return ToBytesFrom(target, packBytesLength, decimalPosition, (byte)(target >= 0 ? 0x0C : 0x0D));
        }

        static byte[] ToBytesFrom(decimal target, int packBytesLength, byte decimalPosition,byte signByte)
        {
            //int digitLength = packBytesLength*2-1;
            var packBytes = new byte[packBytesLength];
            var numLength= packBytesLength * 2 - 1 - decimalPosition;
            var format = decimalPosition==0 ? new string('0',numLength) : $"{new string('0', numLength)}.{new string('0', decimalPosition)}";

            var numStr = target.ToString($"{format};{format};").Replace(".",string.Empty);
            for (int idxOfPack = 0; idxOfPack < packBytesLength - 1; idxOfPack++)
            {
                var numStrIdx= idxOfPack*2;
                packBytes[idxOfPack] = (byte)((byte)int.Parse(numStr[numStrIdx].ToString()) << 4 | (byte)int.Parse(numStr[numStrIdx+1].ToString()));
            }
            packBytes[packBytesLength - 1] = (byte)((byte)int.Parse(numStr.Last().ToString()) << 4 | signByte) ;
            return packBytes;
        }

        public static decimal ToDecimalFrom(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition=0)
        {
            var targetPackCCSID930Bytes = aCCSID930Bytes.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1)).ToArray();

            return ToDecimalFrom(targetPackCCSID930Bytes, decimalPosition);
        }

        public static decimal ToDecimalFrom(byte[] aPackedBytesByCCSID930, byte decimalPosition=0)
        {
            bool isPositive = !IsNegative(aPackedBytesByCCSID930.Last());
            long all = aPackedBytesByCCSID930.Last() >> 4;
            //int lo = aPackCCSID930Bytes.Last() & 0xF0;
            //int mid = 0;
            //int high = 0;
            for (int dig = 2; dig <= aPackedBytesByCCSID930.Length * 2 - 1; dig++)
            {
                var posOfbytes = aPackedBytesByCCSID930.Length - 1 - dig / 2;
                var num = aPackedBytesByCCSID930[posOfbytes] >> (dig % 2 == 0 ? 0 : 4) & 0x0F;
                all += (long)(num * Math.Pow(10, (dig - 1)));
                //var add = (int)(num * Math.Pow(10, (dig - 1) % 4));
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

            return DecimalExtensions.Of(all, isPositive, decimalPosition);
        }

        static bool IsNegative(byte lastByte) => (lastByte | 0xF0) == 0xFD;

        public static string ToStringFrom(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition)
        {
            var decVal = ToDecimalFrom(aCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            if (decimalPosition == 0) return decimal.ToInt64(decVal).ToString("D");

            return (Math.Truncate(decimal.Multiply(decVal, new decimal(Math.Pow(10, decimalPosition)))) / new decimal(Math.Pow(10, decimalPosition))).ToString($"F{decimalPosition}");

        }
        public static string To4sStringFrom(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition)
        {
            if (decimalPosition != 0) throw new NotImplementedException();

            var decVal = ToDecimalFrom(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            return decimal.ToInt64(decVal).ToString($"D{(aEndPosition - aStartPosition + 1) * 2 - 1}");
        }

    }
}
/*
sbyte	-128 ～ 127	符号付き 8 ビット整数	System.SByte
byte	0 ～ 255	符号なし 8 ビット整数	System.Byte
short	-32,768 ～ 32,767	符号付き 16 ビット整数	System.Int16
ushort	0 ～ 65,535	符号なし 16 ビット整数	System.UInt16
int	-2,147,483,648 ～ 2,147,483,647	符号付き 32 ビット整数	System.Int32
uint	0 ～ 4,294,967,295	符号なし 32 ビット整数	System.UInt32
long	-9,223,372,036,854,775,808 から 9,223,372,036,854,775,807	符号付き 64 ビット整数	System.Int64
ulong	0 ～ 18,446,744,073,709,551,615	符号なし 64 ビット整数	System.UInt64
 */
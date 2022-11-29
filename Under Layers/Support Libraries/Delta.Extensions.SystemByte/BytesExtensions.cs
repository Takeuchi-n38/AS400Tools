using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class BytesExtensions
    {
        public static List<byte[]> ReplacePart(List<byte[]> originals, int replaceStartIndex, byte[] replaceBytes)
        {
            var updates = new List<byte[]>();

            originals.ToList().ForEach(original =>
            {
                var update = new byte[original.Length];
                Array.Copy(original, update, update.Length);
                Array.Copy(replaceBytes, 0, update, replaceStartIndex, replaceBytes.Length);
                updates.Add(update);
            });
            return updates;
        }

        public static byte[] ToBytesFrom(string aHexString)
        {
            return Regex.Split(aHexString, @"(?<=\G.{2})(?!$)")
                .Select(hex => (byte)Convert.ToInt32(hex, 16))
                .ToArray();
        }

        public static string ToHexString(this byte aByte)
        {
            return aByte.ToString("X2");
        }

        public static string ToHexString(IEnumerable<byte> aBytes)
        {
            return aBytes.Select(b => b.ToHexString()).Aggregate((all, cur) => $"{all}{cur}");
        }

        //public static string ToHexString(byte[] aBytes, int aStartPosition, int aEndPosition)
        //{
        //    return ToHexString(aBytes.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1)));
        //}
        //            Array.Copy(zoneBytes, 0, destinationArray, aStartPosition - 1, zoneBytes.Length);

        public static void SetBytes(byte[] sourceArray, byte[] destinationArray, int destinationIndex)
        {
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
        }

        public static void SetBytes(byte[] sourceArray, byte[] destinationArray, int aStartIndex, int aEndIndex, byte padByte)
        {
            var length = aEndIndex - aStartIndex + 1;
            for (var i = 0; i < length; i++) destinationArray[aStartIndex + i] = (i < sourceArray.Length) ? sourceArray[i] : padByte;
        }

    }
}

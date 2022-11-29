using Delta.AS400.DataTypes.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Delta.AS400.DataTypes.Characters
{

    public static class CCSID930
    {

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static int GetByteLength(string? utf16Strings)
        {
            int sum = 0;
            foreach (char c in utf16Strings.ToCharArray())
            {
                sum += CodePage290.TryParse(c, out byte cp290byte) ? 1 : 2;
            }
            return sum;
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Encoding ibm290 = Encoding.GetEncoding(20290);//20290   IBM290  IBM EBCDIC (日本語カタカナ)
            //return ibm290.GetBytes(utf16Strings).Length;
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static void ToCCSID930CharBytesFrom(string? utf16Strings, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            ToCCSID930BytesFrom(utf16Strings,aCCSID930Bytes,aStartPosition,aEndPosition, CodePage290.ByteOfSpace);
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static void ToCCSID930BytesFrom(string? utf16Strings,byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition,byte padByte)
        {
            var bytes = ToCCSID930BytesFrom(utf16Strings);
            ToCCSID930BytesFrom(bytes, aCCSID930Bytes, aStartPosition, aEndPosition, padByte);
        }
        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static void ToCCSID930BytesFrom(byte setByte, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            ToCCSID930BytesFrom(new byte[]{ setByte }, aCCSID930Bytes, aStartPosition, aEndPosition, CodePage290.ByteOfSpace);
        }
        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static void ToCCSID930BytesFrom(byte[] bytes, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte padByte)
        {
            var length = aEndPosition - aStartPosition + 1;
            for (var i = 0; i < length; i++) aCCSID930Bytes[aStartPosition - 1 + i] = (i < bytes.Length) ? bytes[i] : padByte;
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static byte[] ToCCSID930BytesFrom(string? utf16Strings,int length)
        {
            var padded = string.IsNullOrEmpty(utf16Strings)?new string(' ',length): utf16Strings.PadRight(length, ' ');
            return ToCCSID930BytesFrom(padded);
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static byte[] ToCCSID930BytesFrom(string? utf16Strings)
        {
            if(string.IsNullOrEmpty(utf16Strings)) return Array.Empty<byte>();

            var CCSID930Bytes =new List<byte>();
            int lastByteLength = 1;
            foreach (char c in utf16Strings.ToCharArray())
            {
                if (CodePage290.TryParse(c, out byte cp290Byte))
                {
                    if (lastByteLength == 2)//shiftIn
                    {
                        CCSID930Bytes.Add(CodePage290.ByteOfShiftIn);
                    }
                    CCSID930Bytes.Add(cp290Byte);
                    lastByteLength = 1;
                    continue;
                }

                if (CodePage300.Characters.Any(kv => kv.Value == c))
                {
                    if (lastByteLength == 1)//shiftOut
                    {
                        CCSID930Bytes.Add(CodePage290.ByteOfShiftOut);
                    }
                    var key = CodePage300.Characters.Where(kv => kv.Value == c).First().Key;
                    CCSID930Bytes.Add(key.Item1); CCSID930Bytes.Add(key.Item2);
                    lastByteLength = 2;
                    continue;
                }

            }
            return CCSID930Bytes.ToArray();
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static long ToIntegerFrom(byte[] aCCSID930Bytes, byte aStartPosition, byte aEndPosition)
        {
            var str=ToStringFrom(aCCSID930Bytes,aStartPosition,aEndPosition);
            if(str.Trim()==string.Empty) return 0;
            return long.Parse(str);
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static string ToStringFrom(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            var bytes= aCCSID930Bytes.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1)).ToArray();
            return ToStringFrom(bytes);
        }

        [Obsolete("廃止予定です。CodePage930 を使ってください。")]
        public static string ToStringFrom(byte[] aCCSID930Bytes)
        {
            var chars = new List<char>();
            var isCP930mode = false;
            for (var i = 0; i < aCCSID930Bytes.Length; i++)
            {
                var b1 = aCCSID930Bytes[i];
                if (CodePage290.IsShiftOut(b1))
                {
                    isCP930mode = true;
                    chars.Add(' ');
                    continue;
                }
                if (CodePage290.IsShiftIn(b1))
                {
                    isCP930mode = false;
                    chars.Add(' ');
                    continue;
                }

                if (isCP930mode)
                {
                    if(i == aCCSID930Bytes.Length-1)
                    {
                        if (aCCSID930Bytes[i] != 0x40)
                        {
                            Debug.WriteLine($"shortageBytes : { aCCSID930Bytes[i].ToString("X2")}");
                        }
                        chars.Add(' ');
                    }
                    else
                    {
                        var b2 = aCCSID930Bytes[++i];
                        if (b1 == 0x40 && b2 == 0x40)
                        {
                            chars.Add(' ');
                            chars.Add(' ');
                        }
                        else
                        {
                            chars.Add(CodePage300.GetChar(b1, b2));
                        }
                    }
                }
                else
                {
                    var val= CodePage290.GetChar(b1);
                    chars.Add(val.HasValue?val.Value:'?');
                }
            }
            return new string(chars.ToArray());
        }

        [Obsolete("廃止予定です。　PackedDecimalとZonedDecimal を使ってください。")]
        public static byte[] ToIntegerZoneFromDecimalPack(byte[] aDecimalPackCCSID930Bytes, int decimalPosition, int lengthOfZone)
        {
            if (decimalPosition < 1) throw new NotImplementedException();

            var decZone = ToIntegerZoneFromIntegerPack(aDecimalPackCCSID930Bytes, aDecimalPackCCSID930Bytes.Length * 2 - 1);

            var zone = new byte[lengthOfZone];
            for (int i = 0; i < lengthOfZone - 1; i++)
            {
                zone[i] = 0xF0;
            }

            for (int idxOfDecZone = decZone.Length - decimalPosition - 1; idxOfDecZone >= 0; idxOfDecZone--)
            {
                var digitMinus1 = decZone.Length - decimalPosition - 1 - idxOfDecZone;
                if (lengthOfZone - 1 - digitMinus1 < 0) break;
                zone[lengthOfZone - 1 - digitMinus1] = decZone[idxOfDecZone];
            }

            zone[lengthOfZone - 1] = (byte)(aDecimalPackCCSID930Bytes.Last() << 4 | zone[lengthOfZone - 1] & 0xFF);

            return zone;
        }

        [Obsolete("廃止予定です。　PackedDecimalとZonedDecimal を使ってください。")]
        public static byte[] ToIntegerZoneFromIntegerPack(byte[] aIntegerPackCCSID930Bytes, int lengthOfZone)
        {
            var zone = new byte[lengthOfZone];
            for (int i = 0; i < lengthOfZone; i++)
            {
                zone[i] = 0xF0;
            }

            zone[lengthOfZone - 1] = (byte)(aIntegerPackCCSID930Bytes.Last() << 4 | aIntegerPackCCSID930Bytes.Last() >> 4);

            for (int idxOfPack = aIntegerPackCCSID930Bytes.Length - 2; idxOfPack >= 0; idxOfPack--)
            {
                var digitMinus1 = (aIntegerPackCCSID930Bytes.Length - 1 - idxOfPack) * 2 - 1;
                if (lengthOfZone - (digitMinus1 + 2) >= zone.Length) break;
                zone[lengthOfZone - (digitMinus1 + 2)] = (byte)(aIntegerPackCCSID930Bytes[idxOfPack] >> 4 | 0xF0);
                if (lengthOfZone - (digitMinus1 + 1) >= zone.Length) break;
                zone[lengthOfZone - (digitMinus1 + 1)] = (byte)(aIntegerPackCCSID930Bytes[idxOfPack] | 0xF0);
            }
            return zone;
        }

        [Obsolete("廃止予定です。　CodePage290.ToStringFrom を使ってください。")]
        public static string ToStringFrom(byte a290Byte)
        {
            var result = CodePage290.GetChar(a290Byte);
            if (result == null) return "?";
            return result.Value.ToString();
        }

        [Obsolete("廃止予定です。　BytesExtensions.ToHexString を使ってください。")]
        public static string ToHexString(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            return aCCSID930Bytes.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1))
                .Select(b => b.ToString("X2")).Aggregate((all, cur) => $"{all}{cur}");
        }

        [Obsolete("廃止予定です。　BytesExtensions.SetBytes を使ってください。")]
        public static void SetBytes(byte[] target, byte[] aCCSID930Bytes, byte aStartPosition, byte aEndPosition)
        {
            Array.Copy(target, 0, aCCSID930Bytes, aStartPosition - 1, aEndPosition - (aStartPosition - 1));
        }

        [Obsolete("廃止予定です。　BytesExtensions.ToBytesFrom を使ってください。")]
        public static byte[] ToBytesFrom(string aHexStringOfCP290ByteOfCharacters)
        {
            return Regex.Split(aHexStringOfCP290ByteOfCharacters, @"(?<=\G.{2})(?!$)")
                .Select(hex => (byte)Convert.ToInt32(hex, 16))
                .ToArray();
        }

        [Obsolete("廃止予定です。　PackedDecimal.ToDecimalFrom を使ってください。")]
        public static decimal ToDecimalFromPack(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition)
        {
            return PackedDecimal.ToDecimalFrom(aCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        }
        [Obsolete("廃止予定です。　PackedDecimal.ToStringFrom を使ってください。")]
        public static string ToStringFromPack(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition)
        {
            var decVal = ToDecimalFromPack(aCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            if (decimalPosition == 0) return decimal.ToInt64(decVal).ToString("D");

            return (Math.Truncate(decimal.Multiply(decVal, new decimal(Math.Pow(10, decimalPosition)))) / new decimal(Math.Pow(10, decimalPosition))).ToString($"F{decimalPosition}");

        }
        [Obsolete("廃止予定です。　PackedDecimal.To4sStringFrom を使ってください。")]
        public static string To4sStringFromPack(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition)
        {
            if (decimalPosition != 0) throw new NotImplementedException();

            var decVal = ToDecimalFromPack(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            return decimal.ToInt64(decVal).ToString($"D{(aEndPosition - aStartPosition + 1) * 2 - 1}");
        }

        [Obsolete("廃止予定です。　ZonedDecimal.ToStringFrom を使ってください。")]
        public static string ToStringFromZone(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            var decVal = ToDecimalFromZone(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            if (decimalPosition == 0) return decimal.ToInt64(decVal).ToString("D");

            return (Math.Truncate(decimal.Multiply(decVal, new decimal(Math.Pow(10, decimalPosition)))) / new decimal(Math.Pow(10, decimalPosition))).ToString($"F{decimalPosition}");

        }

        [Obsolete("廃止予定です。　ZonedDecimal.To4sStringFrom を使ってください。")]
        public static string To4sStringFromZone(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            if (decimalPosition != 0) throw new NotImplementedException();

            var decVal = ToDecimalFromZone(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
            return decimal.ToInt64(decVal).ToString($"D{aEndPosition - aStartPosition + 1}");
        }

        [Obsolete("廃止予定です。　PackedDecimal.ToUnsignedPackedBytesFrom 或いは　PackedDecimal.ToSignedPackedBytesFromを使ってください。")]
        public static void ToPackBytesFrom(decimal target, byte[] aPackCCSID930Bytes, byte aStartPosition, byte aEndPosition, byte decimalPosition)
        {
            PackedDecimal.SetUnsignedBytes(target,aPackCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        }

        [Obsolete("廃止予定です。ZonedDecimal.ToIntFromZone を使ってください。")]
        public static int ToIntFromZone(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            return (int)ToDecimalFromZone(aZoneCCSID930Bytes, aStartPosition, aEndPosition);
        }

        [Obsolete("廃止予定です。ZonedDecimal.ToDecimalFrom を使ってください。")]
        public static decimal ToDecimalFromZone(byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            return ZonedDecimal.ToDecimalFrom(aZoneCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        }

        [Obsolete("廃止予定です。　ZonedDecimal.ToUnsignedZoneBytesFrom 或いは　ZonedDecimal.ToSignedZoneBytesFrom を使ってください。")]
        public static void ToZoneBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        {
            BytesExtensions.SetBytes(ZonedDecimal.ToUnsignedBytesFrom(target, aEndPosition - aStartPosition + 1, decimalPosition), aZoneCCSID930Bytes, aStartPosition - 1);
        }

        //public static void ToUnsignedZoneBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ZonedDecimal.ToUnsignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}

        //public static void ToSignedZoneBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ZonedDecimal.ToSignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}
        //public static void ToUnsignedPackedBytesFrom(decimal target, byte[] aPackCCSID930Bytes, byte aStartPosition, byte aEndPosition, byte decimalPosition = 0)
        //{
        //    PackedDecimal.ToUnsignedPackedBytesFrom(target, aPackCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        //}

        //public static void ToSignedPackedBytesFrom(decimal target, byte[] aCCSID930Bytes, byte aStartPosition, byte aEndPosition, byte decimalPosition = 0)
        //{
        //    PackedDecimal.ToSignedPackedBytesFrom(target, aCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        //}

        //public static void ToUnsignedZoneBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ZonedDecimal.ToUnsignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}

        //public static void ToSignedZoneBytesFrom(decimal target, byte[] aZoneCCSID930Bytes, int aStartPosition, int aEndPosition, byte decimalPosition = 0)
        //{
        //    var zoneBytesLength = aEndPosition - aStartPosition + 1;
        //    var packBytes = ZonedDecimal.ToSignedZonedBytesFrom(target, aStartPosition, aEndPosition, decimalPosition);
        //    for (var i = 0; i < zoneBytesLength; i++) aZoneCCSID930Bytes[aStartPosition - 1 + i] = packBytes[i];
        //}
        //public static void ToUnsignedPackedBytesFrom(decimal target, byte[] aPackCCSID930Bytes, byte aStartPosition, byte aEndPosition, byte decimalPosition = 0)
        //{
        //    PackedDecimal.ToUnsignedPackedBytesFrom(target, aPackCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        //}

        //public static void ToSignedPackedBytesFrom(decimal target, byte[] aCCSID930Bytes, byte aStartPosition, byte aEndPosition, byte decimalPosition = 0)
        //{
        //    PackedDecimal.ToSignedPackedBytesFrom(target, aCCSID930Bytes, aStartPosition, aEndPosition, decimalPosition);
        //}

    }

}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Delta.AS400.DataTypes.Characters
{

    public static class CodePage930
    {

        public static (string padded, int length) PadShiftInOut(string utf16Strings)
        {
            if (CodePage290.Encodable(utf16Strings)) return (utf16Strings, utf16Strings.Length);

            int lastByteLength = 1;
            int curByteLength = 1;
            var paddedStrings = new StringBuilder();
            int sum = 0;
            foreach (char c in utf16Strings.ToCharArray())
            {

                curByteLength = CodePage290.TryParse(c, out byte cp290byte) ? 1 : 2;

                if (lastByteLength == 1 && curByteLength == 2)//shiftIn
                {
                    paddedStrings.Append(" ");
                    sum++;
                }

                //paddedStrings.Append(v.Character.ToString());
                paddedStrings.Append(c);

                sum += curByteLength;

                if (lastByteLength == 2 && curByteLength == 1)//shiftOut
                {
                    paddedStrings.Append(" ");
                    sum++;
                }

                lastByteLength = curByteLength;
            }
            return (paddedStrings.ToString(), sum);
        }

        public static int GetByteLength(string utf16Strings)
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

        public static void SetBytes(string utf16Strings, byte[] adestinationArray, int aStartPosition, int aEndPosition)
        {
            BytesExtensions.SetBytes(ToBytesFrom(utf16Strings), adestinationArray, aStartPosition-1, aEndPosition-1, CodePage290.ByteOfSpace);
        }

        public static byte[] ToBytesFrom(string utf16Strings,int length)
        {
            var padded = string.IsNullOrEmpty(utf16Strings)?new string(' ',length): utf16Strings.PadRight(length, ' ');
            return ToBytesFrom(padded);
        }

        public static byte[] ToBytesFrom(string utf16Strings)
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

        public static long ToIntegerFrom(byte[] aCCSID930Bytes, byte aStartPosition, byte aEndPosition)
        {
            var str=ToStringFrom(aCCSID930Bytes,aStartPosition,aEndPosition);
            if(str.Trim()==string.Empty) return 0;
            return long.Parse(str);
        }

        public static string ToStringFrom(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        {
            var bytes= aCCSID930Bytes.Skip(aStartPosition - 1).Take(aEndPosition - (aStartPosition - 1)).ToArray();
            return ToStringFrom(bytes);
        }

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

        //public static void ToBytesFrom(string? utf16Strings, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition)
        //{
        //    SetBytes(utf16Strings, aCCSID930Bytes, aStartPosition, aEndPosition, CodePage290.ByteOfSpace);
        //}

        //public static void ToBytesFrom(byte[] bytes, byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, byte padByte)
        //{
        //    var length = aEndPosition - aStartPosition + 1;
        //    for (var i = 0; i < length; i++) aCCSID930Bytes[aStartPosition - 1 + i] = (i < bytes.Length) ? bytes[i] : padByte;
        //}


    }

}
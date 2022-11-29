using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delta.AS400.DataTypes.Characters
{
    public partial class CodePage290 //: CCSID930
    {
        public static byte ByteOf0 => 0xF0;

        public static byte ByteOfHyphen => 0x60;

        public static byte ByteOfComma => 0x6B;
        public static byte ByteOfShiftOut => 0x0E;
        public static byte ByteOfShiftIn => 0x0F;
        public static bool IsShiftOut(byte target) => target == ByteOfShiftOut;
        public static bool IsShiftIn(byte target) => target == ByteOfShiftIn;

        public static byte ByteOfSpace => 0x40;

        public static string MaxString(int length)
        {
            return new string('9', length);
        }

        static CodePage290()
        {
        }

        static Encoding IBM290 => EncodingExtension.IBM290;
        public static bool Encodable(string utf16Strings)
        {
            try
            {
                var encoded = IBM290.GetBytes(utf16Strings);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool TryParse(char utf16char, out byte result)
        {
            try
            {
                var converted = IBM290.GetBytes(new char[]{ utf16char });
                result = converted[0];
                return true;
            }
            catch (Exception ex)
            {
                result = 0x6F;
                return false;
            }
        }

        public static string ToStringFrom(byte aCP290ByteOfCharacter)
        {
            var result = GetChar(aCP290ByteOfCharacter);
            if (result == null) return "?";
            return result.Value.ToString();
        }

        public static char? GetChar(byte aCP290ByteOfCharacter)
        {
            if (!TryParse(aCP290ByteOfCharacter,out char utf16char))
            {
                Debug.WriteLine(string.Format("fumeiNaByte:{0:X2}", aCP290ByteOfCharacter));
                return null;
            }
            return utf16char;
        }

        public static bool TryParse(byte source , out char utf16char)
        {
            try
            {
                var converted = IBM290.GetChars(new byte[]{ source });
                utf16char = converted[0];
                return true;
            }
            catch (EncoderFallbackException ex)
            {
                utf16char = '?';
                return false;
            }
        }

        public static string ToHexString(string utf16Strings)
        {
            return string.Join("", IBM290.GetBytes(utf16Strings).Select(b => b.ToString("X2")).ToArray());
        }

        public static IComparer<string> Comparator = new CodePage290Comparator();

    }

}

using System;
using System.Text;

namespace Delta.Pdfs
{
    public class Common
    {
        public static readonly string ENCODING_MS932 = "MS932";

        public static readonly string ENCODING_UTF8 = "UTF-8";

        public static readonly string ENCODING_CP1252 = "Cp1252";

        public static readonly string FORMAT_COORDINATE = "{0}.{1}";

        public enum TypeChar
        {
            HALF_SIZE, FULL_SIZE, UNKNOW_TYPE
        }

        public static readonly int UNKNOW = 0;

        //*
        //* Encode string to hexadecimal string with MS932 charset.
        //*
        //* @param text - input text
        //* @return - hexadecimal string


        public static string ConvertToHexStr(string text)
        {
            byte[] bytes;
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                bytes = Encoding.GetEncoding(932).GetBytes(text);
            }
            catch (Exception)
            {
                throw new SystemException();
            }
            StringBuilder stringBuilder = new StringBuilder("");

            foreach (byte b in bytes)
            {
                stringBuilder.Append(string.Format("{0:X2}", b));
            }

            return stringBuilder.ToString();
        }
    }
}

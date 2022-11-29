using System;

namespace System.Text
{
    public static class EncodingExtension
    {
        static EncodingExtension()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        static Encoding _Shift_JIS = null;
        public static Encoding Shift_JIS
        {
            get
            {
                if (_Shift_JIS == null)
                {
                    _Shift_JIS = Encoding.GetEncoding("Shift_JIS");

                }
                return _Shift_JIS;
            }
        }

        static Encoding _IBM290 = null;
        public static Encoding IBM290
        {
            get
            {
                if (_IBM290 == null)
                {
                    _IBM290 = Encoding.GetEncoding(20290, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);//20290   IBM290  IBM EBCDIC (日本語カタカナ)
                }
                return _IBM290;
            }
        }
    }

}

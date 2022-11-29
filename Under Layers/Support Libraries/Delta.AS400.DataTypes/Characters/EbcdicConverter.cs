using System;
using System.Text;
//using Util.Miscellaneousness;

namespace Delta.AS400.Characters
{
    public class EbcdicConverter
    {
        public static string ToHexString(string input, int length)
        {
            try
            {
                //TODO: Format pattern
                string format = "{0,-" + length + "}";
                string target = SubstringByByte(string.Format(format, input), 0, length);

                bool shiftinFlg = false;
                StringBuilder result = new StringBuilder();
                byte[] prevhex = new byte[2];
                for (int i = 0; i < target.Length; i++)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    //TODO:cp930
                    var hex = Encoding.GetEncoding(20290).GetBytes(target[i].ToString());
                    if (i > 0)
                    {
                        if (shiftinFlg)
                        {
                            // Shift in
                            shiftinFlg = false;
                        }
                        else if (hex.Length > 1 && string.Format("{0:X2}", prevhex[0]).Equals("40"))
                        {
                            // Shift out
                        }
                        else if (string.Format("{0:X2}", hex[0]).Equals("40") && prevhex.Length > 1)
                        {
                            shiftinFlg = true;
                            result.Append(ConvertBytesToString(prevhex));
                        }
                        else
                        {
                            result.Append(ConvertBytesToString(prevhex));
                        }
                    }
                    prevhex = hex;
                }

                result.Append(ConvertBytesToString(prevhex));
                return result.ToString();
            }
            catch (NotSupportedException e)
            {
                throw;// new OurSystemException("エンコードに失敗しました。" + e.Message);
            }
        }

        private static string ConvertBytesToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte hexbyte in bytes)
            {
                sb.Append(string.Format("{0:X2}", hexbyte));
            }
            return sb.ToString();
        }

        private static string SubstringByByte(string input, int start, int end)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                //TODO: cp930
                byte[] toConvertBytes = Encoding.GetEncoding(20290).GetBytes(input);
                int length = end - start;
                int countBytes = 0;
                StringBuilder converted = new StringBuilder();
                int convertedByteLength = 0;
                byte[] byteString = new byte[4];
                bool hasTempByte = false;
                bool isDoubleString = false;

                for (var i = 0; i < toConvertBytes.Length; i++)
                {
                    if (convertedByteLength >= length)
                    {
                        break;
                    }

                    byte targetByte = toConvertBytes[i];
                    if (targetByte == Encoding.GetEncoding(20290).GetBytes("\u000E")[0])
                    {
                        isDoubleString = true;
                    }
                    else if (targetByte == Encoding.GetEncoding(20290).GetBytes("\u000F")[0])
                    {
                        isDoubleString = false;
                    }
                    else
                    {
                        if (countBytes >= start && countBytes < end)
                        {
                            if (isDoubleString)
                            {
                                if (hasTempByte)
                                {
                                    byteString[2] = targetByte;
                                    byteString[3] = 0x0f;
                                    //converted.Append(new string(Encoding.GetEncoding(20290).GetString(byteString)));
                                    converted.Append(Encoding.GetEncoding(20290).GetString(byteString));
                                    convertedByteLength += 2;
                                    hasTempByte = false;
                                }
                                else
                                {
                                    byteString = new byte[4];
                                    byteString[0] = 0x0e;
                                    byteString[1] = targetByte;
                                    hasTempByte = true;
                                }
                            }
                            else
                            {
                                byte[] targetBytes = new byte[1];
                                targetBytes[0] = targetByte;
                                //converted.Append(new string(Encoding.GetEncoding(20290).GetString(targetBytes)));
                                converted.Append(Encoding.GetEncoding(20290).GetString(targetBytes));
                                //(new string(targetBytes, "Cp290"));
                                convertedByteLength++;
                            }
                        }
                        countBytes++;
                    }
                }
                return converted.ToString();
            }
            catch (NotSupportedException e)
            {
                throw;// new OurSystemException(e);
            }
        }
    }
}

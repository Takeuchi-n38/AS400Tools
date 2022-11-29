using System;
using System.Text;

namespace Delta.Pdfs
{
    public class PdfLine
    {
        // PdfChar array
        public PdfChar[] pdfChars;

        // Real length of PdfChar array
        public int length;

        //*
        //* Constructor a line with row nth, max length and font configurations.
        //* @param row - row nth
        //* @param capacity - max length of line
        //* @param pdfFont - font configurations

        public PdfLine(int capacityEachLine)
        {
            // Index start is 1, not 0.
            pdfChars = new PdfChar[capacityEachLine + 1];
            length = 0;
        }

        //*
        //* Add a text to row with end column.
        //* @param endCol - end column number
        //* @param text - value

        public void Add(int endCol, string text)
        {

            if (endCol < 1 || endCol > pdfChars.Length - 1)
            {
                throw new Exception("The index column is out of range. The valid range is from 1 to " + (pdfChars.Length - 1) + ".");
            }

            for (int i = text.Length - 1, j = endCol; i > -1; i--)
            {
                // Validate start column of text
                //TODO
                //if (j == 0)
                //{
                //    throw new Exception("Start column of text is out of range.");
                //}

                // Encode a character with MS932
                byte[] encChar;
                encChar = new byte[] { };
                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    encChar = Encoding.GetEncoding(932).GetBytes(text.Substring(i, 1));
                }
                catch (EncoderFallbackException e)
                {
                    throw new Exception(e.Message);
                }
                // Check whether type of character is HALF SIZE or FULL SIZE
                if (encChar.Length == 1)
                {
                    pdfChars[j] = new PdfChar(encChar[0], Common.TypeChar.HALF_SIZE);
                    j--;
                }
                else
                {
                    pdfChars[j] = new PdfChar(encChar[1], Common.TypeChar.FULL_SIZE);
                    j--;
                    pdfChars[j] = new PdfChar(encChar[0], Common.TypeChar.FULL_SIZE);
                    j--;
                }
            }

            // Update real length of PdfChar array
            if (endCol > length)
            {
                length = endCol;
            }

        }
    }
}

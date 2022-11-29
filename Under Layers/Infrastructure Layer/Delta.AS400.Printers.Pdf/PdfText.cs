using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Pdfs
{
    public class PdfText
    {

        //  Format object as string value to write into PDF file
        private static readonly string FORMAT_TEXT = "<{0}> Tj\n";

        //  Joining status
        public enum JoiningStatus
        {

            INVISIBLE_CHAR,

            INCOMPATIBLE_TYPE,

            OUT_OF_RANGE,

            JOINED,
        }

        //  Beginning position (absolute) of text in line
        private int beginPosInLine;

        //  List of PDF Char has the same type
        private readonly List<PdfChar> pdfChars;

        //  Type of text: HALF_SIZE or FULL_SIZE
        private Common.TypeChar type;

        public PdfText()
        {
            beginPosInLine = Common.UNKNOW;
            pdfChars = new List<PdfChar>();
            type = Common.TypeChar.UNKNOW_TYPE;
        }

        private void SetBeginPosInLine(int beginPosInLine)
        {
            //  If beginning position is unknown, set new value to it
            if (this.beginPosInLine == Common.UNKNOW)
            {
                this.beginPosInLine = beginPosInLine;
            }
            else
            {
                throw new Exception("Can\'t change begin position of this PDF text in line.");
            }

        }

        public int GetBeginPosInLine()
        {
            return beginPosInLine;
        }

        private void SetType(Common.TypeChar type)
        {
            //  If type is unknown, set new value to it
            if (this.type == Common.TypeChar.UNKNOW_TYPE)
            {
                this.type = type;
            }
            else
            {
                throw new Exception("Can\'t change type of this PDF text.");
            }

        }

        public JoiningStatus Join(int posInLine, PdfChar pdfChar)
        {
            //  When the PdfChar is null or space
            if (pdfChar == null || pdfChar.IsInvisible())
            {
                return JoiningStatus.INVISIBLE_CHAR;
            }

            //  When PdfText is empty, will add this pdfChar as first element.
            //  Set beginning position of PdfText and determine type of PdfText.
            if (pdfChars.Count == 0)
            {
                pdfChars.Add(pdfChar);
                SetBeginPosInLine(posInLine);
                SetType(pdfChar.GetType());
                return JoiningStatus.JOINED;
            }

            //  When type of PdfText isn't like type of pdfChar
            if (type != pdfChar.GetType())
            {
                return JoiningStatus.INCOMPATIBLE_TYPE;
            }

            //  When type of PdfText is like type pdfChar
            int relativePos = posInLine - beginPosInLine;
            int start = 0;
            int end = pdfChars.Count - 1;
            if (start <= relativePos
                        && relativePos <= end)
            {
                //  When PdfChar's position is located in array
                //TODO
                pdfChars[relativePos] = pdfChar;
                return JoiningStatus.JOINED;
            }
            else if (type == Common.TypeChar.HALF_SIZE
                        && relativePos
                        <= end + 5)
            {
                //  When type of PdfText is HALF_SIZE and
                //  number of invisible character (space or '\0') between PdfText's end position and PdfChar's
                //  position is 4, join PdfChar
                for (int i = end + 1; i < relativePos; i++)
                {
                    //  Add space
                    pdfChars.Add(PdfChar.Space());
                }

                pdfChars.Add(pdfChar);
                return JoiningStatus.JOINED;
            }
            else if (type == Common.TypeChar.FULL_SIZE
                        && relativePos
                        == end + 1)
            {
                //  When type of PdfText is FULL_SIZE and
                //  PdfChar's position is next PdfText's end position, join PdfChar
                pdfChars.Add(pdfChar);
                return JoiningStatus.JOINED;
            }

            //  Else, status is out of range
            return JoiningStatus.OUT_OF_RANGE;
        }

        public bool IsEmpty()
        {
            return pdfChars.Count == 0;
        }

        public string GetWritableData()
        {
            StringBuilder sb = new StringBuilder("");
            //  Format PdfChar to create data to write into PDF file
            foreach (PdfChar pdfChar in pdfChars)
            {
                sb.Append(pdfChar.ToHexFormat());
            }

            return string.Format(FORMAT_TEXT, sb.ToString());
        }
    }
}
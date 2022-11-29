using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfText
    {

        //  Format object as string value to write into PDF file
        private static readonly String FORMAT_TEXT = "<{0}> Tj\n";

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
            this.beginPosInLine = Common.UNKNOW;
            this.pdfChars = new List<PdfChar>();
            this.type = Common.TypeChar.UNKNOW_TYPE;
        }

        private void SetBeginPosInLine(int beginPosInLine)
        {
            //  If beginning position is unknown, set new value to it
            if ((this.beginPosInLine == Common.UNKNOW))
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
            return this.beginPosInLine;
        }

        private void SetType(Common.TypeChar type)
        {
            //  If type is unknown, set new value to it
            if ((this.type == Common.TypeChar.UNKNOW_TYPE))
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
            if (((pdfChar == null) || pdfChar.IsInvisible()))
            {
                return JoiningStatus.INVISIBLE_CHAR;
            }

            //  When PdfText is empty, will add this pdfChar as first element.
            //  Set beginning position of PdfText and determine type of PdfText.
            if (this.pdfChars.Count == 0)
            {
                this.pdfChars.Add(pdfChar);
                this.SetBeginPosInLine(posInLine);
                this.SetType(pdfChar.GetType());
                return JoiningStatus.JOINED;
            }

            //  When type of PdfText isn't like type of pdfChar
            if ((this.type != pdfChar.GetType()))
            {
                return JoiningStatus.INCOMPATIBLE_TYPE;
            }

            //  When type of PdfText is like type pdfChar
            int relativePos = (posInLine - this.beginPosInLine);
            int start = 0;
            int end = (this.pdfChars.Count - 1);
            if (((start <= relativePos)
                        && (relativePos <= end)))
            {
                //  When PdfChar's position is located in array
                //TODO
                this.pdfChars[relativePos] = pdfChar;
                return JoiningStatus.JOINED;
            }
            else if (((this.type == Common.TypeChar.HALF_SIZE)
                        && (relativePos
                        <= (end + 5))))
            {
                //  When type of PdfText is HALF_SIZE and
                //  number of invisible character (space or '\0') between PdfText's end position and PdfChar's
                //  position is 4, join PdfChar
                for (int i = (end + 1); (i < relativePos); i++)
                {
                    //  Add space
                    this.pdfChars.Add(PdfChar.Space());
                }

                this.pdfChars.Add(pdfChar);
                return JoiningStatus.JOINED;
            }
            else if (((this.type == Common.TypeChar.FULL_SIZE)
                        && (relativePos
                        == (end + 1))))
            {
                //  When type of PdfText is FULL_SIZE and
                //  PdfChar's position is next PdfText's end position, join PdfChar
                this.pdfChars.Add(pdfChar);
                return JoiningStatus.JOINED;
            }

            //  Else, status is out of range
            return JoiningStatus.OUT_OF_RANGE;
        }

        public bool IsEmpty()
        {
            return this.pdfChars.Count == 0;
        }

        public string GetWritableData()
        {
            StringBuilder sb = new StringBuilder("");
            //  Format PdfChar to create data to write into PDF file
            foreach (PdfChar pdfChar in this.pdfChars)
            {
                sb.Append(pdfChar.ToHexFormat());
            }

            return String.Format(FORMAT_TEXT, sb.ToString());
        }
    }
}
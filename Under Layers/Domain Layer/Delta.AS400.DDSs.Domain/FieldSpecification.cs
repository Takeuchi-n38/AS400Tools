using Delta.AS400.DataTypes.Characters;

namespace Delta.AS400.DDSs
{
    public class FieldSpecification
    {
        FieldSpecification(byte[] aCCSID930Bytes, int startPositionInLine, int endPositionInLine)
        {
            CCSID930Bytes = aCCSID930Bytes;
            StartPositionInLine = startPositionInLine;
            EndPositionInLine = endPositionInLine;
        }

        public readonly byte[] CCSID930Bytes;
        public readonly int StartPositionInLine;
        public readonly int EndPositionInLine;

        public bool IsRightJustified => StartPositionInLine == -1;
        public int StartIndexInLine => IsRightJustified ? EndPositionInLine - CCSID930Bytes.Length : StartPositionInLine - 1;

        public static FieldSpecification OfLeftJustified(byte[] aCCSID930Bytes, int startPositionInLine)
        {
            return new FieldSpecification(aCCSID930Bytes, startPositionInLine, -1);
        }
        public static FieldSpecification OfRightJustified(byte[] aCCSID930Bytes, int endPositionInLine)
        {
            return new FieldSpecification(aCCSID930Bytes, -1, endPositionInLine);
        }

        public string ValuesToString => CodePage930.ToStringFrom(CCSID930Bytes);

    }
}

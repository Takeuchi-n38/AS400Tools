using Delta.AS400.Libraries;

namespace Delta.Tools.AS400.Programs.CLs.Lines.Libs
{
    public class RmvlibleLine : CLLine
    {
        public readonly Library LIB;
        public RmvlibleLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            LIB = library;
        }

    }
}
//RMVLIBLE LIB(UT400IPDC)
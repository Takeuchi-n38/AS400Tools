using Delta.AS400.Libraries;

namespace Delta.Tools.AS400.Programs.CLs.Lines.Libs
{
    public class AddlibleLine : CLLine
    {
        public readonly Library LIB;

        public AddlibleLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            LIB = library;
        }

    }
}
//ADDLIBLE   LIB(UT4IPDATO)
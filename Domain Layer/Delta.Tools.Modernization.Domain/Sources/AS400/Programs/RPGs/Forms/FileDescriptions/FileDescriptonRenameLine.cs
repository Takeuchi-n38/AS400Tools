using Delta.Tools.Sources.Statements.Singles;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptonRenameLine : FileDescriptionLine, ISingleStatement
    {
        public readonly string Rename;
        public FileDescriptonRenameLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {
            //F RENAME(TKMSTRR:T4MSTR)
            var clipped = TextClipper.ClipParameter(value, "RENAME");
            Rename = clipped.Split(':')[1];
        }

    }
}

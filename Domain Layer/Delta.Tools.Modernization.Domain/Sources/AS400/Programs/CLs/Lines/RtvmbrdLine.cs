using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RtvmbrdLine : CLLine
    {
        //public List<OperatedFile> OperatedFiles => new List<OperatedFile>() { OperatedFile.Of(FileSource, FileOperations.Read) };

        public readonly ObjectID FileObjectID;
        public readonly string Nbrcurrcd;

        public RtvmbrdLine(ObjectID FileObjectID, string Nbrcurrcd,string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
            this.Nbrcurrcd = Nbrcurrcd;
        }
    }
}


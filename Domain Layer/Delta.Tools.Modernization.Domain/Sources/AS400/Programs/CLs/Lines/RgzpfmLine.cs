using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RgzpfmLine : CLLine
    {

        //public List<OperatedFile> OperatedFiles => new List<OperatedFile>() { OperatedFile.Of(FileSource, FileOperations.Vacuum) };
        //public List<ObjectID> OperatedFiles => new List<ObjectID>() { FileSource.ObjectID };

        public readonly ObjectID FileObjectID;

        public RgzpfmLine(ObjectID FileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
        }

    }
}
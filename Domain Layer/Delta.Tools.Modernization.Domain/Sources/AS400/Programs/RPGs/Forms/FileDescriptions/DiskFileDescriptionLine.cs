using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Sources.Statements.Singles;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class DiskFileDescriptionLine : FileDescriptionLine, ISingleStatement
    {

        readonly string Keywords;
        public readonly bool IsCycle;
        public readonly bool IsA;
        public string Prefix => TextClipper.ClipParameter(Keywords, "PREFIX");


        public DiskFileDescriptionLine(ObjectID FileObjectID, string fileName, string fileType, bool isCycle,bool isA, string fileFormat, string RecordLength, string keywords, string line, int originalLineStartIndex, int originalLineEndIndex) : base(fileName, fileType, fileFormat, RecordLength, line, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;

            IsCycle = isCycle;
            IsA=isA;
            Keywords = keywords;
        }

        //internal PFStructure FileDifintion => ((ExternalFormatFileStructure)File).FileDifintion;

        public string RecordFormatReName { get; set; } = string.Empty;

        public ObjectID FileObjectID;

        public IStructure File { get;set;}

        public bool IsAddableProgramDescriptionFile => Value.Length<=65? false : Value.Substring(65,1)=="A";

    }
}

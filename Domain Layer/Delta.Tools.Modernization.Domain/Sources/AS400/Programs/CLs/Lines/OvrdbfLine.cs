using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class OvrdbfLine : CLLine
    {

        //public List<OperatedFile> OperatedFiles => new List<OperatedFile>() { OperatedFile.Of(FromFileSource, FileOperations.Alias), OperatedFile.Of(ToFile.OriginalSource, FileOperations.UsedByAlias) };
        //public List<ObjectID> OperatedFiles => new List<ObjectID>() { FromFileSource.ObjectID , ToFile.OriginalSource.ObjectID};

        //public readonly Source FromFileSource;
        //public readonly IStructure ToFile;

        //internal OvrdbfLine(Source FromFileSource, IStructure ToFile, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        //{
        //    this.FromFileSource = FromFileSource;
        //    this.ToFile = ToFile;
        //}

        public readonly ObjectID FromFileObjectID;
        public readonly ObjectID ToFileObjectID;

        public OvrdbfLine(ObjectID FromFileSource, ObjectID ToFileSource, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            FromFileObjectID = FromFileSource;
            ToFileObjectID = ToFileSource;
        }

    }
}
//OVRDBF FILE(INPUT) TOFILE(SALELIB/UPURIB)
using System;
using Delta.Tools.Sources.Statements.Singles;
using Delta.AS400.Objects;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class WorkstnFileDescriptionLine : FileDescriptionLine, ISingleStatement
    {
        public readonly ObjectID FileObjectID;

        WorkstnFileDescriptionLine(ObjectID FileObjectID, string fileName, string fileType, string fileFormat, string line, int originalLineStartIndex, int originalLineEndIndex) : base(fileName, fileType, fileFormat,string.Empty, line, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
        }

        public static WorkstnFileDescriptionLine Create(ObjectID objectID, string fileName, string fileType, string fileFormat, string line, int originalLineStartIndex, int originalLineEndIndex)
        {
            return new WorkstnFileDescriptionLine(objectID, fileName, fileType, fileFormat, line, originalLineStartIndex, originalLineEndIndex);
        }

    }
}
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptionLine : Line, IRPGLine
    {
        FormType IRPGLine.FormType => FormType.FileDescription;

        public readonly string FileName;

        public readonly string FileType;

        public readonly string FileFormat;

        public readonly string RecordLength;

        public bool IsExternalFileFormat => FileFormat == "E";

        public FileDescriptionLine(string fileName, string fileType, string fileFormat, string recordLength, string line, int originalLineStartIndex, int originalLineEndIndex) : base(line, originalLineStartIndex, originalLineEndIndex)
        {
            FileName = fileName;
            FileType = fileType;
            FileFormat = fileFormat;
            this.RecordLength = recordLength;
        }

        public FileDescriptionLine(string line, int originalLineStartIndex, int originalLineEndIndex) : base(line, originalLineStartIndex, originalLineEndIndex)
        {

        }
        public FileDescriptionLine(string line, int originalLineStartIndex) : this(line, originalLineStartIndex, originalLineStartIndex)
        {

        }

    }
}
/*
     FTKMSTR    UF A E           K DISK    PREFIX(TK)
     FKBMSTR    IF   E           K DISK    PREFIX(KB)
     FTKMSTR04  IF   E           K DISK    PREFIX(T4)
     F                                     RENAME(TKMSTRR:T4MSTR)
     FSIMEFL    IF   E           K DISK    PREFIX(SI)
     FCDMSY     IF   E           K DISK
     FCDMTR     IF   E           K DISK
     FPQEA500D  CF   E             WORKSTN
     F                                     SFILE(PNL010:RRN)
     F                                     INFDS(WINFDS)
     */

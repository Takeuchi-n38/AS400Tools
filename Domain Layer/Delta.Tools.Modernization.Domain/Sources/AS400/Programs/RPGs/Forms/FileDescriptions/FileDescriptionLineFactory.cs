using Delta.AS400.Objects;
using System;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public abstract class FileDescriptionLineFactory
    {
        virtual protected int FileTypeStartIndex { get; }
        virtual protected int FileFormatStartIndex { get; }

        abstract protected string RecordLength(string line);

        virtual protected int DeviceStartIndex { get; }

        public FileDescriptionLine Create(ObjectID objectIDofSource, string line, int originalLineStartIndex, int originalLineEndIndex)
        {
            if (line.Substring(6, 1) == "*") throw new NotImplementedException();//return  new FileDescriptionCommentLine(line, originalLineStartIndex, originalLineEndIndex);

            var fileName = string.Empty;
            var fileType = string.Empty;
            var fileFormat = string.Empty;
            var recoreLength = string.Empty;
            var keywords = string.Empty;
            if (line.Length >= FileTypeStartIndex)
            {
                fileFormat = line.Substring(FileFormatStartIndex, 1);
                fileName = line.Substring(6, FileTypeStartIndex - 6).TrimEnd();
                fileType = line.Substring(FileTypeStartIndex, 1);
                recoreLength=RecordLength(line);
                keywords = line.Length > DeviceStartIndex + 8 ? line.Substring(DeviceStartIndex + 8) : string.Empty;
            }

            var objectID = objectIDofSource.CreateWithSameLibrary(fileName);

            if (line.Length >= DeviceStartIndex + 4 && line.Substring(DeviceStartIndex, 4) == "DISK")
            {
                var isCycle = line.Substring(FileTypeStartIndex + 1, 1) == "P";
                var isA = line.Length>65?line.Substring(65, 1) == "A":false;
                return new DiskFileDescriptionLine(objectID, fileName, fileType, isCycle, isA, fileFormat, recoreLength, keywords, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.Length >= DeviceStartIndex + 7 && line.Substring(DeviceStartIndex, 7) == "WORKSTN")
            {
                return WorkstnFileDescriptionLine.Create(objectID, fileName, fileType, fileFormat, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.Length >= DeviceStartIndex + 7 && line.Substring(DeviceStartIndex, 7) == "PRINTER")
            {
                return new PrinterFileDescriptionLine(objectID, fileName, fileType, fileFormat, recoreLength, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.Contains("RENAME("))
            {
                return new FileDescriptonRenameLine(line, originalLineStartIndex);
            }

            return new UnKnownFileDescriptionLine(line, originalLineStartIndex, originalLineEndIndex);
        }

    }
}

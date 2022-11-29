using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptionBlock : IBlockStatement<IStatement>
    {
        public override void Add(IStatement element)
        {
            if (element is FileDescriptonRenameLine fileDescriptonRenameLine)
            {
                var DiskFileDescriptionLine = (DiskFileDescriptionLine)Statements.Last();
                DiskFileDescriptionLine.RecordFormatReName = fileDescriptonRenameLine.Rename;
            }

            Statements.Add(element);
        }

        public IEnumerable<DiskFileDescriptionLine> DiskFileDescriptionLines => Statements.Where(e => e is DiskFileDescriptionLine).Cast<DiskFileDescriptionLine>();

        public DiskFileDescriptionLine? CycleFileLine => DiskFileDescriptionLines.Where(e => e.IsCycle).FirstOrDefault();

        public IEnumerable<DiskFileDescriptionLine> ExternalDiskFileDescriptionLines => DiskFileDescriptionLines.Where(l=>l.IsExternalFileFormat);
        public IEnumerable<DiskFileDescriptionLine> InternalFileDescriptionLines => DiskFileDescriptionLines.Where(l=>!l.IsExternalFileFormat);
        public IEnumerable<(string FileName,int LineLength)> InternalFileNameAndLineLengths => InternalFileDescriptionLines.Select(l => (l.FileName,int.Parse(l.RecordLength)));

        public IEnumerable<ObjectID> InternalFileObjectID => InternalFileDescriptionLines.Select(l=>l.FileObjectID);

            
        public IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames => ExternalDiskFileDescriptionLines.SelectMany(e => DiskFileDescriptionLinePrefixedFieldNameModernTypeNames(e));

        static IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> DiskFileDescriptionLinePrefixedFieldNameModernTypeNames(DiskFileDescriptionLine diskFileDescriptionLine)
            => diskFileDescriptionLine.File is ExternalFormatFileStructure fileStructure ?
            fileStructure.RecordFormatFields.Select(r => (r, diskFileDescriptionLine.Prefix + r.Name)) : new List<(IDDSLine RecordFormatFieldLine, string Name)>();

    }
}

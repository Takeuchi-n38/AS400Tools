using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DiskFiles.LFs
{
    public class RecordFormatPfileKeywordLine : RecordFormatKeywordLine
    {
        public readonly PFStructure ReferDisk;
        public RecordFormatPfileKeywordLine(string value, int originalLineStartIndex, IStructure referDisk) : base(value, originalLineStartIndex)
        {
            ReferDisk = (PFStructure)referDisk;
        }

    }
}

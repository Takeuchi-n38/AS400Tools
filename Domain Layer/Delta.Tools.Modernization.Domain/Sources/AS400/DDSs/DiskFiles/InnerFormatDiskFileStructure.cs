using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DiskFiles
{
    public class InnerFormatDiskFileStructure : IStructure
    {
        public Source OriginalSource { get; }
        internal InnerFormatDiskFileStructure(ObjectID objectID)
        {
            OriginalSource = Source.NullValue(objectID);
        }
        public static InnerFormatDiskFileStructure Of(ObjectID objectID)
        {
            return new InnerFormatDiskFileStructure(objectID);
        }

    }
}

using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.PrinterFiles
{
    public class InnerFormatPRTStructure : IStructure
    {
        public Source OriginalSource { get; }
        internal InnerFormatPRTStructure(ObjectID objectID)
        {
            OriginalSource = Source.NullValue(objectID);
        }

        public static InnerFormatPRTStructure Of(ObjectID objectID)
        {
            return new InnerFormatPRTStructure(objectID);
        }

    }
}

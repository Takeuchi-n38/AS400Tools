using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.TXTs
{
    public class TXTStructure : IStructure
    {
        public Source OriginalSource { get; }

        public TXTStructure(Source source)
        {
            OriginalSource = source;
        }

        public TXTStructure(ObjectID objectID) : this(Source.Of(objectID, new string[] { $"{objectID.Name}.TXT" }))
        {

        }

    }
}

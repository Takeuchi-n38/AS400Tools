using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Structures
{
    public interface IStructure
    {
        public Source OriginalSource { get; }

        public ObjectID ObjectID => OriginalSource.ObjectID;

        public string Description => OriginalSource.Description;

    }
}

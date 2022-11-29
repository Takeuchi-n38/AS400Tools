using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Sources
{
    public interface ISourceFactory
    {
        bool SourceExists(ObjectID objectID);

        Source Read(ObjectID objectID);
    }
}

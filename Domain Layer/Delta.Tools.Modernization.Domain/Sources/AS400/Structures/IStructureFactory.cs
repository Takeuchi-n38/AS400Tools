using Delta.Tools.AS400.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Structures
{
    public interface IStructureFactory
    {
        IStructure Create(Source source);
    }
}

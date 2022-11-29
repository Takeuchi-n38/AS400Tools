using Delta.AS400.Objects;
using Delta.AS400.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.DDSs.DiskFiles
{
    public interface IDiskFileStructureFactory
    {
        Structure Create(ObjectID objectID);

    }
}

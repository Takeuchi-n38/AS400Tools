using Delta.AS400.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Delta.Tools.AS400.Configs
{
    public interface IAnalyzerConfig: IToolConfig
    {

        List<ObjectID> EntryObjectIDs();
        
        List<ObjectID> CheckedObjectIDs();

    }
}

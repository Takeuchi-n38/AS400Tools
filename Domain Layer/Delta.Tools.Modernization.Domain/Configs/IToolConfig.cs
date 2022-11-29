using Delta.Tools.AS400.Libraries;
using System.Collections.Generic;
using System.IO;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Modernization;
using Delta.AS400.DataAreas;
using Delta.Tools.Modernization;
using System;

namespace Delta.Tools.AS400.Configs
{
    public interface IToolConfig
    {

        string ModernaizationRootDirectoryPath => $"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta";

        Library MainLibrary { get; }

        PathResolverForTool PathResolver => PathResolverForTool.Of(ModernaizationRootDirectoryPath,MainLibrary);

        List<Library> LibraryList { get; }

        LibraryFactory LibraryFactory { get; }

        List<ObjectID> ReCreateFileObjectIDs()
        {
            List<ObjectID> reCreateFileObjectIDs = new List<ObjectID>();
            return reCreateFileObjectIDs;
        }

        List<DataArea> DataAreas()
        {
            return new List<DataArea>();
        }
    }
}

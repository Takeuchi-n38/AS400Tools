using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;
using System.Collections.Generic;

namespace Delta.Tools.AS400.TXTs
{
    public class ReCreateTXTList
    {
        public ReCreateTXTList(List<ObjectID> ReCreateFileObjectIDs)
        {
            List<ObjectID> reCreateFileObjectIDs = ReCreateFileObjectIDs;

            reCreateFileObjectIDs.ForEach(reCreateFileObjectID => reCreateFiles.Add(reCreateFileObjectID, new TXTStructure(reCreateFileObjectID)));

        }

        Dictionary<ObjectID, IStructure> reCreateFiles = new Dictionary<ObjectID, IStructure>();

        public IStructure ReCreate(ObjectID objectID)
        {

            if (!reCreateFiles.ContainsKey(objectID))
            {
                var reCreateFile = new TXTStructure(objectID);
                reCreateFiles.Add(objectID, reCreateFile);
            }

            return reCreateFiles[objectID];

        }

        public IStructure Find(ObjectID objectID)
        {
            IStructure structure = null;
            if (reCreateFiles.TryGetValue(objectID, out structure))
            {
                return structure;
            }
            return null;
        }

    }
}

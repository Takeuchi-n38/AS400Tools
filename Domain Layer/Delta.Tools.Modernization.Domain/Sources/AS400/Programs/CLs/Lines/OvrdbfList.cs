using Delta.AS400.Objects;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class OvrdbfList
    {
        static public OvrdbfList Instance = new OvrdbfList();
        OvrdbfList()
        {

        }

        Dictionary<string, ObjectID> toFiles = new Dictionary<string, ObjectID>();

        public void Override(ObjectID FromFileObjectID, ObjectID ToFileObjectID)
        {
            var from = FromFileObjectID.Name;
            var toFile = ToFileObjectID;

            if (toFiles.ContainsKey(from))
            {
                toFiles[from] = toFile;
            }
            else
            {
                toFiles.Add(from, toFile);
            }

        }

        public ObjectID Find(string from)
        {
            ObjectID structure = null;
            if (toFiles.TryGetValue(from, out structure))
            {
                return structure;
            }
            return null;
        }


        public void DeleteAll()
        {
            toFiles.Clear();
        }

    }
}

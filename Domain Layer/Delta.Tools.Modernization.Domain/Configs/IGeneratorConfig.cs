using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Configs
{
    public interface IGeneratorConfig: IToolConfig
    {

        List<ObjectID> GenerateCLObjectIDs();

        List<ObjectID> GenerateRPG4ObjectIDs();

        List<ObjectID> GenerateRPG3ObjectIDs();

        public List<ObjectID> GenerateCOBOLObjectIDs()
        {
            return new List<ObjectID>();
        }

        List<ObjectID> NoGenerateServiceObjectIDs();

        List<Library> LibrariesOfDB2foriRepository();

        public ObjectID MapRealFile(ObjectID objectIDOfRPG,ObjectID objectIDOfFile)
        {
            return objectIDOfFile;
        }
    }

}

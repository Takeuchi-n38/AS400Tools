using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public class DSPStructureFactory : IStructureFactory
    {
        DSPStructureFactory()
        {
        }

        public static DSPStructureFactory Of()
        {
            return new DSPStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            return new DSPStructure(source);
        }

    }
}

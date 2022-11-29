using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.TXTs
{
    public class TXTStructureFactory : IStructureFactory
    {
        TXTStructureFactory()
        {
        }
        public static TXTStructureFactory Of()
        {
            return new TXTStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            return new TXTStructure(source);
        }
    }

}

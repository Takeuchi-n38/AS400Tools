using Delta.Tools.AS400.Analyzer.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.RPGs
{
    public class RPGStructureFactory3 : IStructureFactory
    {
        RPGStructureFactory3()
        {
        }
        public static RPGStructureFactory3 Of()
        {
            return new RPGStructureFactory3();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            var s = new RPGStructure3(source);
            RPGStructureFactory.CreateRPGLines(source, RPGLineFactory3.Instance, s);
            return s;
        }
    }
}

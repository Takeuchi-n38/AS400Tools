using Delta.Tools.AS400.Analyzer.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.RPGs
{
    public class RPGStructureFactory4 : IStructureFactory
    {
        RPGStructureFactory4()
        {
        }
        public static RPGStructureFactory4 Of()
        {
            return new RPGStructureFactory4();
        }
        IStructure IStructureFactory.Create(Source source)
        {
            var s = new RPGStructure4(source);
            RPGStructureFactory.CreateRPGLines(source, RPGLineFactory4.Instance, s);
            return s;
        }
    }
}


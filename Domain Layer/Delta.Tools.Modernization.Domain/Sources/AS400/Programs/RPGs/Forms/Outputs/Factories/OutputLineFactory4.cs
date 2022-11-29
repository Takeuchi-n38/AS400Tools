using Delta.Tools.AS400.Programs.RPGs.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs.Factories
{
    public class OutputLineFactory4 : IOutputLineFactory
    {
        IRPGLine IOutputLineFactory.Create(string line, int originalLineStartIndex)
        {
            return new RPGLine4(FormType.Output, line, originalLineStartIndex);
        }
    }
}
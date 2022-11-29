using Delta.Tools.AS400.Programs.RPGs.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs.Factories
{
    public class OutputLineFactory3 : IOutputLineFactory
    {
        IRPGLine IOutputLineFactory.Create(string line, int originalLineStartIndex)
        {
            return new RPGLine3(FormType.Output, line, originalLineStartIndex);
        }
    }
}

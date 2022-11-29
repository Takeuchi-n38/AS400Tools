using Delta.Tools.AS400.Programs.RPGs.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs.Factories
{
    public interface IOutputLineFactory
    {
        IRPGLine Create(string line, int originalLineStartIndex);

    }
}

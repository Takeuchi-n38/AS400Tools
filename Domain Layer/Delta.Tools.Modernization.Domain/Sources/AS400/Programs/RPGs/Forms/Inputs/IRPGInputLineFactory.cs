namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public interface IRPGInputLineFactory
    {
        IRPGInputLine Create(string line, int originalLineStartIndex);

    }
}

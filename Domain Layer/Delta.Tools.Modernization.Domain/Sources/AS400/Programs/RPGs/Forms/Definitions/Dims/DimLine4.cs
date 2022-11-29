using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims
{
    public class DimLine4 : RPGLine4, ISingleStatement
    {

        public readonly int Size;

        public DimLine4(int size,IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {
            Size=size;
        }
    }
}
//0016      D ARQTY           S              9  2 DIM(8) 
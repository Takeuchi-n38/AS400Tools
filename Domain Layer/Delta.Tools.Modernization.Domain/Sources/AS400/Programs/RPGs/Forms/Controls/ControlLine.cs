using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Controls
{
    public class ControlLine : Line, IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Control;

        public ControlLine(string value, int originalLineStartIndex) : base(value.Length < 76 ? value.PadRight(76) : value, originalLineStartIndex)
        {
        }

    }
}
//0000      H DATEDIT(*YMD/)  日付編集様式がＹＭＤで、その仕切り文字が／
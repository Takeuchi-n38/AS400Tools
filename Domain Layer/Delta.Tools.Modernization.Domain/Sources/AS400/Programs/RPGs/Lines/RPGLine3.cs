using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.Sources.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Lines
{
    public class RPGLine3 : Line, IRPGLine3
    {

        readonly FormType FormType;
        FormType IRPGLine.FormType => FormType;

        public static int MaxLengthOfLine = 70;

        protected RPGLine3(FormType FormType,string value, int originalLineStartIndex, int originalLineEndIndex) : base(value.Length < MaxLengthOfLine ? value.PadRight(MaxLengthOfLine) : value.Substring(0,70), originalLineStartIndex, originalLineEndIndex)
        {
            this.FormType= FormType;
        }

        public RPGLine3(FormType FormType, string value, int originalLineStartIndex) : this(FormType, value, originalLineStartIndex, originalLineStartIndex)
        {

        }
    }
}

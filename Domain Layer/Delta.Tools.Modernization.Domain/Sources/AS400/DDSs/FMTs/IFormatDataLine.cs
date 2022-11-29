using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.DDSs.FMTs
{
    public interface IFormatDataLine : ILine
    {
        string LineType => Value.Substring(5, 1);

        bool IsCommentLine => LineType == "*";

        bool IsHeaderLine => LineType == "H";

        bool IsIncludeRecordLine => LineType == "I";
        bool IsExcludeRecordLine => LineType == "O";

        bool IsRecordLine => IsIncludeRecordLine || IsExcludeRecordLine;

        bool IsFieldLine => LineType == "F";

    }
}

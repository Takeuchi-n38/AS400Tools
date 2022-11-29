using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public class DSPStructure : IStructure
    {
        public Source OriginalSource { get; }
        public DSPStructure(Source source)
        {
            OriginalSource = source;
        }

    }
}

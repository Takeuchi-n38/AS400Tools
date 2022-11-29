using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.DDSs
{
    public abstract class DdsStructure : IStructure
    {
        public Source OriginalSource { get; }
        public ObjectID ObjectID => OriginalSource.ObjectID;

        protected DdsStructure(Source source)
        {
            OriginalSource = source;
        }

    }
}

using Delta.AS400.Objects;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Sources;
using System.Text;

namespace Delta.Tools.AS400.Structures
{
    public class NotFoundSourceStructure : IStructure
    {
        public Source OriginalSource { get; }

        NotFoundSourceStructure(ObjectID objectID)
        {
            OriginalSource = Source.NullValue(objectID);
        }
        public static NotFoundSourceStructure Of(ObjectID objectID)
        {
            return new NotFoundSourceStructure(objectID);
        }

    }
}

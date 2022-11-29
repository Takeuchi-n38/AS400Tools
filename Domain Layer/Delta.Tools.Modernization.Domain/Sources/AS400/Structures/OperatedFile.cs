using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Statements.Singles;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Structures
{
    public class OperatedFile : ISingleStatement
    {
        public bool OriginalSourceIsNotFound => OriginalSource.IsNotFound;
        public ObjectID OriginalSourceObjectID => OriginalSource.ObjectID;
        readonly Source OriginalSource;
        readonly FileOperations Operations;

        OperatedFile(Source OriginalSource, FileOperations operations)
        {
            this.OriginalSource = OriginalSource;
            this.Operations = operations;
        }

        public static OperatedFile Of(Source OriginalSource, FileOperations operations)
        {
            return new OperatedFile(OriginalSource, operations);
        }

    }
}

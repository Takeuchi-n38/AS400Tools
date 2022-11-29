using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Structures.Programs
{
    public abstract class ProgramStructure : IStructure
    {
        public Source OriginalSource { get; }

        public virtual ObjectID? WorkstationFileObjectID => null;

        public List<IStatement> Elements;

        protected ProgramStructure(Source source, List<IStatement> PgmLines)
        {
            OriginalSource = source;
            Elements = PgmLines;
        }


        public ObjectID ObjectID => OriginalSource.ObjectID;

    }
}

using Delta.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Singles;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.CLs
{
    public class CLStructure : ProgramStructure
    {
        public IEnumerable<T> FindLines<T>() => Elements.Where(l => l is T).Cast<T>();

        public bool IsSndpgmmsg => Elements.Count(pl => pl is SndpgmmsgLine) > 0;
        public bool IsSndmsg => Elements.Count(pl => pl is SndmsgLine) > 0;
        public bool IsCalling => ThisCallerProgramLines.Count() > 0;

        public IEnumerable<IStatement> ThisCallerProgramLines => callerPgmLinesByCall.Concat(callerPgmLinesBySbmjob).Concat(callerPgmLinesBySbmrmtcmd);
        IEnumerable<IStatement> callerPgmLinesByCall => Elements.Where(pl => IsProgramCallStatement(pl));
        IEnumerable<IStatement> callerPgmLinesBySbmjob => Elements.Where(pl => pl is SbmjobLine && IsProgramCallStatement(((SbmjobLine)pl).Command)).Select(pl => ((SbmjobLine)pl).Command);
        IEnumerable<IStatement> callerPgmLinesBySbmrmtcmd => Elements.Where(pl => pl is SbmrmtcmdLine && IsProgramCallStatement(((SbmrmtcmdLine)pl).Command)).Select(pl => ((SbmrmtcmdLine)pl).Command);

        bool IsProgramCallStatement(IStatement line) => line is AutostartLine || line is CallLine;

        public int CountOfDCLF => Elements.Count(line => line is DclfLine);

        public override ObjectID? WorkstationFileObjectID => Elements.Where(line => line is DclfLine).Select(line => ((DclfLine)line).FileObjectID).FirstOrDefault();

        public IEnumerable<string> DtaaraNames => Elements.Where(pl => pl is RtvdtaaraLine).Select(pl=>((RtvdtaaraLine)pl).DtaaraName);

        public CLStructure(Source source, List<IStatement> pgmLines) : base(source, pgmLines)
        {
            CommentCount = pgmLines.Count(element => element is ICommentStatement || element is IEmptyStatement);
        }

        public readonly int CommentCount;

    }
}

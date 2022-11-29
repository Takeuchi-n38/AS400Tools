using Delta.Tools.AS400.DDSs.DisplayFiles.Commands;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public class DisplayFileStructure : IStructure
    {
        public IEnumerable<AttentionCommand> AllAttentionCommands()
        {
            return AttentionCommands.Concat(RecordFormatHeaderList.SelectMany(h => h.AttentionCommands)).Distinct();
        }
        public IEnumerable<Indicator> AllIndicatorsInAttentionCommand()
        {
            return AllAttentionCommands().Where(cmd=>cmd.HasIndicator).Select(cmd=>cmd.Indicator).Distinct();
        }
        public IEnumerable<FunctionCommand> AllFunctonCommands()
        {
            return FunctionCommands.Concat(RecordFormatHeaderList.SelectMany(h => h.FunctionCommands)).Distinct();
        }
        public IEnumerable<Indicator> AllIndicatorsInFunctonCommand()
        {
            return AllFunctonCommands().Where(cmd => cmd.HasIndicator).Select(cmd => cmd.Indicator).Distinct();
        }

        List<AttentionCommand> AttentionCommands;
        List<FunctionCommand> FunctionCommands;

        public List<RecordFormatHeader> RecordFormatHeaderList;

        public SubFileControlRecordFormatHeader SubFileControlRecordForamtHeader=> RecordFormatHeaderList.Where(r=>r is SubFileControlRecordFormatHeader).Cast<SubFileControlRecordFormatHeader>().FirstOrDefault();

        public Source OriginalSource { get; }
        public DisplayFileStructure(Source source, List<AttentionCommand> attentionCommands, List<FunctionCommand> functionCommands, List<RecordFormatHeader> RecordFormatHeaderList)
        {
            OriginalSource = source;
            this.AttentionCommands = attentionCommands;
            this.FunctionCommands = functionCommands;
            this.RecordFormatHeaderList = RecordFormatHeaderList;
        }
    }
}

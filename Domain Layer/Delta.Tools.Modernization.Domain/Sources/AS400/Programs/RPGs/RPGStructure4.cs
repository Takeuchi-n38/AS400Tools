using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs
{
    public class RPGStructure4 : RPGStructure
    {

        public RPGStructure4(Source source) : base(source)
        {

        }

        protected override void ReformInputBlock()
        {
            for (var i = 0; i < InputBlock.Statements.Count; i++)
            {
                var rpgLine = InputBlock.Statements[i];
                if (rpgLine is IRPGInputLine4 && ((IRPGInputLine4)rpgLine).IsNestedBlockStartStatement)
                {
                    NestedBlockStatement<IStatement> block = new ProgramDescribedInputFile((IRPGInputLine4)rpgLine);
                    InputBlock.Statements.RemoveAt(i);
                    while (i < InputBlock.Statements.Count)
                    {
                        var nextLine = InputBlock.Statements[i];
                        if (block.TryClose(nextLine))
                        {
                            InputBlock.Statements.Insert(i, block);
                            break;
                        }
                        else
                        {
                            block.Add(nextLine);
                            InputBlock.Statements.RemoveAt(i);
                            if (i == InputBlock.Statements.Count)
                            {
                                InputBlock.Statements.Insert(i, block);
                                break;
                            }
                        }
                    }

                }
            }

        }

        protected override void ReformProgramDataBlock()
        {

            var dimLineIndex = -1;

            for (var i = 0; i < ProgramDataBlock.Statements.Count; i++)
            {

                var rpgLine = (ILine)ProgramDataBlock.Statements[i];

                if (rpgLine.Value.StartsWith("**"))
                {
                    dimLineIndex += 1;
                }
                else
                {
                    if (rpgLine is ICommentStatement) continue;

                    if(DefinitionBlock.DimCtdataLines.Count()> dimLineIndex)
                    {
                        DefinitionBlock.DimCtdataLines.ElementAt(dimLineIndex).Add(rpgLine.Value);
                    }
                    else
                    {
                        DefinitionBlock.DimCtdataLines.Last().Add(rpgLine.Value);
                    }
                }
            }

        }

    }
}

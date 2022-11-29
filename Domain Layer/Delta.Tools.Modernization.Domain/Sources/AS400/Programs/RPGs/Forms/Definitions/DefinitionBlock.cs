using Delta.AS400.DataTypes;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class DefinitionBlock : IBlockStatement<IStatement>
    {
        public DefinitionBlock()
        {
        }

        public IEnumerable<IDataTypeDefinition> TypeDefinitions(string rpgName)
        {
            var typeDefinitions = new List<IDataTypeDefinition>();
            var prBlock = Statements.Where(s => s is PrBlock && ((PrBlock)s).IsPrameter(rpgName)).Cast<PrBlock>().FirstOrDefault();
            if (!(prBlock is null))
            {
                var Variables = prBlock.Statements.Where(prElement => prElement is DifinitionBlockItemLine).Cast<IDataTypeDefinition>();

                typeDefinitions.AddRange(Variables);
            }
            return typeDefinitions;
        }

        public IEnumerable<DimCtdataLine4> DimCtdataLines => Statements.Where(el => el is DimCtdataLine4).Cast<DimCtdataLine4>();

        public void Reform()
        {
            GatherBlock();
        }

        void GatherBlock()
        {
            for (var i = 0; i < Statements.Count; i++)
            {
                var rpgLine = Statements[i];
                if (rpgLine is INestedBlockStartStatement)
                {
                    NestedBlockStatement<IStatement> block = DifinitionBlockFactory.Create((INestedBlockStartStatement)rpgLine);
                    Statements.RemoveAt(i);
                    while (i < Statements.Count)
                    {
                        var nextLine = Statements[i];
                        if (block.TryClose(nextLine))
                        {
                            Statements.Insert(i, block);
                            break;
                        }
                        else
                        {
                            block.Add(nextLine);
                            Statements.RemoveAt(i);
                            if (i == Statements.Count)
                            {
                                Statements.Insert(i, block);
                                break;
                            }
                        }
                    }

                }
            }

        }

    }
}

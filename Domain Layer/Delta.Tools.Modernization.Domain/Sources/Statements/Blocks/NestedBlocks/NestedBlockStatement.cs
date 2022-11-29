using System;

namespace Delta.Tools.Sources.Statements.Blocks.NestedBlocks
{
    public abstract class NestedBlockStatement<T> : IBlockStatement<T>
        where T : IStatement
    {

        public abstract bool TryClose(IStatement closerLine);

        public virtual bool Skip => false;

        public static void GatherNestedBlock(IBlockStatement<IStatement> originalBlock, Func<INestedBlockStartStatement, NestedBlockStatement<IStatement>> blockFactory)
        {
            for (var i = 0; i < originalBlock.Statements.Count; i++)
            {
                if (originalBlock.Statements[i] is INestedBlockStartStatement)
                {
                    var nestedBlock = GatherNestedBlock(originalBlock, i, blockFactory);
                    originalBlock.Statements.Insert(i, nestedBlock);
                }
            }
        }

        static NestedBlockStatement<IStatement> GatherNestedBlock(IBlockStatement<IStatement> originalBlock,
            int i, Func<INestedBlockStartStatement, NestedBlockStatement<IStatement>> blockFactory)
        {
            var openerStatement = (INestedBlockStartStatement)originalBlock.Statements[i];
            NestedBlockStatement<IStatement> nestedBlock = blockFactory(openerStatement);
            originalBlock.Statements.RemoveAt(i);
            while (i < originalBlock.Statements.Count)
            {
                var nextStatement = originalBlock.Statements[i];
                if (nestedBlock.TryClose(nextStatement))
                {
                    if(!nestedBlock.Skip)
                    {
                        originalBlock.Statements.RemoveAt(i);
                    }
                    return nestedBlock;
                }
                else if (nextStatement is INestedBlockStartStatement)
                {
                    var innerNestedBlock = GatherNestedBlock(originalBlock, i, blockFactory);
                    nestedBlock.Add(innerNestedBlock);
                }
                else
                {
                    nestedBlock.Add(nextStatement);
                    originalBlock.Statements.RemoveAt(i);
                }
            }
            return nestedBlock;
        }
    }
}

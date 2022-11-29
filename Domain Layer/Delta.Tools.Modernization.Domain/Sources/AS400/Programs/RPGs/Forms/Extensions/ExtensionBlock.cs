using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions.Dims;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public class ExtensionBlock : IBlockStatement<IStatement>
    {
        public void Reform()
        {
            GatherBlock();
        }


        void GatherBlock()
        {
            //for (var i = 0; i < Statements.Count; i++)
            //{
            //    var rpgLine = (IRPGDefinitionLine)Statements[i];
            //    if (rpgLine is INestedBlockStartStatement)
            //    {
            //        NestedBlockStatement<IStatement> block = DifinitionBlockFactory.Create((INestedBlockStartStatement)rpgLine);
            //        Statements.RemoveAt(i);
            //        while (i < Statements.Count)
            //        {
            //            var nextLine = Statements[i];
            //            if (block.TryClose(nextLine))
            //            {
            //                Statements.Insert(i, block);
            //                break;
            //            }
            //            else
            //            {
            //                block.Add(nextLine);
            //                Statements.RemoveAt(i);
            //                if (i == Statements.Count)
            //                {
            //                    Statements.Insert(i, block);
            //                    break;
            //                }
            //            }
            //        }

            //    }
            //}

        }

        public IEnumerable<DimCtdataLine3> DimCtdataLines => Statements.Where(el => el is DimCtdataLine3).Cast<DimCtdataLine3>();

    }
}

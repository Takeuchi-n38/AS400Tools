using Delta.AS400.DataTypes;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CalculationBlock : IBlockStatement<IStatement>
    {

        public override void Add(IStatement element)
        {
            if (element is DefineLine defineLine)
            {
                DefineLines.Add(defineLine);
            }
            else
            if (element is RPGCallLine rPGCallLine)
            {
                RPGCallLines.Add(rPGCallLine);
            }
            else
            if (element is ExfmtLine exfmtLine)
            {
                ExfmtLineNumbers.Add(exfmtLine.StartLineIndex);
            }
            Statements.Add(element);
        }

        public List<DefineLine> DefineLines { get; } = new List<DefineLine>();
        public List<RPGCallLine> RPGCallLines { get; } = new List<RPGCallLine>();
        public List<int> ExfmtLineNumbers { get; } = new List<int>();

        IBlockStatement<IStatement> CalculateBlock { get; set; }

        List<IBlockStatement<IStatement>> SubroutineBlocks { get; set; } = new List<IBlockStatement<IStatement>>();
        public bool IsExistInzsr => SubroutineBlocks.Where(x=>x is MethodBlockStatement).Cast<MethodBlockStatement>().Any(x=>x.IsInzsr);
        List<IBlockStatement<IStatement>> AllRoutineBlocks
        {
            get
            {
                var all = new List<IBlockStatement<IStatement>>();
                all.Add(CalculateBlock);
                all.AddRange(SubroutineBlocks);
                return all;
            }
        }

        public List<IDataTypeDefinition> TypeDefinitions = new List<IDataTypeDefinition>();
        internal void GatherTypeDefinitions()
        {
            for (var i = 0; i < Statements.Count; i++)
            {
                if (Statements[i] is ICommentStatement) continue;
                if (Statements[i] is ParmLine) continue;
                var rpgLine =(CalculationLine) Statements[i];
                int length = 0;
                if (rpgLine.FieldLength != string.Empty && int.TryParse(rpgLine.FieldLength, out length))
                {
                    var t = (IDataTypeDefinition)DataTypeDefinition.Of(rpgLine.ResultField, rpgLine.FieldLength, string.Empty, rpgLine.DecimalPositions);
                    TypeDefinitions.Add(t);
                }
            }
        }

        void AddParmLineToRPGCallLine()
        {
            for (var i = 0; i < Statements.Count; i++)
            {
                if (Statements[i] is RPGCallLine rpgCallLine && (Statements[i + 1] is ParmLine))
                {
                    var iOfP=1;
                    while (Statements[i + iOfP] is ParmLine parmLine)
                    {
                        rpgCallLine.AddCallerProgramParameterNames(parmLine.ResultField);
                        iOfP++;
                    }
                }
            }
        }

        public CalculationBlock()
        {

        }

        public void Reform()
        {
            if (Statements.Count == 0)
            {
                CalculateBlock = MethodBlockStatement.CalculateOf();
                Statements.Add(CalculateBlock);
                return;
            }

            GatherTypeDefinitions();
            AddParmLineToRPGCallLine();
            DevideBlocks();

            var ExfmtLineNumbers = CalculateBlock.Statements.Where(line => line is ExfmtLine).Cast<ExfmtLine>()
                        .Select(line => line.StartLineIndex).ToList();

            ExfmtLineNumbers.ForEach(l => ((MethodBlockStatement)CalculateBlock).AddExfmtLineNumber(l));

            AllRoutineBlocks.ForEach(block => NestedBlockStatement<IStatement>.GatherNestedBlock(block, NestedBlockFactory.Create));

        }

        void DevideBlocks()
        {
            for (var i = 0; i < Statements.Count; i++)
            {

                var rpgLine = Statements[i];

                if (CalculateBlock == null)
                {

                    if (rpgLine is KlistLine)
                    {
                        GatherKlistBlock(i);
                        continue;
                    }
                    if (rpgLine is PlistLine)
                    {
                        GatherParameters(i);
                        continue;
                    }
                    if (rpgLine is ICommentStatement)
                    {
                        continue;
                    }
                    if (rpgLine is DefineLine)
                    {
                        continue;
                    }
                    if (rpgLine is UnKnownCalculationLine)
                    {
                        continue;
                    }
                    CalculateBlock = MethodBlockStatement.CalculateOf();
                    CalculateBlock.Add(rpgLine);
                    Statements.RemoveAt(i);
                    Statements.Insert(i, CalculateBlock);
                    continue;

                }

                if (SubroutineBlocks.Count == 0)
                {

                    if (!(rpgLine is BegsrLine))
                    {
                        CalculateBlock.Add(rpgLine);
                        Statements.RemoveAt(i);
                        i--;
                        continue;
                    }

                }

                if (rpgLine is BegsrLine)
                {
                    GatherSubroutineBlock(i);
                    continue;
                }
            }

            if (CalculateBlock == null)//C仕様書がコメントのみの場合
            {
                CalculateBlock = MethodBlockStatement.CalculateOf();
                for (var i = 0; i < Statements.Count; i++)
                {
                    if(Statements[i] is CalculationLine)
                    {
                        var rpgLine = (CalculationLine)Statements[i];
                        CalculateBlock.Add(rpgLine);
                    }
                }
                Statements.RemoveAll(e => true);
                Statements.Add(CalculateBlock);
            }
        }

        void GatherKlistBlock(int i)
        {
            var rpgLine = Statements[i];
            var klist = new Klist((KlistLine)rpgLine);
            Statements.RemoveAt(i);
            while (true)
            {
                var nextLine = Statements[i];
                if (nextLine is KfldLine)
                {
                    klist.Add((KfldLine)nextLine);
                    Statements.RemoveAt(i);
                }
                else if (nextLine is ICommentStatement)
                {
                    //klist.Add((KfldLine)nextLine);
                    Statements.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            Statements.Insert(i, klist);
        }

        public List<IDataTypeDefinition> Parameters = new List<IDataTypeDefinition>();

        void GatherParameters(int i)
        {
            var rpgLine = Statements[i];
            IBlockStatement<ParmLine> plist = new Plist((PlistLine)rpgLine);
            Statements.RemoveAt(i);
            while (true)
            {
                var nextLine = Statements[i];
                if (nextLine is ParmLine parmLine)
                {
                    plist.Add(parmLine);
                    Parameters.Add(parmLine);
                    Statements.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            Statements.Insert(i, plist);
        }

        void GatherSubroutineBlock(int i)
        {
            var rpgLine = (BegsrLine)Statements[i];
            IBlockStatement<IStatement> subroutinBlock = new MethodBlockStatement(rpgLine.Factor1, rpgLine);
            Statements.RemoveAt(i);
            while (true)
            {
                var nextLine = Statements[i];
                if (nextLine is EndsrLine)
                {
                    ((MethodBlockStatement)subroutinBlock).CloseStatement = nextLine;
                    Statements.RemoveAt(i);
                    break;
                }
                else
                if (nextLine is KlistLine)
                {
                    GatherKlistBlock(i);
                    continue;
                }
                else
                {
                    subroutinBlock.Add(nextLine);
                    Statements.RemoveAt(i);
                }
            }

            var ExfmtLineNumbers = subroutinBlock.Statements.Where(line => line is ExfmtLine).Cast<ExfmtLine>().Select(line => line.StartLineIndex).ToList();

            ExfmtLineNumbers.ForEach(l => ((MethodBlockStatement)subroutinBlock).AddExfmtLineNumber(l));

            Statements.Insert(i, subroutinBlock);
            SubroutineBlocks.Add(subroutinBlock);

        }

    }
}

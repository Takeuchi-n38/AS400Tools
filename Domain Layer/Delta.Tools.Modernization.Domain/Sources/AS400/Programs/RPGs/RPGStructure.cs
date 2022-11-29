using Delta.AS400.DataTypes;
using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Controls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Programs.RPGs.Forms.Ls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs
{
    public abstract class RPGStructure : ProgramStructure
    {

        public bool IsCalling => RPGCallLines.Count() > 0;

        public List<RPGCallLine> RPGCallLines => CalculationBlock.RPGCallLines;

        internal IBlockStatement<IStatement> ControlBlock { get; } = new ControlBlock();

        internal FileDescriptionBlock FileDescriptionBlock { get; } = new FileDescriptionBlock();
        internal IBlockStatement<IStatement> LBlock { get; } = new LBlock();

        internal ExtensionBlock ExtensionBlock { get; } = new ExtensionBlock();
        internal DefinitionBlock DefinitionBlock { get; } = new DefinitionBlock();
        internal InputBlock InputBlock { get; } = new InputBlock();
        internal CalculationBlock CalculationBlock { get; } = new CalculationBlock();
        internal OutputBlock OutputBlock { get; } = new OutputBlock();
        internal ProgramDataBlock ProgramDataBlock { get; } = new ProgramDataBlock();

        public bool IsExistInzsr => CalculationBlock.IsExistInzsr;

        public DiskFileDescriptionLine? CycleFileLine => FileDescriptionBlock.CycleFileLine;


        public List<IDataTypeDefinition> TypeDefinitions
        {
            get
            {
                var parameters = new List<IDataTypeDefinition>();

                parameters.AddRange(CalculationBlock.Parameters);

                if (parameters.Count == 0)
                {
                    parameters.AddRange(DefinitionBlock.TypeDefinitions(ObjectID.Name));
                }

                return parameters;
            }
        }

        public List<IDataTypeDefinition> Variables => CalculationBlock.TypeDefinitions;

        public IEnumerable<string> DiskFileNames => DiskFileDescriptionLines.Select(l => l.FileObjectID.Name);

        public IEnumerable<ProgramDescribedInputFile> ProgramDescribedInputFiles => InputBlock.ProgramDescribedInputFiles;

        public void Add(IRPGLine rpgLine)
        {
            IBlockStatement<IStatement> objectBlock = null;
            if (rpgLine.FormType == FormType.Control)
            {
                objectBlock = ControlBlock;
            }
            if (rpgLine.FormType == FormType.FileDescription)
            {
                objectBlock = FileDescriptionBlock;
            }
            if (rpgLine.FormType == FormType.L)
            {
                objectBlock = LBlock;
            }
            if (rpgLine.FormType == FormType.Extension)
            {
                objectBlock = ExtensionBlock;
            }
            if (rpgLine.FormType == FormType.Definition)
            {
                objectBlock = DefinitionBlock;
            }
            if (rpgLine.FormType == FormType.Input)
            {
                objectBlock = InputBlock;
            }
            if (rpgLine.FormType == FormType.Calculation)
            {
                objectBlock = CalculationBlock;
            }
            if (rpgLine.FormType == FormType.Output)
            {
                objectBlock = OutputBlock;
            }
            if (rpgLine.FormType == FormType.ProgramData)
            {
                objectBlock = ProgramDataBlock;
            }
            if (objectBlock == null) throw new ArgumentException();
            objectBlock.Add(rpgLine); return;
        }

        public void ReformBlocks()
        {
            ExtensionBlock.Reform();
            DefinitionBlock.Reform();
            ReformInputBlock();
            CalculationBlock.Reform();
            OutputBlock.Reform(InternalFileNameAndLineLengths);
            ReformProgramDataBlock();
        }
        protected abstract void ReformInputBlock();

        protected abstract void ReformProgramDataBlock();

        protected RPGStructure(Source source) : base(source, new List<IStatement>())
        {

            Elements.Add(ControlBlock);
            Elements.Add(FileDescriptionBlock);
            Elements.Add(LBlock);
            Elements.Add(ExtensionBlock);
            Elements.Add(DefinitionBlock);
            Elements.Add(InputBlock);
            Elements.Add(CalculationBlock);
            Elements.Add(OutputBlock);
            Elements.Add(ProgramDataBlock);

        }

        public int CommentCount { get; private set; }
        public void SetCommentCount()
        {
            CommentCount = ControlBlock.Statements.Count(element => element is ICommentStatement)
            + FileDescriptionBlock.Statements.Count(element => element is ICommentStatement)
            + LBlock.Statements.Count(element => element is ICommentStatement)
            + ExtensionBlock.Statements.Count(element => element is ICommentStatement)
            + DefinitionBlock.Statements.Count(element => element is ICommentStatement)
            + InputBlock.Statements.Count(element => element is ICommentStatement)
            + CalculationBlock.Statements.Count(element => element is ICommentStatement)
            + OutputBlock.Statements.Count(element => element is ICommentStatement)
            + ProgramDataBlock.Statements.Count(element => element is ICommentStatement)
            ;

        }

        public IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames=> FileDescriptionBlock.FieldNameModernTypeNames;

        IEnumerable<IStatement> FileDescriptionContainer => FileDescriptionBlock.Statements;

        public IEnumerable<FileDescriptionLine> FileDescriptions => FileDescriptionContainer.Where(line => line is WorkstnFileDescriptionLine || line is DiskFileDescriptionLine || line is PrinterFileDescriptionLine).Cast<FileDescriptionLine>();

        public IEnumerable<DiskFileDescriptionLine> ExternalDiskFileDescriptionLines => FileDescriptionBlock.ExternalDiskFileDescriptionLines;
        public IEnumerable<ObjectID> InternalFileObjectID => FileDescriptionBlock.InternalFileObjectID;
        public IEnumerable<(string FileName, int LineLength)> InternalFileNameAndLineLengths => FileDescriptionBlock.InternalFileNameAndLineLengths;
        public IEnumerable<WorkstnFileDescriptionLine> WorkstnFileDescriptionLines => FileDescriptions.Where(line => line is WorkstnFileDescriptionLine).Cast<WorkstnFileDescriptionLine>();
        public IEnumerable<DiskFileDescriptionLine> DiskFileDescriptionLines => FileDescriptionBlock.DiskFileDescriptionLines;
        public IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines => FileDescriptions.Where(line => line is PrinterFileDescriptionLine).Cast<PrinterFileDescriptionLine>();

        public override ObjectID? WorkstationFileObjectID => WorkstnFileDescriptionLines.Select(line => line.FileObjectID).FirstOrDefault();

        public int CountOfF => FileDescriptions.Count();

        public bool RefferingWorkstnFile => CountOfWorkstn>0;
        public int CountOfWorkstn => WorkstnFileDescriptionLines.Count();

        public int CountOfDisk => DiskFileDescriptionLines.Count();

        public int CountOfPrinter => PrinterFileDescriptionLines.Count();

        public string Description => $"{OriginalSource.Description.ToUpper()},{CommentCount},{CountOfWorkstn},{CountOfDisk},{CountOfPrinter}";

    }
}

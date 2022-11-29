using Delta.AS400.DataTypes;
using Delta.AS400.Objects;
using Delta.AS400.RPGs.Forms.Definitions.Blocks.Pis;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.PrinterFiles;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Generator.Statements.Singles;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs;
using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects;
using Delta.Tools.AS400.Programs.RPGs.Forms.Controls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Programs.RPGs.Forms.Ls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.AS400.Structures;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations.Dos;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Blocks.Ifs;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs
{
    public class RPGStructureToCSharper
    {
        ObjectIDFactory ObjectIDFactory;
        DiskFileStructureBuilder DiskFileStructureFactory;
        DiskFileDescriptionLineToCSharper DiskFileDescriptionLineToCSharper;
        GeneratorFromPrtf GeneratorFromPrtf;
        GeneratorFromDspf GeneratorFromDspf;
        StructureBuilder ForPRT;
        ProgramStructureBuilder ProgramStructureFactory;
        public RPGStructureToCSharper(ProgramStructureBuilder ProgramStructureFactory,DiskFileStructureBuilder diskFileStructureFactory, GeneratorFromPrtf generatorFromPrtf, GeneratorFromDspf generatorFromDspf, StructureBuilder forPRT)
        {
            this.ProgramStructureFactory= ProgramStructureFactory;
            this.DiskFileStructureFactory = diskFileStructureFactory;
            this.ObjectIDFactory=diskFileStructureFactory.ObjectIDFactory;
            this.DiskFileDescriptionLineToCSharper=new DiskFileDescriptionLineToCSharper(DiskFileStructureFactory);
            this.GeneratorFromPrtf = generatorFromPrtf;
            this.GeneratorFromDspf = generatorFromDspf;
            this.ForPRT = forPRT;
        }

        ObjectID TargetRpgObjectID { get;set;}

        public IEnumerable<string> CreateContentLines(RPGStructure rpg, List<Variable> outputVariables, List<Variable> outputtedVariables)
        {

            TargetRpgObjectID = rpg.ObjectID;

            var elements = rpg.Elements;

            var contentLinesOfInputBlock = new List<string>();
            elements.Where(statement => statement is InputBlock)
                .SelectMany(statement => ((IBlockStatement<IStatement>)statement).Statements)
                .ToList()
                .ForEach(inputLine => {
                    if (inputLine is ICommentStatement)
                    {
                        contentLinesOfInputBlock.Add($"{((ILine)inputLine).ToOriginalComment()}");
                    }
                    else
                    if (inputLine is ProgramDescribedInputFile programDescribedInputFile)
                    {
                        ProgramDescribedFileToCSharper.GenerateInputCSharpSourceLines(programDescribedInputFile, contentLinesOfInputBlock, outputtedVariables);
                    }
                    else if (inputLine is DsBlock3 dsBlock)
                    {
                        DsBlockToCSharper.ToCSharp(dsBlock, contentLinesOfInputBlock, outputtedVariables);
                    }
                    else
                    if (inputLine is IRPGLine3)
                    {
                            contentLinesOfInputBlock.Add($"//TODO RPG3 {((ILine)inputLine).ToOriginalComment()}");
                    }
                    else
                    {
                        throw new NotImplementedException();
                    } 
                    }
                );


            //var contentLinesOfDsBlock3 = new List<string>();
            //elements.Where(statement => statement is InputBlock)
            //    .SelectMany(statement => ((IBlockStatement<IStatement>)statement).Statements)
            //    .Where(statement => statement is DsBlock3).Cast<DsBlock3>().ToList()
            //    .ForEach(dsBlock => DsBlockToCSharper.ToCSharp(dsBlock, contentLinesOfDsBlock3, outputtedVariables));
         
            var contentLinesOfDsBlock4 = new List<string>();
            elements.Where(statement => statement is DefinitionBlock)
                .SelectMany(statement => ((IBlockStatement<IStatement>)statement).Statements)
                .Where(statement => statement is DsBlock4).Cast<DsBlock4>().ToList()
                .ForEach(dsBlock => DsBlockToCSharper.ToCSharp(dsBlock, contentLinesOfDsBlock4, outputtedVariables));

            //var contentLinesOfProgramDescribedInputFile = new List<string>();
            //elements.Where(statement => statement is InputBlock)
            //    .SelectMany(statement => ((IBlockStatement<IStatement>)statement).Statements)
            //    .Where(statement => statement is ProgramDescribedInputFile).Cast<ProgramDescribedInputFile>().ToList()
            //    .ForEach(programDescribedInputFile =>
            //        ProgramDescribedFileToCSharper.GenerateInputCSharpSourceLines(programDescribedInputFile, contentLinesOfProgramDescribedInputFile, outputtedVariables)
            //    );

            var variables = rpg.Variables.Select(v => VariableFactory.Of(v)).ToList();
            variables.Where(v => !outputtedVariables.Contains(v)).ToList().ForEach(v =>
            {
                outputVariables.Add(v);
                outputtedVariables.Add(v);
            });


            var contentLines = new List<string>();
            elements.ForEach(element =>
            {
                var curText = new StringBuilder();

                if (element is ControlBlock controlBlock)
                {
                    controlBlock.Statements.ToList().ForEach(line => {
                        if (line is RPGCommentLine commentStatement)
                        {
                            contentLines.Add($"{commentStatement.ToOriginalComment()}");
                        }
                        else
                        if (line is ControlLine controlLine)
                        {
                            contentLines.Add($"//todo hLine {controlLine.ToOriginalComment()}");
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    });
                }
                else if (element is FileDescriptionBlock)
                {
                    curText.AppendLine("#region FileDescriptionBlock");
                    ((IBlockStatement<IStatement>)element).Statements.ForEach(ElementOfFileDescriptionBlock => {
                        if (ElementOfFileDescriptionBlock is ICommentStatement)
                        {
                            curText.AppendLine($"{((ILine)ElementOfFileDescriptionBlock).ToOriginalComment()}");
                        }
                        else if (ElementOfFileDescriptionBlock is FileDescriptonRenameLine)
                        {
                            curText.AppendLine($"{((ILine)ElementOfFileDescriptionBlock).ToOriginalComment()}");
                        }
                        else
                        if (ElementOfFileDescriptionBlock is DiskFileDescriptionLine DiskFileDescriptionLine)
                        {
                            DiskFileDescriptionLineToCSharper.GenerateCSharpSourceLines(DiskFileDescriptionLine,curText,outputtedVariables);
                        }
                        else
                        if (ElementOfFileDescriptionBlock is PrinterFileDescriptionLine PrinterFileDescriptionLine)
                        {

                            if (!PrinterFileDescriptionLine.IsExternalFileFormat)
                            {
                                var OperandFileName = PrinterFileDescriptionLine.FileName.ToCSharpOperand();
                                curText.AppendLine($"IPrinter {OperandFileName.ToCamelCase()};{PrinterFileDescriptionLine.ToOriginalComment()}");
                                //curText.AppendLine($"public IReportPagesCreator ReportPagesCreatorOf{OperandFileName}()");
                                //curText.AppendLine("{");
                                //curText.AppendLine($"{Indent.Single}return {OperandFileName.ToCamelCase()};");
                                //curText.AppendLine("}");
                                return;//next
                            }

                            curText.AppendLine($"IPrinter qprint;{PrinterFileDescriptionLine.ToOriginalComment()}");
                            //curText.AppendLine($"public IReportPagesCreator ReportPagesCreatorOfQprint()");
                            //curText.AppendLine("{");
                            //curText.AppendLine($"{Indent.Single}return qprint;");
                            //curText.AppendLine("}");

                            PRTStructure printf = (PRTStructure)ForPRT.Create(PrinterFileDescriptionLine.FileObjectID);

                            CommentFactory.OriginalLineCommentLines(printf.OriginalSource.OriginalLines).ToList().ForEach(line => curText.AppendLine(line));

                            printf.PrintLineLists.ForEach(
                                printLineList =>
                                {
                                    var ContainerName = ((IDDSLine)printLineList.PrintLines.First()).Name.ToPublicModernName();

                                    var MaxLineNumber = printLineList.PrintLines.Count();

                                    curText.AppendLine($"void Write({ContainerName}OutputRowsContainer row)");
                                    curText.AppendLine($"{{");
                                    curText.AppendLine($"{Indent.Single}qprint.Excpt(row);");
                                    curText.AppendLine($"}}");

                                    var CSharpSourceLines = new List<string>();

                                    GeneratorFromPrtf.Output(CSharpSourceLines, ContainerName, MaxLineNumber);

                                    for (var lineNumber = 1; lineNumber <= MaxLineNumber; lineNumber++)
                                    {
                                        var printLine = printLineList.PrintLines[lineNumber - 1];
                                        GeneratorFromPrtf.GenerateOutputRowsByExternallyDescribed(CSharpSourceLines, ContainerName, lineNumber, false, printLine, outputtedVariables);

                                        var TypeDefinitions = DiskFileStructureFactory.TypeDefinitionList(printLine.IsVariables.ToList(), null);

                                        VariableFactory.Of(TypeDefinitions).ToList().ForEach(variable =>
                                        {
                                            if (!outputtedVariables.Contains(variable))
                                            {
                                                CSharpSourceLines.Add($"{PropertyItem.Of(variable).ToAutoImplementedPropertiesString()}");
                                                outputtedVariables.Add(variable);
                                            }
                                        });

                                    }

                                    CSharpSourceLines.ForEach(line => curText.AppendLine(line));

                                }
                            );

                        }
                        else
                        if (ElementOfFileDescriptionBlock is WorkstnFileDescriptionLine)
                        {
                            var WorkstnFileDescriptionLine = (WorkstnFileDescriptionLine)ElementOfFileDescriptionBlock;

                            curText.AppendLine($"#region WorkstnFileDescriptionLine");

                            curText.AppendLine(WorkstnFileDescriptionLine.ToOriginalComment());

                            var workstationLines = GeneratorFromDspf.CreateContentsForService(WorkstnFileDescriptionLine.FileObjectID, outputtedVariables);
                            workstationLines.ToList().ForEach(line => curText.AppendLine(line));

                            curText.AppendLine($"#endregion WorkstnFileDescriptionLine");

                        }
                        else
                        if (ElementOfFileDescriptionBlock is UnKnownFileDescriptionLine unKnownFileDescriptionLine)
                        {
                            curText.AppendLine($"//todo UnKnownFileDescriptionLine {((ILine)unKnownFileDescriptionLine).ToOriginalComment()}");
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    });
                    curText.AppendLine("#endregion FileDescriptionBlock");

                }
                else if (element is LBlock lBlock)
                {
                    lBlock.Statements.ForEach(lLine =>
                    {
                        if (lLine is ICommentStatement)
                        {
                            contentLines.Add($"{((ILine)lLine).ToOriginalComment()}");
                        }
                        else
                        if (lLine is LLine)
                        {
                            contentLines.Add($"//todo lLine {((ILine)lLine).ToOriginalComment()}");
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    });

                }
                else if (element is ExtensionBlock)
                {
                    //curText.AppendLine("#region ExtensionBlock");

                    ((IBlockStatement<IStatement>)element).Statements.ForEach(ExtensionLine =>
                    {
                        if (ExtensionLine is ICommentStatement)
                        {
                            curText.AppendLine($"{((ILine)ExtensionLine).ToOriginalComment()}");
                        }
                        else if (ExtensionLine is DimLine3 dimLine)
                        {

                            curText.AppendLine($"{dimLine.ToOriginalComment()}");

                            var vari= VariableFactory.Of((IDataTypeDefinition)dimLine);
                            var ModernTypeName = vari.TypeSpelling;

                            var VarName = ((IDataTypeDefinition)dimLine).Name.ToCSharpOperand();
                            var ArrayLength = int.Parse(((IRPGExtensionLine)dimLine).ArrayLength);
                            var ArrayVariable = Variable.Of(TypeOfVariable.OfArray(ModernTypeName,vari.TypeLength,vari.TypeNumberOfDecimalPlaces, ArrayLength), VarName);

                            curText.AppendLine($"{ModernTypeName}[] {VarName} = new {ModernTypeName}[{ArrayLength}];");
                            outputtedVariables.Add(ArrayVariable);
                        }
                        else if (ExtensionLine is DimCtdataLine3 dimCtdataLine)
                        {
                            var line =(IDataTypeDefinition) dimCtdataLine;
                            var variable=VariableFactory.Of(line);
                            var ModernTypeName = variable.TypeSpelling;// dimCtdataLine.ModernTypeName;
                            var programDataValues = dimCtdataLine.programDataValues;
                            var ArrayLength= programDataValues.Count + 1;
                            var VarName= line.Name.ToCSharpOperand();
                            var CSharpSourceLines = new List<string>();

                            var OriginalComment = ((ILine)dimCtdataLine).ToOriginalComment();
                            CSharpSourceLines.Add($"{OriginalComment}");
                            CSharpSourceLines.Add($"private static readonly {ModernTypeName}[] {VarName} = new {ModernTypeName}[{ArrayLength}]");
                            CSharpSourceLines.Add("{");
                            CSharpSourceLines.Add($"{Indent.Single}{variable.TypeInitialValueSpelling},");
                            for (var i = 0; i < programDataValues.Count; i++)
                            {
                                CSharpSourceLines.Add($"{Indent.Single}{programDataValues[i]},");
                            }

                            CSharpSourceLines.Add("};");

                            CSharpSourceLines.ForEach(line => curText.AppendLine(line));
                            var ArrayVariable = Variable.Of(TypeOfVariable.OfArray(ModernTypeName, variable.TypeLength, variable.TypeNumberOfDecimalPlaces, ArrayLength), VarName);
                            outputtedVariables.Add(ArrayVariable);
                        }
                        else
                        if (ExtensionLine is IRPGExtensionLine)
                        {
                            curText.AppendLine($"{((ILine)ExtensionLine).ToOriginalComment()}");
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    });

                    //curText.AppendLine("#endregion ExtensionBlock");

                }
                else if (element is DefinitionBlock)
                {
                    if(((IBlockStatement<IStatement>)element).Statements.Count > 0)
                    {
                        curText.AppendLine("#region DefinitionBlock");

                        contentLinesOfDsBlock4.ForEach(line => curText.AppendLine(line));

                        ((IBlockStatement<IStatement>)element).Statements.ForEach(ElementOfFileDefinitionBlock =>
                        {
                            if (ElementOfFileDefinitionBlock is ICommentStatement)
                            {
                                curText.AppendLine($"{((ILine)ElementOfFileDefinitionBlock).ToOriginalComment()}");
                            }
                            else if (ElementOfFileDefinitionBlock is DimLine4 dimLine)
                            {

                                curText.AppendLine($"{dimLine.ToOriginalComment()}");

                                var vari = VariableFactory.Of((IDataTypeDefinition)dimLine);
                                var ModernTypeName = vari.TypeSpelling;

                                var VarName = ((IDataTypeDefinition)dimLine).Name.ToCSharpOperand();

                                var ArrayVariable = Variable.Of(TypeOfVariable.OfArray(ModernTypeName, vari.TypeLength,vari.TypeNumberOfDecimalPlaces, dimLine.Size), VarName);

                                curText.AppendLine($"{ModernTypeName}[] {VarName} = new {ModernTypeName}[{dimLine.Size}];");
                                outputtedVariables.Add(ArrayVariable);
                            }
                            else if (ElementOfFileDefinitionBlock is DimCtdataLine4 dimCtdataLine)
                            {
                                var line = dimCtdataLine.line;
                                var ModernTypeName = VariableFactory.Of((IDataTypeDefinition)line).TypeSpelling;// dimCtdataLine.ModernTypeName;
                                var programDataValues = dimCtdataLine.programDataValues;
                                var CSharpSourceLines = new List<string>();

                                var OriginalComment = ((ILine)ElementOfFileDefinitionBlock).ToOriginalComment();
                                CSharpSourceLines.Add($"{OriginalComment}");
                                CSharpSourceLines.Add($"private static readonly {ModernTypeName}[] {line.Name.ToCSharpOperand()} = new {ModernTypeName}[{programDataValues.Count}]");
                                CSharpSourceLines.Add("{");
                                for (var i = 0; i < programDataValues.Count; i++)
                                {
                                    CSharpSourceLines.Add($"{Indent.Single}{programDataValues[i]},");
                                }

                                CSharpSourceLines.Add("};");

                                CSharpSourceLines.ForEach(line => curText.AppendLine(line));
                            }
                            else if (ElementOfFileDefinitionBlock is DsBlock4 dsBlock)
                            {
                                //var CSharpSourceLines = new List<string>();

                                //DsBlockToCSharper.ToCSharp(dsBlock, CSharpSourceLines, outputtedVariables);

                                //contentLinesOfDsBlock4.ForEach(line => curText.AppendLine(line));
                            }
                            else if (ElementOfFileDefinitionBlock is PiBlock piBlock)
                            {

                                var CSharpSourceLines = new List<string>();

                                var openerLine = piBlock.openerLine;
                                var Statements = piBlock.Statements;

                                CSharpSourceLines.Add($"{openerLine.ToOriginalComment()}");

                                Statements.ForEach(statement => {
                                    if (statement is DifinitionBlockItemLine)
                                    {
                                        var line = (DifinitionBlockItemLine)statement;
                                        CSharpSourceLines.Add($"{line.ToOriginalComment()}");
                                    }
                                    else
                                    {
                                        throw new NotImplementedException();
                                    }
                                });

                                CSharpSourceLines.ForEach(line => curText.AppendLine(line));

                            }
                            else if (ElementOfFileDefinitionBlock is PrBlock)
                            {
                                var prBlock = (PrBlock)ElementOfFileDefinitionBlock;

                                curText.AppendLine($"{prBlock.OpenerLine.ToOriginalComment()}");

                                prBlock.Statements.ForEach(prElement =>
                                {
                                    if (prElement is DifinitionBlockItemLine)
                                    {
                                        var variable = VariableFactory.Of((IRPGDefinitionLine4)prElement);
                                        var originalComment = ((DifinitionBlockItemLine)prElement).ToOriginalComment();

                                        if (outputtedVariables.Contains(variable))
                                        {
                                            curText.AppendLine($"{originalComment}");
                                        }
                                        else
                                        {
                                            curText.AppendLine($"{PropertyItem.Of(variable).ToAutoImplementedPropertiesString()}{originalComment}");
                                            outputtedVariables.Add(variable);
                                        }
                                    }
                                    else
                                    {
                                        throw new NotImplementedException();
                                    }

                                });
                            }
                            else if (ElementOfFileDefinitionBlock is VarLine)
                            {
                                var variable = VariableFactory.Of((IRPGDefinitionLine4)ElementOfFileDefinitionBlock);

                                var originalComment = ((VarLine)ElementOfFileDefinitionBlock).ToOriginalComment();
                                if (outputtedVariables.Contains(variable))
                                {
                                    curText.AppendLine($"{originalComment}");
                                }
                                else
                                {
                                    curText.AppendLine($"{PropertyItem.Of(variable).ToAutoImplementedPropertiesString()}{originalComment}");
                                    outputtedVariables.Add(variable);
                                }
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        });

                        curText.AppendLine("#endregion DefinitionBlock");

                    }

                }
                else if (element is InputBlock inputBlock)
                {
                    //curText.AppendLine("#region InputBlock");

                    //inputBlock.Statements.ForEach(inputLine => {
                    //    if (inputLine is ICommentStatement)
                    //    {
                    //        contentLines.Add($"{((ILine)inputLine).ToOriginalComment()}");
                    //    }
                    //    else
                    //    if (inputLine is ProgramDescribedInputFile programDescribedInputFile)
                    //    {
                    //        //contentLinesOfProgramDescribedInputFile.ForEach(line => curText.AppendLine(line));
                    //    }
                    //    else if (inputLine is DsBlock3 dsBlock)
                    //    {
                    //        //var CSharpSourceLines = new List<string>();

                    //        //DsBlockToCSharper.ToCSharp(dsBlock, CSharpSourceLines, outputtedVariables);

                    //        //contentLinesOfDsBlock3.ForEach(line => curText.AppendLine(line));
                    //    }
                    //    else
                    //    if (inputLine is IRPGLine3)
                    //    {
                    //        contentLines.Add($"//TODO RPG3 {((ILine)inputLine).ToOriginalComment()}");
                    //    }
                    //    else
                    //    {
                    //        throw new NotImplementedException();
                    //    }
                    //});

                    //contentLinesOfProgramDescribedInputFile.ForEach(line => curText.AppendLine(line));
                    //contentLinesOfDsBlock3.ForEach(line => curText.AppendLine(line));

                    contentLinesOfInputBlock.ForEach(line => curText.AppendLine(line));
                    //curText.AppendLine("#endregion InputBlock");

                }
                else if (element is CalculationBlock calculationBlock)
                {
                    var FieldNameModernTypeNames = rpg.FieldNameModernTypeNames;
                    calculationBlock.DefineLines.Where(defineLine => defineLine.ResultField != "DTAARA" && defineLine.ResultField != "ERRDTA").Select(defineLine =>
                    {
                        if (defineLine.FieldLength != string.Empty)
                        {
                            var itype = DataTypeDefinition.Of(defineLine.ResultField.ToCSharpOperand(),
                                defineLine.FieldLength, string.Empty, defineLine.DecimalPositions);
                            return Variable.Of(TypeOfVariableFactory.Of(itype), defineLine.ResultField.ToCSharpOperand());
                        }

                        var finded = FieldNameModernTypeNames.Where(kv => kv.Name.TrimEnd() == defineLine.Factor2.TrimEnd()).FirstOrDefault();
                        return Variable.Of(TypeOfVariableFactory.Of(DiskFileStructureFactory.CreateTypeDefinition(finded.RecordFormatFieldLine, null)), defineLine.ResultField.ToCSharpOperand());
                    }
                    )
                    //calculationBlock.DefineLines.Select(l => Variable.Of(TypeOfVariableFactory.Of(GeneratorService.Instance.CreateTypeDefinition(l.RecordFormatFieldLine, null)), l.ResultField.ToCSharpOperand()))
                    .ToList().ForEach(
                    ScalarVariable =>
                    {
                        curText.AppendLine($"{ScalarVariable.TypeSpelling} {ScalarVariable.Name} {{  get; set; }}");
                        outputtedVariables.Add(ScalarVariable);
                    }
                    );


                    //Variable.Of(TypeOfVariableFactory.Of(GeneratorService.Instance.CreateTypeDefinition(l.RecordFormatFieldLine, null)), l.ResultField.ToCSharpOperand())).ToList().ForEach(
                    //    ScalarVariable => {
                    //        curText.AppendLine($"{ScalarVariable.TypeSpelling} {ScalarVariable.Name} {{  get; set; }}");
                    //        outputtedVariables.Add(ScalarVariable);
                    //    }
                    //    );

                    calculationBlock.TypeDefinitions.ForEach(v=> outputtedVariables.Add(VariableFactory.Of(v)));

                    calculationBlock.ExfmtLineNumbers.ForEach(line =>
                    {
                        curText.AppendLine($"bool GoToAfterExfmt{line:D4} {{ get;set;}} = false;");
                    });

                    var CSharpSourceLines = new List<string>();
                    GenerateCSharpSourceLines(CSharpSourceLines, Indent.Zero,rpg, outputtedVariables, ((IBlockStatement<IStatement>)element).Statements, FieldNameModernTypeNames);
                    CSharpSourceLines.ForEach(line => curText.AppendLine(line));

                }
                else if (element is OutputBlock)
                {
                    OutputBlockGenerateCSharpSourceLines((OutputBlock)element, contentLines, outputtedVariables);
                }
                else if (element is ProgramDataBlock programDataBlock)
                {
                    programDataBlock.Statements.ForEach(programDataLine =>
                    {
                        if (programDataLine is ICommentStatement)
                        {
                            contentLines.Add($"{((ILine)programDataLine).ToOriginalComment()}");
                        }
                        else
                        if (programDataLine is ProgramDataLine)
                        {
                            contentLines.Add($"{((ILine)programDataLine).ToOriginalComment()}");
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    });
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (curText.Length != 0)
                {
                    curText.ToString().Split("\r\n").ToList().ForEach(line => {
                        if (line.Length > 0) contentLines.Add(line);
                    });
                }
            }
            );

            return contentLines;
        }

        void GenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg,List<Variable> variables, List<IStatement> statements, 
            IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            statements.ForEach(statement => GenerateCSharpSourceLines(CSharpSourceLines, indent,rpg, variables, statement, FieldNameModernTypeNames));
        }

        void GenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg,  List<Variable> variables, IStatement statement, 
            IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var originalComment = (statement is ILine) ? ((ILine)statement).ToOriginalComment() : string.Empty;
            if (statement is ICommentStatement)
            {
                CSharpSourceLines.Add($"{indent}{originalComment}");return;
            }
            else if (statement is ReturnLine)
            {
                CSharpSourceLines.Add($"{indent}// TODO:RETURN {originalComment}"); return;
            }
            else if (statement is LeaveLine leaveLine)
            {
                if (leaveLine.HasIndicator)
                {
                    CSharpSourceLines.Add($"{indent}if ({(leaveLine.NameOfIndicator1=="LR"?string.Empty:"In")}{leaveLine.NameOfIndicator1} {(leaveLine.IsNOfIndicator1 ? " !=": " == ")} \"1\") break;{originalComment}"); return;//TODO:goto in DoHival
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}break; {originalComment}");return;//TODO:goto in DoHival
                }
            }
            else if (statement is IterLine)
            {
                CSharpSourceLines.Add($"{indent}continue; {originalComment}");return;//TODO:goto in DoHival
            }
            else if (statement is DefineLine)
            {
                CSharpSourceLines.Add($"{indent}{originalComment}"); return;
            }
            else if (statement is UnKnownCalculationLine)
            {
                CSharpSourceLines.Add($"{indent}//todo cLine {originalComment}");return;
            }
            else if (statement is KlistLine)
            {
                CSharpSourceLines.Add($"{indent}//todo rpg3 {originalComment}"); return;
            }
            else if (statement is EndcsLine)
            {
                CSharpSourceLines.Add(indent + "{}" + originalComment);//CAS対策。openerの印を見つけにくいので
                return;
            }
            else if (statement is ElseLine)
            {
                var decrementedIndent = indent.Decrement();
                CSharpSourceLines.Add(decrementedIndent + "}");
                CSharpSourceLines.Add($"{decrementedIndent}else {originalComment}");
                CSharpSourceLines.Add(decrementedIndent + "{");
                return;
            }
            else if (statement is PlistLine)
            {
                CSharpSourceLines.Add($"{indent}//not dif {originalComment}"); return;
            }

            if (statement is MethodBlockStatement methodBlockStatement)
            {
                MethodBlockStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, methodBlockStatement, FieldNameModernTypeNames); 
                return;
            }
            else
            if (statement is DoBlockStatement doStatement)
            {
                DoStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, doStatement, FieldNameModernTypeNames);
                return;
            }
            else
            if (statement is DouBlockStatement douStatement)
            {
                DouStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, douStatement, FieldNameModernTypeNames); 
                return;
            }
            else
            if (statement is DoueqBlockStatement doueqStatement)
            {
                DoueqStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, doueqStatement, FieldNameModernTypeNames);
                return;
            }
            else
            if (statement is DoweqBlockStatement doweqStatement)
            {
                DoweqStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, doweqStatement, FieldNameModernTypeNames);
                return;
            }
            else
            if (statement is DowgtBlockStatement dowgtStatement)
            {
                DowgtStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, dowgtStatement, FieldNameModernTypeNames);
                return;
            }
            else
            if (statement is DoForBlockStatement doBlockStatement)
            {
                DoBlockStatementGenerateCSharpSourceLines(CSharpSourceLines, indent,rpg, variables, doBlockStatement, FieldNameModernTypeNames); 
                return;
            }
            else
            if (statement is IfBlockStatement3 ifBlockStatement3)
            {
                IfBlockStatement3GenerateCSharpSourceLines(CSharpSourceLines, indent,rpg, variables, ifBlockStatement3, FieldNameModernTypeNames);
                return;
            }
            else
            if (statement is IfBlockStatement ifBlockStatement)
            {
                IfBlockStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, ifBlockStatement, FieldNameModernTypeNames); 
                return;
            }
            else
            if (statement is SelectBlock selectBlock)
            {
                SelectBlockGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, selectBlock, FieldNameModernTypeNames); 
                return;
            }
            else
            if (statement is RPGCallLine rPGCallLine)
            {
                RPGCallLineGenerateCSharpSourceLines(CSharpSourceLines, indent, rpg, variables, rPGCallLine); 
                return;
            }
            else
            if (statement is Plist plist)
            {
                PlistGenerateCSharpSourceLines(CSharpSourceLines, indent, variables, plist); return;
            }
            else
            if (statement is Klist klist)
            {
                KlistGenerateCSharpSourceLines(CSharpSourceLines, indent, variables, klist, FieldNameModernTypeNames); return;
            }
            else
            if (statement is DivBlock divBlock)
            {
                DivBlockGenerateCSharpSourceLines(CSharpSourceLines, indent, variables, divBlock); return;
            }
            else
            if (statement is EvalLine evalLine)
            {
                EvalLineGenerateCSharpSourceLines(CSharpSourceLines, indent, variables, evalLine);
                return;
            }

            if (statement is CalculationLine calculationLine)
            {
                //if (In97 == "1") 
                var indCond = string.Empty;
                if (calculationLine.HasIndicator)
                {
                    indCond = $"In{calculationLine.NameOfIndicator1}==\"{(calculationLine.IsNOfIndicator1 ? "0" : "1")}\"";
                    if (calculationLine.IsRPG3Line)
                    {
                        if (calculationLine.HasIndicator2)
                        {
                            indCond = $"{indCond} && In{calculationLine.NameOfIndicator2}==\"{(calculationLine.IsNOfIndicator2 ? "0" : "1")}\"";
                            if (calculationLine.HasIndicator3)
                            {
                                indCond = $"{indCond} && In{calculationLine.NameOfIndicator3}==\"{(calculationLine.IsNOfIndicator3 ? "0" : "1")}\"";
                            }
                        }
                    }

                    indCond = $"if({indCond}) ";
                }

                if (statement is SetonLine setonLine)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    setonLine.HiLoEq.Where(indicator => indicator != string.Empty).ToList().ForEach(
                        indicator =>
                        {
                            var control=string.Empty;
                            if (setonLine.Indicator1.Trim() != string.Empty)
                            {
                                control=$"if(In{setonLine.NameOfIndicator1}==\"{(setonLine.IsNOfIndicator1?"0":"1")}\") ";
                            }
                            //if(setonLine.IsOR) control=") ";
                           
                            if (indicator == "LR")
                            {
                                if (rpg.RefferingWorkstnFile)
                                {
                                    CSharpSourceLines.Add($"{indent}LR.TurnOn();");
                                }
                                else
                                {
                                    CSharpSourceLines.Add($"{indent}{control}LR = \"1\";");
                                }
                            }
                            else
                            {
                                CSharpSourceLines.Add($"{indent}{control}In{indicator} = \"1\";");
                            }
                        }
                    );
                    return;
                }
                else if (statement is SetofLine)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    calculationLine.HiLoEq.Where(indicator => indicator != string.Empty).ToList().ForEach(indicator => CSharpSourceLines.Add($"{indent}{indCond}In{indicator} = \"0\";")); 
                    return;
                }

                string Factor1 = calculationLine.Factor1;

                if (statement is TagLine)
                {
                    CSharpSourceLines.Add($"{indent}{Factor1.ToPublicModernName().ToLower()}: ;{originalComment}"); 
                    return;
                }

                string Factor1Operand = string.Empty;
                Variable? Factor1OperandVariable = null;
                if (Factor1.Contains(','))
                {
                    var splitted = Factor1.Split(',');
                    var arrayName = splitted[0].ToCSharpOperand();
                    var arrayIndex = splitted[1].ToCSharpOperand();
                    Factor1Operand = $"{arrayName}[{arrayIndex}]";
                    var arrayVariable = variables.Where(v => v.Name == arrayName).FirstOrDefault() ?? Variable.OfUnknownNameBy(arrayName);
                    if (arrayVariable.OfTypeIsArray)
                    {
                        Factor1OperandVariable = Variable.Of(arrayVariable.TypeOfVariable.ArrayItemType, Factor1Operand);
                    }
                    else
                    {
                        Factor1OperandVariable = Variable.Of(arrayVariable.TypeOfVariable, Factor1Operand);
                    }
                }
                else
                {
                    Factor1Operand = Factor1.ToCSharpOperand();
                    Factor1OperandVariable = variables.Where(v => v.Name == Factor1Operand).FirstOrDefault() ?? Variable.OfUnknownNameBy(Factor1Operand);
                }

                if (statement is EndsrLine)
                {
                    string label = (Factor1Operand != string.Empty)?$"{Factor1Operand.ToLower()}:;": string.Empty;
                    CSharpSourceLines.Add($"{indent}{label}}} {originalComment}");
                    return;
                }

                string Factor2 = calculationLine.Factor2;

                //ExfmtLine
                if (statement is ExfmtLine)
                {
                    string RecordFormatName = Factor2.ToPublicModernName();
                    string LineNumber = calculationLine.StartLineIndex.ToString("D4");
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    CSharpSourceLines.Add($"{indent}{indCond}Exfmt({RecordFormatName},null);//TODO:set null to AfterExfmtMethod in not in CalculateMethod");
                    CSharpSourceLines.Add($"{indent}//Exfmt({RecordFormatName});");
                    CSharpSourceLines.Add($"{indent}//GoToAfterExfmt{LineNumber} = true;");
                    CSharpSourceLines.Add($"{indent}//if(GoToAfterExfmt{LineNumber}) return;");
                    CSharpSourceLines.Add($"{indent}//AfterExfmt{LineNumber}:;");
                    return;
                }

                string Factor2Operand = string.Empty;
                Variable? Factor2OperandVariable=null;
                if (Factor2!="','" && Factor2.Contains(','))
                {
                    var splitted = Factor2.Split(',');
                    var arrayName = splitted[0].ToCSharpOperand();
                    var arrayIndex = splitted[1].ToCSharpOperand();
                    Factor2Operand = $"{arrayName}[{arrayIndex}]";
                    var arrayVariable = variables.Where(v => v.Name == arrayName).FirstOrDefault() ?? Variable.OfUnknownNameBy(arrayName);
                    if (arrayVariable.OfTypeIsArray)
                    {
                        Factor2OperandVariable = Variable.Of(arrayVariable.TypeOfVariable.ArrayItemType, Factor2Operand);
                    }
                    else
                    {
                        Factor2OperandVariable = Variable.Of(arrayVariable.TypeOfVariable, Factor2Operand);
                    }
                }
                else
                {
                    Factor2Operand = Factor2.ToCSharpOperand();
                    Factor2OperandVariable = variables.Where(v => v.Name == Factor2Operand).FirstOrDefault() ?? Variable.OfUnknownNameBy(Factor2Operand);
                }

                if (statement is DeleteLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}Delete({Factor2Operand}); {originalComment}"); return;
                }
                else
                if (statement is ExcptLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}Excpt({Factor2Operand}); {originalComment}"); return;
                }
                else
                if (statement is UpdateLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}Update({Factor2Operand}); {originalComment}"); return;
                }
                else
                if (statement is WriteLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}Write({Factor2Operand}); {originalComment}"); return;
                }
                else
                if (statement is ExsrLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}{Factor2Operand}(); {originalComment}"); return;
                }
                else
                if (statement is GotoLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}goto {Factor2Operand.ToLower()}; {originalComment}"); return;
                }
                else
                if (statement is ReadpLine)
                {
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readp", RecordName, IndicatorNumber, originalComment);
                    return;
                }
                else
                if (statement is ReadpnLine)
                {
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readpn", RecordName, IndicatorNumber, originalComment);
                    return;
                }
                else
                if (statement is ReadnLine)
                {
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readn", RecordName, IndicatorNumber, originalComment);
                    return;
                }
                else
                if (statement is ReadLine)
                {
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Read", RecordName, IndicatorNumber, originalComment);
                    return;
                }

                string ResultField = calculationLine.ResultField;
                string ResultFieldOperand = string.Empty;
                Variable? ResultFieldOperandVariable =null;
                if (ResultField.Contains(','))
                {
                    var splitted = ResultField.Split(',');
                    if (splitted[0] == "*IN")
                    {
                        var arrayIndex = splitted[1].ToCSharpOperand();
                        ResultFieldOperand = $"In{arrayIndex}";
                        ResultFieldOperandVariable=Variable.Of(TypeOfVariable.OfString(1), ResultFieldOperand);
                    }
                    else
                    {
                        var arrayName = splitted[0].ToCSharpOperand();
                        var arrayIndex = splitted[1].ToCSharpOperand();
                        ResultFieldOperand = $"{arrayName}[{arrayIndex}]";
                        var arrayVariable = (variables.Where(v => v.Name == arrayName).FirstOrDefault() ?? Variable.OfUnknownNameBy(arrayName));
                        if (arrayVariable.OfTypeIsArray)
                        {
                            ResultFieldOperandVariable = Variable.Of(arrayVariable.TypeOfVariable.ArrayItemType, ResultFieldOperand);
                        }
                        else
                        {
                            ResultFieldOperandVariable = Variable.Of(arrayVariable.TypeOfVariable, Factor2Operand);
                        }
                    }

                }
                else
                {
                    ResultFieldOperand = ResultField.ToCSharpOperand();
                    ResultFieldOperandVariable = variables.Where(v => v.Name == ResultFieldOperand).FirstOrDefault() ?? Variable.OfUnknownNameBy(ResultFieldOperand);
                }

                if (statement is TimeLine)
                {
                    CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = Time ; {originalComment}"); 
                    return;
                }
                else if (statement is ClearLine)
                {
                    string RecordFormatName = ResultFieldOperand;
                    if(RecordFormatName==string.Empty) RecordFormatName = Factor2Operand;

                    if (Factor2OperandVariable.OfTypeIsArray)
                    {
                        var v = Factor2OperandVariable.TypeOfVariable;
                        CSharpSourceLines.Add($"{indent}{indCond}{RecordFormatName} = Enumerable.Repeat<{v.ArrayItemType.Spelling}>({v.ArrayItemType.InitialValueSpelling}, {v.ArraySize}).ToArray(); {originalComment}");
                    }
                    else
                    if (Factor2OperandVariable.OfTypeIsString)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{RecordFormatName} =  string.Empty; {originalComment}");
                    }
                    else
                    {
                        string EntityName = RecordFormatName.EndsWith("r") ? RecordFormatName[0..^1] : RecordFormatName;
                        CSharpSourceLines.Add($"{indent}{indCond}{RecordFormatName} = new {EntityName}(); {originalComment}");
                    }
                    return;
                }
                else if (statement is CasLine)
                {
                    string SubroutineName = ResultFieldOperand;
                    CSharpSourceLines.Add(indent + "{ " + SubroutineName + "();}" + originalComment);
                    return;
                }
                else
                if (statement is CatLine catline)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");

                    if (Factor1Operand == string.Empty)
                    {
                        var right= Factor2Operand;
                        if(catline.OperationExtender2 == "P") right=$"{right}.PadRight({ResultFieldOperandVariable.TypeLength})";
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} += {right} ; {originalComment}");
                    }
                    else
                    {
                        var right = $"{Factor1Operand} + {Factor2Operand}";
                        if (catline.OperationExtender2 == "P") right = $"({right}).PadRight({ResultFieldOperandVariable.TypeLength})";
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {right} ; {originalComment}");
                    }
                    return;
                }

                if (statement is SetgtLine setgtLine)
                {
                    var KeyName = (Factor1.Trim() == "*HIVAL") ? string.Empty : Factor1Operand;
                    var EntityName = Factor2Operand;
                    if (!rpg.DiskFileNames.Contains(Factor2) && Factor2Operand.EndsWith("r"))
                    {
                        EntityName = Factor2Operand[0..^1];
                    }
                    CSharpSourceLines.Add($"{indent}{indCond}Setgt{EntityName}({KeyName}); {originalComment}");return;
                }
                else
                if (statement is SetllLine setllLine)
                {
                    var KeyName = (Factor1.Trim() == "*LOVAL") ? string.Empty : Factor1Operand;
                    var EntityName = Factor2Operand;
                    if (!rpg.DiskFileNames.Contains(Factor2)&& Factor2Operand.EndsWith("r"))
                    {
                        EntityName = Factor2Operand[0..^1] ;
                    }
                    CSharpSourceLines.Add($"{indent}{indCond}Setll{EntityName}({KeyName}); {originalComment}");return;
                }
                else
                if (statement is ReadpeLine readpeLine)
                {
                    string KeyName = Factor1Operand;
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadeLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readpe", RecordName, IndicatorNumber, originalComment, KeyName);
                    return;
                }
                else
                if (statement is ReadenLine readenLine)
                {
                    string KeyName = Factor1Operand;
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadeLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readen", RecordName, IndicatorNumber, originalComment, KeyName);
                    return;
                }
                else
                if (statement is ReadpenLine)
                {
                    string KeyName = Factor1Operand;
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadeLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Readpen", RecordName, IndicatorNumber, originalComment, KeyName);
                    return;
                }
                else
                if (statement is ReadeLine)
                {
                    string KeyName = Factor1Operand;
                    string RecordName = Factor2Operand;
                    string IndicatorNumber = calculationLine.Eq;
                    ReadeLineGenerateCSharpSourceLines(indent, CSharpSourceLines, "Reade", RecordName, IndicatorNumber, originalComment, KeyName);
                    return;
                }


                if (statement is MoveaLine moveaLine)
                {
                    //string[] In = new string[99];
                    //     C                     MOVEA'1111'    SW        
                    CSharpSourceLines.Add($"{indent}{originalComment}");

                    if (Factor2.StartsWith("'"))
                    {
                        string OnOffValues = Factor2.Replace("'", string.Empty);
                        int StartPosition = 0;
                        if(ResultField.Length>=6 && int.TryParse(ResultField.Substring(4,2),out StartPosition))
                        {
                            for (var i = 0; i < OnOffValues.Length; i++)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{StartPosition + i} = \"{OnOffValues[i]}\";");
                            }
                        }
                        else
                        {
                            for (var i = 0; i < OnOffValues.Length; i++)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}{ResultField.ToCSharpOperand()}[{i}] = \"{OnOffValues[i]}\";");
                            }
                        }
                    }
                    else
                    if (Factor2=="*BLANK")
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}Enumerable.Range(0,{ResultField.ToCSharpOperand()}.Length-1).ToList().ForEach(i => {ResultField.ToCSharpOperand()}[i]=string.Empty);");
                    }
                    else//*BLANK
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}Enumerable.Range(0,{ResultField.ToCSharpOperand()}.Length-1).ToList().ForEach(i => {ResultField.ToCSharpOperand()}[i]={Factor2.ToCSharpOperand()}[i]);");
                    }
                    return;
                }
                else
                if (statement is MoveLine moveLine)
                {
                    var castSpellings = ResultFieldOperandVariable.AddCastSpellingWithRightJustified(Factor2OperandVariable);

                    CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {castSpellings.spelling}; {castSpellings.comment}{originalComment}");
                    return;
                }
                else
                if (statement is ZaddLine || statement is MovelLine)
                {
                    var castSpellings = ResultFieldOperandVariable.AddCastSpelling(Factor2OperandVariable);
                    if(ResultFieldOperandVariable.OfTypeIsArray && Factor2OperandVariable.IsConst)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{castSpellings.spelling}; {originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {castSpellings.spelling}; {castSpellings.comment}{originalComment}"); 
                    }
                    return;
                }
                else
                if (statement is ZsubLine zsubLine)
                {
                    var castSpellings = ResultFieldOperandVariable.SubCastSpelling(Factor2OperandVariable);
                    CSharpSourceLines.Add($"{indent}{ResultFieldOperand} = {castSpellings.spelling}; {castSpellings.comment}{originalComment}");
                    return;
                }

                if (statement is AdddurLine adddurLine)
                {
                    string addDays = Factor2Operand.Split(":")[0];
                    CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {Factor1Operand}.AddDays({addDays}); {originalComment}"); 
                    return;
                }
                else
                if (statement is SubdurLine subdurLine)
                {
                    string subDays = Factor2Operand.Split(":")[0];
                    CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {Factor1Operand}.AddDays(-{subDays}); {originalComment}"); 
                    return;
                }
                else
                if (statement is ChainLine  chainLine)
                {
                    string KeyName = Factor1Operand;
                    var RecordFormatName = Factor2Operand;
                    var EntityName = Factor2Operand;
                    if (!rpg.DiskFileNames.Contains(Factor2) && Factor2Operand.EndsWith("r"))
                    {
                        EntityName = Factor2Operand[0..^1];
                    }
                    string IndicatorNumber = chainLine.Hi;
                    if (RecordFormatName.StartsWith("Pnl"))
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}Chain{EntityName}({KeyName},{IndicatorNumber}); {originalComment}"); return;
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}In{IndicatorNumber} = Chain{EntityName}({KeyName})?\"0\":\"1\"; {originalComment}"); return;
                    }
                }
                else
                if (statement is ChainnLine chainnLine)
                {
                    string KeyName = Factor1Operand;
                    string RecordName = Factor2Operand;
                    var IndicatorNumber = chainnLine.Hi;
                    if (RecordName.StartsWith("Pnl"))
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}Chain{RecordName}({KeyName},{IndicatorNumber}); {originalComment}"); return;
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}In{IndicatorNumber} = Chain{RecordName}({KeyName})?\"0\":\"1\"; {originalComment}"); return;
                    }
                }
                else
                if (statement is CaseqLine caseqLine)
                {
                    var comparisonCondition = ComparisonCondition.Of(Factor1.Trim() + "=" + Factor2.Trim());
                    string SubroutineName = ResultFieldOperand;
                    CSharpSourceLines.Add($"{indent}if ({comparisonCondition.Expression(variables)}) {{ {SubroutineName}();}} {originalComment}");
                    CSharpSourceLines.Add($"{indent}else");
                    return;
                }
                else
                if (statement is SubLine subLine)
                {
                    if (Factor1Operand == string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} -= {Factor2Operand}; {originalComment}");
                    }
                    else
                    {
                        if(int.TryParse(Factor2Operand,out var tmp) && ResultFieldOperandVariable.OfTypeIsShort)
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = (short)({Factor1Operand} - {Factor2Operand}); {originalComment}");
                        }
                        else
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {Factor1Operand} - {Factor2Operand}; {originalComment}");
                        }
                    }
                    return;
                }
                else
                if (statement is MultLine multLine)
                {
                    string LeftOperand = Factor1Operand;
                    string RightOperand = Factor2Operand;
                    string Target = ResultFieldOperand;
                    if (RightOperand == "1")
                    {
                        var targetVariable = variables.Find(v => v.Name == Target);
                        var castSpelling = string.Empty;
                        if (targetVariable != null)
                        {
                            if (targetVariable.OfTypeIsInteger)
                            {
                                castSpelling = $"({targetVariable.TypeSpelling})";
                            }
                        }
                        CSharpSourceLines.Add($"{indent}{indCond}{Target} = {castSpelling}Math.Floor({LeftOperand}); {originalComment}");
                    }
                    else
                    {
                        var LeftOperandVariable = variables.Find(v => v.Name == LeftOperand);
                        var RightOperandVariable = variables.Find(v => v.Name == RightOperand);
                        if(LeftOperandVariable!=null&& LeftOperandVariable.OfTypeIsDecimal
                            ||
                            RightOperandVariable != null && RightOperandVariable.OfTypeIsDecimal)
                        {
                            //Decimal.Multiply(d1, d2)
                            var targetVariable = variables.Find(v => v.Name == Target);
                            var castSpelling = string.Empty;
                            if (targetVariable != null)
                            {
                                if (targetVariable.OfTypeIsInteger)
                                {
                                    castSpelling = $"({targetVariable.TypeSpelling})";
                                }
                                else
                                if (targetVariable.OfTypeIsArray&&targetVariable.TypeOfVariable.ArrayItemType.IsInteger)
                                {
                                    castSpelling = $"({targetVariable.TypeOfVariable.ArrayItemType.Spelling})";
                                }
                            }
                            CSharpSourceLines.Add($"{indent}{indCond}{Target} = {castSpelling}decimal.Multiply({LeftOperand}, {RightOperand}); {originalComment}");
                        }
                        else
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}{Target} = {LeftOperand} * {RightOperand}; {originalComment}");
                        }
                    }
                    return;
                }
                else
                if (statement is MulthLine multhLine)
                {
                    string LeftOperand = Factor1Operand;
                    string RightOperand = Factor2Operand;
                    string Target = ResultFieldOperand;
                    if (RightOperand == "1")
                    {
                        var targetVariable = variables.Find(v => v.Name == Target);
                        var castSpelling = string.Empty;
                        if (targetVariable != null)
                        {
                            if (targetVariable.OfTypeIsInteger)
                            {
                                castSpelling = $"({targetVariable.TypeSpelling})";
                            }
                        }
                        CSharpSourceLines.Add($"{indent}{indCond}{Target} = {castSpelling}Math.Round({LeftOperand}, MidpointRounding.AwayFromZero); {originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{Target} = {LeftOperand} * {RightOperand};//TODO Round {originalComment}");
                    }

                    return;
                }
                else
                if (statement is AddLine addLine)
                {
                    //0030 0038 C   25      1         ADD  DNO       DNO     
                    if (Factor1Operand == string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} += {Factor2Operand}; {originalComment}");
                    }
                    else
                    if (Factor2Operand == "0.99")
                    {
                        var targetVariable = variables.Find(v => v.Name == ResultFieldOperand);
                        var castSpelling = string.Empty;
                        if (targetVariable != null)
                        {
                            if (targetVariable.OfTypeIsInteger)
                            {
                                castSpelling = $"({targetVariable.TypeSpelling})";
                            }
                        }
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {castSpelling}Math.Ceiling({Factor1Operand}); {originalComment}");
                    }
                    else
                    if ((Factor1Operand == ResultFieldOperand && Factor2Operand == "1")||(Factor2Operand == ResultFieldOperand && Factor1Operand == "1"))
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand}++; {originalComment}");
                    }
                    else
                    if ((Factor1Operand == "1"|| Factor2Operand == "1") && ResultFieldOperandVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = (short)({Factor1Operand} + {Factor2Operand}); {originalComment}");
                    }
                    else
                    if (ResultFieldOperandVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = (short)({Factor1Operand} + {Factor2Operand});//TODO:cast short {originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}{ResultFieldOperand} = {Factor1Operand} + {Factor2Operand}; {originalComment}");
                    }

                    return;
                }

                if (statement is CompLine compLine)
                {

                    string LeftOperand = Factor1Operand;
                    string RightOperand = Factor2Operand;
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    if (compLine.IsUniqueIndicator)
                    {
                        if (compLine.Hi != string.Empty && compLine.Lo != string.Empty)
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Hi} = ({LeftOperand} == {RightOperand})?\"0\":\"1\";");
                        }
                        else
                        if (compLine.Eq != string.Empty)//等(EQ): (75 から 76) 演算項目 1 が演算項目 2 と等しい。
                        {
                            if(compLine.Hi != string.Empty)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Eq} = ({LeftOperand} < {RightOperand})?\"0\":\"1\";");
                            }
                            else
                            if (compLine.Lo != string.Empty)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Eq} = ({LeftOperand} > {RightOperand})?\"0\":\"1\";");
                            }
                            else
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Eq} = ({LeftOperand} == {RightOperand})?\"1\":\"0\";");
                            }
                        }
                        else
                        {
                            //高(HI): (71 から 72) 演算項目 1 が演算項目 2 より大きい。
                            if (compLine.Hi != string.Empty)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Hi} = ({LeftOperand} > {RightOperand})?\"1\":\"0\";");
                            }

                            //低(LO): (73 から 74) 演算項目 1 が演算項目 2 より小さい。
                            if (compLine.Lo != string.Empty)
                            {
                                CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Lo} = ({LeftOperand} < {RightOperand})?\"1\":\"0\";");
                            }
                        }
                    }
                    else
                    {
                        //高(HI): (71 から 72) 演算項目 1 が演算項目 2 より大きい。
                        if (compLine.Hi != string.Empty)
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Hi} = ({LeftOperand} > {RightOperand})?\"1\":\"0\";");
                        }
                        //低(LO): (73 から 74) 演算項目 1 が演算項目 2 より小さい。
                        if (compLine.Lo != string.Empty)
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Lo} = ({LeftOperand} < {RightOperand})?\"1\":\"0\";");
                        }
                        //等(EQ): (75 から 76) 演算項目 1 が演算項目 2 と等しい。
                        if (compLine.Eq != string.Empty)
                        {
                            CSharpSourceLines.Add($"{indent}{indCond}In{compLine.Eq} = ({LeftOperand} == {RightOperand})?\"1\":\"0\";");
                        }
                    }

                    return;
                }

                if (statement is LokupLine lokupLine)
                {
                    string LeftOperand = Factor1Operand;
                    string RightOperand = Factor2Operand;
                    
                    //高(HI): (71 から 72) 演算項目 1 が演算項目 2 より大きい。
                    if (lokupLine.Hi != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}NotImplemented {originalComment}");
                    }
                    //低(LO): (73 から 74) 演算項目 1 が演算項目 2 より小さい。
                    if (lokupLine.Lo != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}NotImplemented {originalComment}");
                    }
                    //等(EQ): (75 から 76) 演算項目 1 が演算項目 2 と等しい。
                    if (lokupLine.Eq != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{indCond}In{lokupLine.Eq} = {RightOperand}.Contains({LeftOperand}) ? \"1\":\"0\";{originalComment}");
                    }
                    return;
                }

                if (statement is BitoffLine bitoffLine)
                {
                    string Source;
                    if (calculationLine.Factor2.Trim() == "'01234567'")
                    {
                        Source = "0";
                    }
                    else
                    {
                        Source = Factor2Operand;
                    }
                    if (Source == "0")
                    {
                        CSharpSourceLines.Add($"{indent}{ResultFieldOperand} = \"0\"; {originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{ResultFieldOperand} = {Source}; {originalComment}");//TODO
                    }
                    return;
                }

                if (statement is TestnLine testnLine)
                {
                    
                    if (testnLine.Hi != string.Empty)//結果フィールドに数字が入っている場合。
                    {
                        CSharpSourceLines.Add($"{indent}In{testnLine.Hi} = !{ResultFieldOperand}.Contains(' ') && decimal.TryParse({ResultFieldOperand}.Trim(), out var number{testnLine.StartLineIndexD4})?\"1\":\"0\"; {originalComment}");
                    }
                    else
                    if(testnLine.Lo != string.Empty)//結果フィールドに数字と少なくと も 1 個のブランクが入っている場合
                    {
                        CSharpSourceLines.Add($"{indent}In{testnLine.Lo} = {ResultFieldOperand}.Contains(' ') && decimal.TryParse({ResultFieldOperand}.Trim(), out var number{testnLine.StartLineIndexD4})?\"1\":\"0\"; {originalComment}");
                    }
                    else
                    if (testnLine.Eq != string.Empty)//結果フィールドにすべてブランク が入っている場合。
                    {
                        CSharpSourceLines.Add($"{indent}In{testnLine.Eq} = {ResultFieldOperand}.Trim()==string.Empty?\"1\":\"0\"; {originalComment}");
                    }
                    return;
                }

                //if (statement is EndLine endLine)
                //{
                //    CSharpSourceLines.Add($"{indent}//TODO rpg3 {originalComment}");
                //    return;
                //}

                if (statement is EndifLine endifLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO rpg3 {originalComment}");
                    return;
                }

                if (statement is EnddoLine enddoLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO rpg3 {originalComment}");
                    return;
                }

                if (statement is ParmLine parmLine)
                {
                    CSharpSourceLines.Add($"{indent}//Done {originalComment}");
                    return;
                }
            }

            if (statement is OreqLine oreqLine)
            {
                CSharpSourceLines.Add($"{indent}//TODO rpg3 {originalComment}");
                return;
            }


            throw new NotImplementedException();

        }

        static void ReadLineGenerateCSharpSourceLines(Indent indent, List<string> cSharpSourceLines, string functionName, string recordName, string indicatorNumber, string OriginalComment)
        {
            cSharpSourceLines.Add($"{indent}//var firstEntity = {functionName}{recordName}().FirstOrDefault();In{indicatorNumber} = firstEntity == null?\"1\":\"0\";if(firstEntity != null) {recordName} = firstEntity; {OriginalComment}");

            cSharpSourceLines.Add($"{indent}//var {recordName.ToLower()}s = {functionName}{recordName}().ToList();//TODO: {functionName.ToUpper()} {OriginalComment}");

            cSharpSourceLines.Add($"{indent}/*");
            cSharpSourceLines.Add($"{indent}In{indicatorNumber} = \"0\";");
            cSharpSourceLines.Add($"{indent}var {recordName.ToLower()}s = {functionName}{recordName}().ToList();");
            cSharpSourceLines.Add($"{indent}for (int i = 0; i < {recordName.ToLower()}s.Count; i++)");
            cSharpSourceLines.Add($"{indent}{{");
            cSharpSourceLines.Add($"{indent.Increment()}{recordName} = {recordName.ToLower()}s[i];");
            cSharpSourceLines.Add($"{indent.Increment()}etc..");
            cSharpSourceLines.Add($"{indent.Increment()}if (In{indicatorNumber} == \"1\") break;");
            cSharpSourceLines.Add($"{indent}}}");
            cSharpSourceLines.Add($"{indent}In{indicatorNumber} = \"1\";");
            cSharpSourceLines.Add($"{indent}*/");

        }
        static void ReadeLineGenerateCSharpSourceLines(Indent indent, List<string> cSharpSourceLines, string functionName, string recordName, string indicatorNumber, string OriginalComment, string keyName)
        {
                                                                                                                                                                                          
            cSharpSourceLines.Add($"{indent}//var firstEntity = {functionName}{recordName}({keyName}).FirstOrDefault();In{indicatorNumber} = firstEntity == null?\"1\":\"0\";if(firstEntity != null) {recordName} = firstEntity; {OriginalComment}");

            cSharpSourceLines.Add($"{indent}//var {recordName.ToLower()}s = {functionName}{recordName}({keyName});//TODO: {functionName.ToUpper()} {OriginalComment}");

            cSharpSourceLines.Add($"{indent}/*");
            cSharpSourceLines.Add($"{indent}In{indicatorNumber} = \"0\";");
            cSharpSourceLines.Add($"{indent}var {recordName.ToLower()}s = {functionName}{recordName}({keyName});");
            cSharpSourceLines.Add($"{indent}for (int i = 0; i < {recordName.ToLower()}s.Count; i++)");
            cSharpSourceLines.Add($"{indent}{{");
            cSharpSourceLines.Add($"{indent.Increment()}{recordName} = {recordName.ToLower()}s[i];");
            cSharpSourceLines.Add($"{indent.Increment()}etc..");
            cSharpSourceLines.Add($"{indent.Increment()}if (In{indicatorNumber} == \"1\") break;");
            cSharpSourceLines.Add($"{indent}}}");
            cSharpSourceLines.Add($"{indent}In{indicatorNumber} = \"1\";");
            cSharpSourceLines.Add($"{indent}*/");

        }

        static void EvalLineGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> variables, EvalLine evalLine)
        {
            string originalComment = evalLine.ToOriginalComment();

            string TargetSpelling = evalLine.Factor2andMore.Split("=")[0];

            string SourceSpelling = evalLine.Factor2andMore.Split("=")[1];

            var target = TargetSpelling.ToCSharpOperand();
            var targetVariable = variables.Where(v => v.Name == target).FirstOrDefault() ?? Variable.OfUnknownNameBy(target);

            var source = SourceSpelling.ToCSharpOperand();
            var sourceVariable = variables.Where(v => v.Name == source).FirstOrDefault() ?? Variable.OfUnknownNameBy(source);

            if (!targetVariable.OfTypeIsUnknown && !targetVariable.OfTypeIsArray && !sourceVariable.OfTypeIsUnknown)
            {
                var castSpellings = targetVariable.AddCastSpelling(sourceVariable);
                CSharpSourceLines.Add($"{indent}{targetVariable.Name} = {castSpellings.spelling}; {castSpellings.comment}{originalComment}");
                return;
            }

            var FormulaSpelling = Formula.Of(SourceSpelling);

            if (FormulaSpelling.EndsWith(" *  - 1"))
            {
                var newFormula = FormulaSpelling.Replace(" *  - 1", string.Empty);
                var newFormulaVariable = variables.Find(v => v.Name == newFormula);
                var castNewFormulaSpelling = Variable.GetCastSpelling(newFormulaVariable, targetVariable);
                CSharpSourceLines.Add($"{indent}{target} = -({castNewFormulaSpelling}{newFormula}); {originalComment}");
                return;
            }

            var arithmetics = new char[2] { '+', '-' };
            foreach (var a in arithmetics)
            {
                if (FormulaSpelling.Count(c => c == a) != 1) continue;
                var operands = FormulaSpelling.Split(a);
                int intvalue;
                if (int.TryParse(operands[1].Trim(), out intvalue))
                {
                    var operand = operands[0].ToCSharpOperand();
                    if (target == operand)
                    {
                        CSharpSourceLines.Add($"{indent}{target} {a}= {intvalue}; {originalComment}");
                        return;
                    }

                    if (targetVariable != null && targetVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{target} = (short)({operand} {a} {intvalue}); {originalComment}");
                        return;
                    }
                }
                if (int.TryParse(operands[0].Trim(), out intvalue))
                {
                    var operand = operands[1].ToCSharpOperand();
                    if (targetVariable != null && targetVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{target} = (short)({intvalue} {a} {operand}); {originalComment}");
                        return;
                    }
                }
            }

            var formula = FormulaSpelling;

            if (targetVariable.OfTypeIsArray)
            {
                CSharpSourceLines.Add($"{indent}for (int i = 0; i < {target}.Length; i++) {target}[i] = {formula}; {originalComment}");
                return;
            }

            if (targetVariable.OfTypeIsShort && formula.Contains("0.5"))
            {
                CSharpSourceLines.Add($"{indent}{target} = (short)({formula}); {originalComment}");
                return;
            }

            if (IsArithmeticOperationsContainedIn(FormulaSpelling))
            {
                CSharpSourceLines.Add($"{indent}{target} = {FormulaSpelling}; {originalComment}");
            }
            else
            {
                var castSpellings = targetVariable.AddCastSpelling(sourceVariable);
                CSharpSourceLines.Add($"{indent}{targetVariable.Name} = {castSpellings.spelling}; {castSpellings.comment}{originalComment}");
            }
        }
        static bool IsArithmeticOperationsContainedIn(string source) => source.IndexOfAny(new char[] { '+', '-', '*', '/', }) > 0;

        static void DivBlockGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> variables, DivBlock divBlock)
        {
            var openerLine=divBlock.openerLine;
            var LeftOperand = openerLine.Factor1.ToCSharpOperand();
            var RightOperand = openerLine.Factor2.ToCSharpOperand();
            string openerTarget = openerLine.ResultField.ToCSharpOperand();
            var closerLine = divBlock.closerLine;
            if (closerLine != null)
            {
                string closerTarget = closerLine.ResultField.ToCSharpOperand();
                int rightOperand = 0;
                if (int.TryParse(RightOperand, out rightOperand))
                {
                    var openerTargetVariable = variables.Find(v => v.Name == openerTarget);
                    if (openerTargetVariable != null && openerTargetVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{openerTarget} = (short)({LeftOperand} / {RightOperand}); {openerLine.ToOriginalComment()}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{openerTarget} = {LeftOperand} / {RightOperand}; {openerLine.ToOriginalComment()}");
                    }
                    var closerTargetVariable = variables.Find(v => v.Name == closerTarget);

                    if (closerTargetVariable != null && closerTargetVariable.OfTypeIsShort)
                    {
                        CSharpSourceLines.Add($"{indent}{closerTarget} = (short)({LeftOperand} % {RightOperand}); {closerLine.ToOriginalComment()}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{closerTarget} = {LeftOperand} % {RightOperand}; {closerLine.ToOriginalComment()}");
                    }
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{openerTarget} = {LeftOperand} / {RightOperand}; {openerLine.ToOriginalComment()}");
                    CSharpSourceLines.Add($"{indent}{closerTarget} = {LeftOperand} % {RightOperand}; {closerLine.ToOriginalComment()}");
                }
            }
            else
            {
                if (LeftOperand == string.Empty)
                {
                    CSharpSourceLines.Add($"{indent}{openerTarget} /= {RightOperand}; {openerLine.ToOriginalComment()}");
                }
                else
                {
                    var openerTargetVariable = variables.Find(v => v.Name == openerTarget);
                    if (openerTargetVariable != null && openerTargetVariable.OfTypeIsDecimal)
                    {
                        //0200      C           CRSU,I    DIV  CASUM     DIV    113         || | |   
                        //Div = decimal.Round(decimal.Divide(Crsu[I - 1], Casum), 3, MidpointRounding.ToZero);
                        CSharpSourceLines.Add($"{indent}{openerTarget} = decimal.Round(decimal.Divide({LeftOperand} , {RightOperand}), {openerTargetVariable.TypeNumberOfDecimalPlaces}, MidpointRounding.ToZero); {openerLine.ToOriginalComment()}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{openerTarget} = {LeftOperand} / {RightOperand}; {openerLine.ToOriginalComment()}");
                    }
                }
            }

        }

        void KlistGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> variables, Klist klist, 
            IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var klistLine=klist.klistLine;

            CSharpSourceLines.Add($"{indent}{klistLine.ToOriginalComment()}");

            var Statements=klist.Statements;

            Statements.ForEach(line =>
            {
                //done //0021      C                   KFLD                    UBDEVRNO1      
                CSharpSourceLines.Add($"{indent}{line.ToOriginalComment()}");
            });

            if (Statements.Count == 1)
            {
                //string Sykey => Sysycd;
                CSharpSourceLines.Add($"{indent}{SetTypeOfKlist(Statements[0], FieldNameModernTypeNames, variables).Spelling} {klistLine.Factor1.ToCSharpOperand()} => {Statements[0].ResultField.ToCSharpOperand()};");
                //0065      C     SYKEY         KLIST                                              
                //0066      C                   KFLD                    SYSYCD      
            }
            else
            {
                //(string ubdevrno, string ubdevrno1) ubkey => (ubdevrno, ubdevrno1); //0019      C     UBKEY         KLIST     
                var fieldVariables = Statements.Select(kfld => Variable.Of(SetTypeOfKlist(kfld, FieldNameModernTypeNames, variables), kfld.ResultField.ToCSharpOperand())).ToList();

                var tuppleType = $"({string.Join(',', fieldVariables.Select(kfld => kfld.TypeSpelling + " " + kfld.Name.ToLower()).ToArray())})";

                var names = new List<string>();
                fieldVariables.ForEach(targetVariable => {
                    var sourceVariable = variables.Where(v => v.Name.ToLower() == targetVariable.Name.ToLower()).FirstOrDefault() ?? Variable.Of(TypeOfVariable.OfUnknown, targetVariable.Name);
                    var castSpellings = targetVariable.AddCastSpelling(sourceVariable);
                    names.Add(castSpellings.spelling);
                });

                CSharpSourceLines.Add($"{indent}{tuppleType} {klistLine.Factor1.ToCSharpOperand()} => ({string.Join(',', names)});");
            }

            CSharpSourceLines.Add(string.Empty);
        }

        TypeOfVariable SetTypeOfKlist(KfldLine kfldLine, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames, List<Variable> variables)
        {
            string name = kfldLine.ResultField.TrimEnd();

            var other= variables.Where(v=>v.Name==name).FirstOrDefault();
            if(other!=null) return other.TypeOfVariable;

            IDataTypeDefinition t=null;
            var r = FieldNameModernTypeNames.Where(kv => kv.Name.TrimEnd() == name).Select(l => l.RecordFormatFieldLine).FirstOrDefault();
            if (r != null)
            {
                t =DiskFileStructureFactory.CreateTypeDefinition(r, null);
            }
            else
            {
                t = (IDataTypeDefinition)DataTypeDefinition.Of(name, kfldLine.FieldLength, string.Empty, kfldLine.DecimalPositions);

            }
            var tOfV= TypeOfVariableFactory.Of(t);
            variables.Add(Variable.Of(tOfV,name));
            return tOfV;
        }

        static void PlistGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> variables, Plist plist)
        {
            //Plist
            var plstLine=plist.plstLine;

            CSharpSourceLines.Add($"{indent}{plstLine.ToOriginalComment()}");

            var Statements=plist.Statements;

            Statements.ForEach(parm =>
            {
                var parmVariable = VariableFactory.Of(parm);

                if (!variables.Exists(v => v.Name == parmVariable.Name))
                {//TODO:string parmLine
                    CSharpSourceLines.Add($"{indent}{PropertyItem.OfInternal(parmVariable).ToAutoImplementedPropertiesString()}{parm.ToOriginalComment()}");
                    variables.Add(parmVariable);
                }
            });

            if (!plstLine.IsEntry)
            {
                Variable plstVariable = null;
                if (Statements.Count == 1)
                {
                    var variable = VariableFactory.Of(Statements[0]);
                    if (variables.Exists(v => v.Name == variable.Name))
                    {//TODO:string parmLine
                        variable = variables.First(v => v.Name == variable.Name);
                    }
                    else
                    {
                        variables.Add(variable);
                    }

                    plstVariable = variable.Of(plstLine.Factor1.ToCSharpOperand());

                    CSharpSourceLines.Add($"{indent}{plstVariable.TypeSpelling} {plstVariable.Name} => {variable.Name};");

                    CSharpSourceLines.Add($"{indent}{plstVariable.TypeSpelling} {plstVariable.Name}");
                    CSharpSourceLines.Add($"{indent}{{");
                    CSharpSourceLines.Add($"{indent.Increment()}get");
                    CSharpSourceLines.Add($"{indent.Increment()}{{");
                    CSharpSourceLines.Add($"{indent.Increment().Increment()}return {variable.Name};");
                    CSharpSourceLines.Add($"{indent.Increment()}}}");
                    CSharpSourceLines.Add($"{indent.Increment()}set");
                    CSharpSourceLines.Add($"{indent.Increment()}{{");
                    CSharpSourceLines.Add($"{indent.Increment().Increment()}{variable.Name} = value;");
                    CSharpSourceLines.Add($"{indent.Increment()}}}");
                    CSharpSourceLines.Add($"{indent}}}");
                }
                else
                {
                    var parameters = string.Join(',', Statements.Select(statement =>
                    {
                        var variable = VariableFactory.Of(statement);
                        if (variables.Exists(v => v.Name == variable.Name))
                        {//TODO:string parmLine
                            variable = variables.First(v => v.Name == variable.Name);
                        }
                        else
                        {
                            variable = variables.First(v => v.Name == variable.Name);
                        }
                        return variable.TypeSpelling + " " + variable.CamelCaseName;
                    }).ToArray());

                    //CSharpSourceLines.Add($"{indent}({parameters}) {plstLine.Name} => ({string.Join(',', Statements.Select(kfld => kfld.Variable.Name).ToArray())});");

                    plstVariable = Variable.Of(TypeOfVariable.OfNoLength($"({parameters})"), plstLine.Factor1.ToCSharpOperand());

                    CSharpSourceLines.Add($"{indent}{plstVariable.TypeSpelling} {plstVariable.Name}");
                    CSharpSourceLines.Add($"{indent}{{");
                    CSharpSourceLines.Add($"{indent.Increment()}get");
                    CSharpSourceLines.Add($"{indent.Increment()}{{");
                    var names = Statements.Select(kfld => VariableFactory.Of(kfld).Name).ToArray();
                    CSharpSourceLines.Add($"{indent.Increment().Increment()}return ({string.Join(',', names)});");
                    CSharpSourceLines.Add($"{indent.Increment()}}}");
                    CSharpSourceLines.Add($"{indent.Increment()}set");
                    CSharpSourceLines.Add($"{indent.Increment()}{{");
                    names.ToList().ForEach(name => CSharpSourceLines.Add($"{indent.Increment().Increment()}{name} = value.{name.ToLower()};"));
                    CSharpSourceLines.Add($"{indent.Increment()}}}");
                    CSharpSourceLines.Add($"{indent}}}");
                }

                variables.Add(plstVariable);

                CSharpSourceLines.Add(string.Empty);
            }

        }

        void RPGCallLineGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, RPGCallLine RPGCallLine)
        {
            var objectID = ObjectIDFactory.Create(RPGCallLine.Library, RPGCallLine.ProgramName);

            var pgm = ProgramStructureFactory.Create(objectID);

            CallProgramStatementCSharpGenerater.Generate(RPGCallLine.ThisCallProgramStatement(pgm), indent, CSharpSourceLines, variables);

        }

        void MethodBlockStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, MethodBlockStatement MethodBlockStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            CSharpSourceLines.Add($"{indent}void {MethodBlockStatement.Name.ToPublicModernName()}() {(MethodBlockStatement.NameComment==null?string.Empty:MethodBlockStatement.NameComment.ToOriginalComment())}");
            CSharpSourceLines.Add(indent + "{");

            MethodBlockStatement.ExfmtLineNumbers.ForEach(exfmtLineNumber => GoToAfterExfmtToCSharp.GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), exfmtLineNumber, true));

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(),rpg, variables, MethodBlockStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add(ClosingStatement(MethodBlockStatement,indent));

        }

        static string ClosingStatement(MethodBlockStatement MethodBlockStatement,Indent indent)
        {
            var CloseStatement= MethodBlockStatement.CloseStatement;
            if (CloseStatement == null) return $"{indent}}}";

            if(CloseStatement is EndsrLine endsrLine)
            {
                string label = string.Empty;

                if (endsrLine.Factor1.Trim() != string.Empty)
                {
                    label = $"{endsrLine.Factor1.ToCSharpOperand().ToLower()}:;";
                }
                var OriginalComment = ((ILine)endsrLine).ToOriginalComment();
                return $"{indent}{label}}} {OriginalComment}";
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        void DoStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, DoBlockStatement DoStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = DoStatement.openerLine;
            var closerLine = DoStatement.closerLine;

            if(openerLine is DoWithFactor1Line)
            {
                CSharpSourceLines.Add($"{indent}while (({ConditionExpression(variables, openerLine)})){{ {((Line)openerLine).ToOriginalComment()}");
            }
            else
            {
                CSharpSourceLines.Add($"{indent}for(var i{openerLine.StartLineIndexD4}=0;i{openerLine.StartLineIndexD4}==0;i{openerLine.StartLineIndexD4}++){{ {((Line)openerLine).ToOriginalComment()}");
            }

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(),rpg, variables, DoStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }

        void DouStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg,List<Variable> variables, DouBlockStatement DouStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine= DouStatement.openerLine;
            var closerLine = DouStatement.closerLine;

            CSharpSourceLines.Add($"{indent}while (!({ConditionExpression(variables, openerLine)})){{ {((Line)openerLine).ToOriginalComment()}");

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(),rpg, variables, DouStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }
        void DoueqStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, DoueqBlockStatement DouStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = DouStatement.openerLine;
            var closerLine = DouStatement.closerLine;

            CSharpSourceLines.Add($"{indent}while (!({ConditionExpression(variables, openerLine)})){{ {((Line)openerLine).ToOriginalComment()}");

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), rpg,variables, DouStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }
        void DoweqStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, DoweqBlockStatement DouStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = DouStatement.openerLine;
            var closerLine = DouStatement.closerLine;

            CSharpSourceLines.Add($"{indent}while (({ConditionExpression(variables, openerLine)})){{ {((Line)openerLine).ToOriginalComment()}");

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(),rpg, variables, DouStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }
        void DowgtStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, DowgtBlockStatement DouStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = DouStatement.openerLine;
            var closerLine = DouStatement.closerLine;

            CSharpSourceLines.Add($"{indent}while (({ConditionExpression(variables, openerLine)})){{ {((Line)openerLine).ToOriginalComment()}");

            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), rpg, variables, DouStatement.Statements, FieldNameModernTypeNames);

            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }
        void DoBlockStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines,Indent indent, RPGStructure rpg,  List<Variable> variables, DoForBlockStatement DoBlockStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = DoBlockStatement.openerLine;
            var closerLine = DoBlockStatement.closerLine;

            CSharpSourceLines.Add($"{indent}for ({ConditionExpression(variables, openerLine)}) {((Line)openerLine).ToOriginalComment()}");
            CSharpSourceLines.Add(indent + "{");
            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), rpg, variables, DoBlockStatement.Statements, FieldNameModernTypeNames);
            CSharpSourceLines.Add(indent + "} " + closerLine.ToOriginalComment());

        }
        void IfBlockStatement3GenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, IfBlockStatement3 IfBlockStatement3, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = (CalculationLine)IfBlockStatement3.openerLine;

            var Factor1Operand = ToOperand(openerLine.Factor1);
            var Factor1OperandVariable = variables.Where(v => v.Name == Factor1Operand).FirstOrDefault() ?? Variable.OfUnknownNameBy(Factor1Operand);
            var Factor2Operand = ToOperand(openerLine.Factor2);
            var Factor2OperandVariable = variables.Where(v => v.Name == Factor2Operand).FirstOrDefault() ?? Variable.OfUnknownNameBy(Factor2Operand);

            var isStringCompare = Factor1OperandVariable.OfTypeIsString && Factor2OperandVariable.OfTypeIsString;

            CSharpSourceLines.Add($"{indent}if(");

            if (openerLine is IfeqLine || openerLine is IfneLine)
            {
                var ope= openerLine is IfeqLine? "==" : "!=";
                if (Factor1OperandVariable.OfTypeIsArray && Factor1OperandVariable.TypeOfVariable.ArrayItemType.IsByte && Factor2Operand=="string.Empty")
                {
                    CSharpSourceLines.Add($"{indent}{Factor1Operand}.Any(b => b {ope} CodePage290.ByteOfSpace) {openerLine.ToOriginalComment()}");
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{Factor1Operand} {ope} {Factor2Operand} {openerLine.ToOriginalComment()}");
                }
            }
            else
            if (openerLine is IfleLine ifleLine)
            {
                if (isStringCompare)
                {
                    CSharpSourceLines.Add($"{indent}CodePage290Comparator.Compare({Factor1Operand}, {Factor2Operand}) <= 0 {openerLine.ToOriginalComment()}");
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{Factor1Operand} <= {Factor2Operand} {openerLine.ToOriginalComment()}");
                }
            }
            else
            if (openerLine is IfgeLine ifgeLine)
            {
                if (isStringCompare)
                {
                    CSharpSourceLines.Add($"{indent}CodePage290Comparator.Compare({Factor1Operand}, {Factor2Operand}) >= 0 {openerLine.ToOriginalComment()}");
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{Factor1Operand} >= {Factor2Operand} {openerLine.ToOriginalComment()}");
                }
            }
            else
            if (openerLine is IfltLine ifltLine)
            {
                if (isStringCompare)
                {
                    CSharpSourceLines.Add($"{indent}CodePage290Comparator.Compare({Factor1Operand}, {Factor2Operand}) < 0 {openerLine.ToOriginalComment()}");
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{Factor1Operand} < {Factor2Operand} {openerLine.ToOriginalComment()}");
                }
            }
            else
            if (openerLine is IfgtLine ifgtLine)
            {
                CSharpSourceLines.Add($"{indent}{Factor1Operand} > {Factor2Operand} {openerLine.ToOriginalComment()}");
            }

            foreach(var line in IfBlockStatement3.Statements.TakeWhile(l=> !IfBlockStatement3.IsContent(l)).Cast<ILine>().ToList())
            {
                var comment = line.ToOriginalComment();
                if (line is ICommentStatement)
                {
                    CSharpSourceLines.Add($"{indent}{comment}");
                    continue;
                }

                if (!(line is CalculationLine))
                {
                    throw new NotImplementedException();
                }

                var logicalOperator = string.Empty;
                var comparelOperator = string.Empty;

                if (line is AndeqLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = "==";
                }
                else
                if (line is AndneLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = "!=";
                }
                else
                if (line is AndgtLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = ">";
                }
                else
                if (line is AndltLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = "<";
                }
                else
                if (line is AndgeLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = ">=";
                }
                else
                if (line is AndleLine)
                {
                    logicalOperator = "&&";
                    comparelOperator = "<=";
                }
                else
                if (line is OreqLine)
                {
                    logicalOperator = "||";
                    comparelOperator = "==";
                }
                else
                if (line is OrgeLine)
                {
                    logicalOperator = "||";
                    comparelOperator = ">=";

                }
                else 
                if (line is OrgtLine)
                {
                    logicalOperator = "||";
                    comparelOperator = ">";

                }
                else
                if (line is OrltLine)
                {
                    logicalOperator = "||";
                    comparelOperator = "<";
                }
                else
                if (line is OrneLine)
                {
                    logicalOperator = "||";
                    comparelOperator = "!=";
                }
                else
                {
                    throw new NotImplementedException();
                }

                var calculationLine = (CalculationLine)line;

                var leftOperand = ToOperand(calculationLine.Factor1);
                var rightOperand = ToOperand(calculationLine.Factor2);

                var leftOperandVariable = variables.Where(v => v.Name == leftOperand).FirstOrDefault() ?? Variable.OfUnknownNameBy(leftOperand);
                var rightOperandVariable = variables.Where(v => v.Name == rightOperand).FirstOrDefault() ?? Variable.OfUnknownNameBy(rightOperand);

                var isStringCompareSub = leftOperandVariable.OfTypeIsString && rightOperandVariable.OfTypeIsString;

                if (isStringCompareSub && comparelOperator!="==" && comparelOperator != "!=")
                {
                    CSharpSourceLines.Add($"{indent}{logicalOperator} CodePage290Comparator.Compare({leftOperand}, {rightOperand}) {comparelOperator} 0 {comment}");
                }
                else
                {
                    CSharpSourceLines.Add($"{indent}{logicalOperator} {leftOperand} {comparelOperator} {rightOperand} {comment}");
                }

            }

            var closerLine = IfBlockStatement3.closerLine;

            CSharpSourceLines.Add($"{indent})");
            CSharpSourceLines.Add($"{indent}{{");
            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), rpg, variables, 
                IfBlockStatement3.Statements.SkipWhile(l => !IfBlockStatement3.IsContent(l)).ToList(), 
                FieldNameModernTypeNames);
            CSharpSourceLines.Add($"{indent}}} {((Line)closerLine).ToOriginalComment()}");
        }

        void IfBlockStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, IfBlockStatement IfBlockStatement, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = IfBlockStatement.openerLine;
            var closerLine = IfBlockStatement.closerLine;

            CSharpSourceLines.Add($"{indent}if({ConditionExpression(variables, (CalculationLine)openerLine)}) {((Line)openerLine).ToOriginalComment()}");
            CSharpSourceLines.Add(indent + "{");
            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), rpg, variables, IfBlockStatement.Statements,FieldNameModernTypeNames);
            CSharpSourceLines.Add(indent + "} " + ((Line)closerLine).ToOriginalComment());
        }

        static string ToOperand(string original)
        {
            string operand = string.Empty;

            if (original.Contains(',') && !(original.StartsWith('\'') && original.EndsWith('\'')) )
            {
                var splitted = original.Split(',');
                operand = $"{splitted[0].ToCSharpOperand()}[{splitted[1].ToCSharpOperand()}]";
            }
            else
            {
                operand = original.ToCSharpOperand();
            }
            return operand;
        }
        static string ConditionExpression(List<Variable> variables, CalculationLine statement)
        {

            if (statement is IfLine ifLine)
            {
                return LogicalCondition.Of(ifLine.Factor2andMore, variables).Expression;
            }
            else if (statement is DoWithFactor1Line doWithFactor1Line)
            {
                if (doWithFactor1Line.Indicator1.Trim() == string.Empty)
                {
                    return "true";
                }
                else
                {
                    return $"In{doWithFactor1Line.Indicator1.Trim()} == \"1\"";
                }
            }
            else if (statement is DoLine doline)
            {
                throw new NotImplementedException();
            }
            else if (statement is DouLine douLine)
            {
                return LogicalCondition.Of(douLine.Factor2andMore, variables).Expression;
            }
            else if (statement is DoueqLine || statement is DoweqLine)
            {
                //0060      C           *INLR     DOUEQ*ON   
                return LogicalCondition.Of($"{statement.Factor1} = {statement.Factor2}", variables).Expression;
            }
            else if (statement is DowgtLine)
            {
                return LogicalCondition.Of($"{statement.Factor1} > {statement.Factor2}", variables).Expression;
            }
            else if (statement is DoForLine doForLine)
            {
                var typeDec = "int ";

                string IndexName = doForLine.ResultField.ToCSharpOperand() == string.Empty ? $"i{doForLine.StartLineIndex}" : doForLine.ResultField.ToCSharpOperand();

                if (variables.Any(variables => variables.Name == IndexName)) typeDec = string.Empty;

                string StartIndex = doForLine.Factor1.Trim() == string.Empty ? "1" : doForLine.Factor1.ToCSharpOperand();
                string EndIndex = doForLine.Factor2.Trim() == string.Empty ? "1" : doForLine.Factor2.ToCSharpOperand();
                if(doForLine.Factor2.Trim() == "*HIVAL") EndIndex = "int.MaxValue";

                return $"{typeDec}{IndexName} = {StartIndex}; {IndexName} <= {EndIndex}; {IndexName}++";
            }
            else
            {
                throw new NotImplementedException();
                //return statement.ConditionExpression(variables);
            }
        }

        void SelectBlockGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, RPGStructure rpg, List<Variable> variables, SelectBlock SelectBlock, IEnumerable<(IDDSLine RecordFormatFieldLine, string Name)> FieldNameModernTypeNames)
        {
            var openerLine = SelectBlock.openerLine;
            var closerLine = SelectBlock.closerLine;

            CSharpSourceLines.Add($"{indent}{((Line)openerLine).ToOriginalComment()}");

            var enteredWhenLine = false;
            var Statements=SelectBlock.Statements;

            for (var i = 0; i < Statements.Count; i++)
            {
                var statement = Statements[i];
                if (statement is OreqLine oreqLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:{oreqLine.ToOriginalComment()}");
                }
                else
                if (statement is AndgtLine andgtLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:{andgtLine.ToOriginalComment()}");
                }
                else
                if (statement is AndleLine andleLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:{andleLine.ToOriginalComment()}");
                }
                else
                if (statement is WhenLine whenLine)
                {
                    if (enteredWhenLine)
                    {
                        CSharpSourceLines.Add(indent + "}");
                        CSharpSourceLines.Add(indent + "else");
                    }
                    enteredWhenLine = true;
                    CSharpSourceLines.Add($"{indent}if ({LogicalCondition.Of(whenLine.Factor2andMore, variables).Expression }) {{ {whenLine.ToOriginalComment()}");
                }
                else
                if (statement is WheqLine wheqLine)
                {
                    if (enteredWhenLine)
                    {
                        CSharpSourceLines.Add(indent + "}");
                        CSharpSourceLines.Add(indent + "else");
                    }
                    enteredWhenLine = true;
                    CSharpSourceLines.Add($"{indent}if ({LogicalCondition.Of($"{wheqLine.Factor1} = {wheqLine.Factor2}", variables).Expression }) {{ {wheqLine.ToOriginalComment()}");
                }
                else if (statement is OtherLine otherLine)
                {
                    if (enteredWhenLine)
                    {
                        CSharpSourceLines.Add(indent + "}");
                        CSharpSourceLines.Add(indent + "else");
                    }
                    enteredWhenLine = true;
                    CSharpSourceLines.Add($"{indent}{{{otherLine.ToOriginalComment()}");
                }
                else
                {
                    GenerateCSharpSourceLines(CSharpSourceLines, enteredWhenLine ? indent.Increment() : indent, rpg, variables, statement, FieldNameModernTypeNames);
                }
            }

            CSharpSourceLines.Add(indent + "}" + closerLine.ToOriginalComment());

        }

        void OutputBlockGenerateCSharpSourceLines(OutputBlock element, List<string> CSharpSourceLines, List<Variable> variables)
        {
            element.Statements.ForEach(statement =>
            {
                if (statement is OutputRowsContainerBlock outputRowsContainerBlock)
                {
                    OutputRowsContainerBlockGenerateCSharpSourceLines(outputRowsContainerBlock, CSharpSourceLines, variables);
                }
                else
                if (statement is ICommentStatement)
                {

                    if (statement is Line)
                    {
                        CSharpSourceLines.Add($"{((Line)statement).ToOriginalComment()}");
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                {
                    throw new NotImplementedException();
                }
            });
        }

        void OutputRowsContainerBlockGenerateCSharpSourceLines(OutputRowsContainerBlock statement, List<string> CSharpSourceLines, List<Variable> variables)
        {
            if (statement.IsForDiskFile)
            {
                OutputRowsContainerBlockGenerateCSharpSourceLinesForDisk(statement, CSharpSourceLines, variables);
            }
            else
            {
                OutputRowsContainerBlockGenerateCSharpSourceLinesForPrinter(statement, CSharpSourceLines, variables);
            }
        }
        void OutputRowsContainerBlockGenerateCSharpSourceLinesForDisk(OutputRowsContainerBlock statement, List<string> CSharpSourceLines, List<Variable> variables)
        {
            var Statements = statement.Statements;

            Indent indent = Indent.Zero;
            var containerName = Statements[0].Name.ToPublicModernName();
            var RecordLength = Statements[0].RecordLength;
            if (RecordLength == string.Empty) RecordLength = "string.Empty";

            Enumerable.Range(1, Statements.Count).ToList().ForEach(i => {

                var prefix= $"{containerName}{(i==1?string.Empty:i)}";
                CSharpSourceLines.Add($"{indent}void Excpt({prefix}RecordFormatSpecification recordFormat)");
                CSharpSourceLines.Add($"{indent}{{");
                CSharpSourceLines.Add($"{indent.Increment()}{statement.FileName.ToPublicModernName().ToLower()}Repository.Insert(recordFormat.RecordFormat());");
                CSharpSourceLines.Add($"{indent}}}");

                var outputRowBlock = Statements[i - 1];

                outputRowBlock.Statements.ToList().ForEach(item => CSharpSourceLines.Add($"{indent}{item.ToOriginalComment()}"));

                try
                {
                    ProgramDescribedFileToCSharper.GenerateOutputRowsByProgramDescribed(CSharpSourceLines, RecordLength, prefix, true, outputRowBlock, variables);
                }
                catch
                {
                    Console.WriteLine($"error OutputRowsContainerBlockGenerateCSharpSourceLinesForDisk {string.Join(",", TargetRpgObjectID.ToClassification())}");
                    outputRowBlock.Statements.ToList().ForEach(item => CSharpSourceLines.Add($"{indent}//error generate {item.ToOriginalComment()}"));
                }

            });

        }

        void OutputRowsContainerBlockGenerateCSharpSourceLinesForPrinter(OutputRowsContainerBlock statement, List<string> CSharpSourceLines, List<Variable> variables)
        {
            var Statements = statement.Statements;

            Indent indent = Indent.Zero;
            var containerName = Statements[0].Name.ToPublicModernName();
            if (containerName == string.Empty)
            {
                CSharpSourceLines.Add($"{indent}void Excpt()");
                CSharpSourceLines.Add($"{indent}{{");
                CSharpSourceLines.Add($"{indent.Increment()}{statement.FileName.ToPublicModernName().ToLower()}.Excpt(E);");
                CSharpSourceLines.Add($"{indent}}}");
                containerName="E";
            }
            else
            {
                CSharpSourceLines.Add($"{indent}void Excpt({containerName}OutputRowsContainer rows)");
                CSharpSourceLines.Add($"{indent}{{");
                CSharpSourceLines.Add($"{indent.Increment()}{statement.FileName.ToPublicModernName().ToLower()}.Excpt(rows);");
                CSharpSourceLines.Add($"{indent}}}");
            }

            GeneratorFromPrtf.Output(CSharpSourceLines, containerName, Statements.Count);

            Enumerable.Range(1, Statements.Count).ToList().ForEach(i => {
                var outputRowBlock = Statements[i - 1];

                outputRowBlock.Statements.ToList().ForEach(item => CSharpSourceLines.Add($"{indent}{item.ToOriginalComment()}"));

                GeneratorFromPrtf.GenerateOutputRowsByProgramDescribed(CSharpSourceLines, containerName, i, true, outputRowBlock, variables);

            });

        }
    }
}

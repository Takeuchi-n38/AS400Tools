using Delta.AS400.DataTypes;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.DisplayFiles.Commands;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Generator.PresentationLayer.Presenters;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Sources;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Delta.Tools.AS400.Generator
{
    public class GeneratorFromDspf
    {
        Library mainLibrary;
        PathResolverForTool PathResolver;
        DiskFileStructureBuilder DiskFileStructureFactory;
        ObjectIDFactory ObjectIDFactory;
        ISourceFactory dspfSourceReader;
        public GeneratorFromDspf(Library mainLibrary, PathResolverForTool PathResolver,DiskFileStructureBuilder DiskFileStructureFactory, ISourceFactory reader)
        {
            this.mainLibrary= mainLibrary;
            this.PathResolver= PathResolver;
            this.DiskFileStructureFactory= DiskFileStructureFactory;
            this.ObjectIDFactory= DiskFileStructureFactory.ObjectIDFactory;
            this.dspfSourceReader = reader;
        }

        public void Generate(bool isCLnotRPG, ObjectID dspfObjectID,ObjectID serviceObjectID, List<Variable> parametersOfService, bool IsCallingInService,
IEnumerable<ObjectID> fileDifinitionObjectIDs, IEnumerable<string> externalDiskFileNames,
            IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines)
        {
            Source dspfSource = dspfSourceReader.Read(dspfObjectID);

            var dspf = Create(dspfSource);

            var appFolderPath = PathResolver.ApplicationServiceFolderPath(dspf.OriginalSource.ObjectID);
            var preFolderPath = PathResolver.PresentatioFileFolderPath(dspf.OriginalSource.ObjectID);

            var iPresenter = IPresenterStructureFactory.Create(PathResolver,dspf);
            PathResolver.WriteIPresenterSource(dspf.OriginalSource.ObjectID, iPresenter.FileName, ((ISourceFile)iPresenter).ToSourceContents());

            dspf.RecordFormatHeaderList.ForEach(RecordFormatHeader => {

                var ViewModelDTO = ViewModelDTOFactory.Create(PathResolver,dspf.OriginalSource.ObjectID, RecordFormatHeader);
                PathResolver.WriteViewModelDTOSource(dspf.OriginalSource.ObjectID, ViewModelDTO.FileName, ((ISourceFile)ViewModelDTO).ToSourceContents());

                var RecordFormatXamlCs = RecordFormatXamlCsFactory.Create(PathResolver,dspf, serviceObjectID, RecordFormatHeader);
                PathResolver.WriteRecordFormatXamlCsSource(dspf.OriginalSource.ObjectID, RecordFormatXamlCs.FileName, ((ISourceFile)RecordFormatXamlCs).ToSourceContents());

                var RecordFormatXaml = RecordFormatXamlFactory.Create(PathResolver,dspf, RecordFormatHeader);
                PathResolver.WriteRecordFormatXamlSource(dspf.OriginalSource.ObjectID, RecordFormatXaml.Name, ((ISourceFile)RecordFormatXaml).ToSourceContents());

            });

            var DisplayFileXamlCs = DisplayFileXamlCsFactory.Create(isCLnotRPG, mainLibrary, dspf, serviceObjectID, parametersOfService, IsCallingInService, fileDifinitionObjectIDs, externalDiskFileNames, PrinterFileDescriptionLines);
            PathResolver.WriteDisplayFileXamlCsSource(dspf.OriginalSource.ObjectID, DisplayFileXamlCs.FileName, ((ISourceFile)DisplayFileXamlCs).ToSourceContents());

            var DisplayFileXaml = DisplayFileXamlFactory.Create(PathResolver, dspf);
            PathResolver.WriteDisplayFileXamlSource(dspf.OriginalSource.ObjectID, DisplayFileXaml.Name, ((ISourceFile)DisplayFileXaml).ToSourceContents());

        }

        public IEnumerable<string> CreateContentsForService(ObjectID objectID, List<Variable> outputtedVariables)
        {

            Source dspfSource = dspfSourceReader.Read(objectID);

            var dspf = Create(dspfSource);

            var text = new List<string>(); 

            var repositoryPresenterName = $"{ dspfSource.ObjectID.Name.ToPublicModernName().ToLower() }Presenter";
            text.Add($"I{dspfSource.ObjectID.Name.ToPublicModernName()}Presenter {repositoryPresenterName};");

            //text.Add("public ReactivePropertySlim<bool> CloseWindow { get; } = new ReactivePropertySlim<bool>(false);");
            //text.Add("void SignOff()");
            //text.Add("{");
            //text.Add($"{Indent.Single}CloseWindow.Value = true;");
            //text.Add("}");

            text.Add("Action MethodAfterExfmt { get; set; }");
            text.Add("public void ExecuteAttentionCommand(string indicatorNumber)");
            text.Add("{");
            text.Add($"{Indent.Single}IndicatorsForCommandButtons.SetByCommandNumber(int.Parse(indicatorNumber));");
            text.Add($"{Indent.Single}MethodAfterExfmt.Invoke();");
            text.Add("}");
            text.Add("Action ReadRecordFormat { get; set; }");
            text.Add("public void ExecuteFunctionCommand(string indicatorNumber)");
            text.Add("{");
            text.Add($"{Indent.Single}IndicatorsForCommandButtons.SetByCommandNumber(int.Parse(indicatorNumber));");
            text.Add($"{Indent.Single}ReadRecordFormat.Invoke();");
            text.Add($"{Indent.Single}MethodAfterExfmt.Invoke();");
            text.Add("}");

            dspf.RecordFormatHeaderList.ForEach(recordFormatHeader =>
            {
                var recordFormatName = recordFormatHeader.PublicModernName;


                if (recordFormatHeader is SubFileControlRecordFormatHeader)
                {
                    var pnlName=  ((SubFileControlRecordFormatHeader)recordFormatHeader).SubFileRecordName ;
                    text.Add($"void Chain{pnlName}(int recordNumber, int indicatorNumber)");
                    text.Add("{");
                    text.Add($"{Indent.Single}var subFileItem = {repositoryPresenterName}.Chain(recordNumber);");
                    text.Add($"{Indent.Single}{pnlName} = subFileItem.value;");
                    text.Add($"{Indent.Single}if (indicatorNumber <= 24)");
                    text.Add($"{Indent.Single}{{");
                    text.Add($"{Indent.Couple}IndicatorsForCommandButtons.Set(indicatorNumber, subFileItem.notFound);");
                    text.Add($"{Indent.Single}}}");
                    text.Add($"{Indent.Single}else");
                    text.Add($"{Indent.Single}{{");
                    text.Add($"{Indent.Couple}Flags.Set(indicatorNumber, subFileItem.notFound);");
                    text.Add($"{Indent.Single}}}");
                    text.Add("}");

                }

                if(recordFormatHeader is SubFileRecordFormatHeader)
                {
                    text.Add($"void Write({recordFormatName} recordFormat)");
                    text.Add("{");
                    text.Add($"{Indent.Single}{repositoryPresenterName}.Write(recordFormat,Rrn);");
                    text.Add("}");

                }
                else
                {
                    text.Add($"void Exfmt({recordFormatName} recordFormat)");
                    text.Add("{");
                    text.Add($"{Indent.Single}var exfmtCallerMethod = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod();");
                    text.Add($"{Indent.Single}Action methodAfterExfmt = () => exfmtCallerMethod.Invoke(this, null);");
                    text.Add($"{Indent.Single}Exfmt(recordFormat, methodAfterExfmt);");
                    text.Add("}");

                    text.Add($"void Exfmt({recordFormatName} recordFormat, Action methodAfterExfmt)");
                    text.Add("{");
                    text.Add($"{Indent.Single}ReadRecordFormat = () => this.{recordFormatName} = {repositoryPresenterName}.Read{recordFormatName}();");
                    text.Add($"{Indent.Single}MethodAfterExfmt = methodAfterExfmt;");
                    text.Add($"{Indent.Single}{repositoryPresenterName}.Write(recordFormat);");
                    text.Add("}");

                    text.Add($"void Write({recordFormatName} recordFormat)");
                    text.Add("{");
                    text.Add($"{Indent.Single}{repositoryPresenterName}.Write(recordFormat);");
                    text.Add("}");

                }


                var fieldNames = VariableFactory.Of(recordFormatHeader.RecordFormatFields.ITypeDefinitions);

                text.Add($"{recordFormatName} {recordFormatName}");
                text.Add("{");
                text.Add($"{Indent.Single}get");
                text.Add($"{Indent.Single}{{");
                text.Add($"{Indent.Couple}var entity = new {recordFormatName}();");
                fieldNames.Select(f => f.Name).ToList().ForEach(fieldName => text.Add($"{Indent.Couple}entity.{fieldName} = {fieldName};"));
                text.Add($"{Indent.Couple}return entity;");
                text.Add($"{Indent.Single}}}");
                text.Add($"{Indent.Single}set");
                text.Add($"{Indent.Single}{{");
                fieldNames.Select(f => f.Name).ToList().ForEach(fieldName => text.Add($"{Indent.Couple}{fieldName} = value.{fieldName};"));
                text.Add($"{Indent.Single}}}");
                text.Add("}");

            });

            dspf.RecordFormatHeaderList.SelectMany(recordFormatHeader => VariableFactory.Of(recordFormatHeader.RecordFormatFields.ITypeDefinitions))
                .ToList().ForEach(v =>
                {
                    if (!outputtedVariables.Any(o => o.Name == v.Name))
                    {
                        text.Add(PropertyItem.Of(v).ToAutoImplementedPropertiesString());
                        outputtedVariables.Add(v);
                    }
                });

            return text;


        }

        public DisplayFileStructure Create(Source dspfSource)
        {
            var AttentionCommands = new List<AttentionCommand>();
            var FunctionCommands = new List<FunctionCommand> ();
            var RecordFormatHeaders = new List<RecordFormatHeader>();

            for (var originalLinesIndex = 0; originalLinesIndex < dspfSource.OriginalLines.Length; originalLinesIndex++)
            {
                IDisplayFileLine dfileLine = new DDSLine(dspfSource.OriginalLines[originalLinesIndex], originalLinesIndex);

                if (dfileLine.IsCommentLine) continue;

                if (dfileLine.IsRecordFormatHeaderLine)
                {
                    RecordFormatHeader recordFormatHeader;
                    if (dfileLine.IsSubFileControlRecord)
                    {
                        recordFormatHeader = new SubFileControlRecordFormatHeader(dfileLine.Name.ToPublicModernName(),dfileLine.SubFileRecordName.ToPublicModernName());
                    }
                    else
                    if(dfileLine.IsSubFileRecord)
                    {
                        recordFormatHeader = new SubFileRecordFormatHeader(dfileLine.Name.ToPublicModernName());
                    }
                    else
                    {
                        recordFormatHeader = new RecordFormatHeader(dfileLine.Name.ToPublicModernName());
                    }

                    if (dfileLine.IsContainsOverlayKeyword) recordFormatHeader.IsOverlay=true;
                    if (dfileLine.IsContainsProtectKeyword) recordFormatHeader.IsProtect = true;

                    RecordFormatHeaders.Add(recordFormatHeader);
                    continue;
                }

                if (dfileLine.IsField|| dfileLine.Position.Contains("+"))
                {
                    int line = 0;
                    int position = 0;
                    if (dfileLine.Position.Contains("+"))
                    {
                        IDisplayFileLine pre = new DDSLine(dspfSource.OriginalLines[originalLinesIndex - 1], originalLinesIndex - 1);
                        int preindex=1;
                        while (!int.TryParse(pre.Line,out line))
                        {
                            preindex--;
                            pre = new DDSLine(dspfSource.OriginalLines[originalLinesIndex - preindex], originalLinesIndex - preindex);
                        }

                        position = int.Parse(pre.Position) + (pre.IsConst ? pre.LengthOfConst : int.Parse(pre.Length)) + int.Parse(dfileLine.Position.Replace("+", ""));
                    }
                    else
                    {
                        line = int.Parse(dfileLine.Line);
                        position = int.Parse(dfileLine.Position);
                    }

                    IRecordFormatField recordFormatField;
                    if (dfileLine.HaveName)
                    {
                        IDataTypeDefinition typeDefinition = GetTypeDefinition(dspfSource.ObjectID.Library,dfileLine);
                        recordFormatField=new RecordFormatBindField(dfileLine,line,position, typeDefinition, !((IDisplayFileLine)dfileLine).IsBoth);
                    }
                    else
                    if(dfileLine.Keywords.StartsWith("'")&& dfileLine.Keywords.EndsWith("+"))
                    {
                        originalLinesIndex++;
                        IDisplayFileLine dfileLineNext = new DDSLine(dspfSource.OriginalLines[originalLinesIndex], originalLinesIndex);
                        recordFormatField = new RecordFormatConstField(dfileLine, line, position, dfileLine.Keywords[0..^1] + dfileLineNext.Keywords);
                    }
                    else
                    {
                        recordFormatField = new RecordFormatConstField(dfileLine, line, position, dfileLine.Keywords);
                    }

                    var recordFormatHeader= RecordFormatHeaders.Last();
                    recordFormatHeader.Add(recordFormatField);
                    continue;
                }

                if (dfileLine.HaveName)
                {
                    IDataTypeDefinition typeDefinition = GetTypeDefinition(dspfSource.ObjectID.Library, dfileLine);
                    RecordFormatHeaders.Last().Add(new RecordFormatVariableField(typeDefinition));
                    continue;
                }

                if (dfileLine.HaveKeywords)
                {
                    RecordFormatHeader recordFormatHeader;
                    List<AttentionCommand> attentionCommands;
                    List<FunctionCommand> functionCommands;
                    if (RecordFormatHeaders.Count == 0)
                    {
                        attentionCommands= AttentionCommands;
                        functionCommands = FunctionCommands;
                    }
                    else
                    {
                        recordFormatHeader = RecordFormatHeaders.Last();
                        attentionCommands = recordFormatHeader.AttentionCommands;
                        functionCommands = recordFormatHeader.FunctionCommands;
                        if (dfileLine.Keywords.StartsWith("COLOR("))
                        {
                            recordFormatHeader.RecordFormatFields.OutputFields.Last().Color = TextClipper.ClipParameter(dfileLine.Keywords, "COLOR");
                            continue;
                        }
                        if (dfileLine.IsContainsOverlayKeyword) recordFormatHeader.IsOverlay = true;
                        if (dfileLine.IsContainsProtectKeyword) recordFormatHeader.IsProtect = true;
                    }

                    Enumerable.Range(1, 24).Where(commandNumber => !attentionCommands.Exists(cmd=> cmd.Number == commandNumber) && dfileLine.IsContainsCAKeyword(commandNumber)).ToList().ForEach(commandNumber => 
                    {
                        AttentionCommand attentionCommand;
                        if (dfileLine.HasIndicator)
                        {
                            attentionCommand= new AttentionCommand(commandNumber,dfileLine.Indicator);
                        }
                        else
                        {
                            attentionCommand = new AttentionCommand(commandNumber);
                        }
                        attentionCommands.Add(attentionCommand);
                    });
                    Enumerable.Range(1, 24).Where(commandNumber => !functionCommands.Exists(cmd => cmd.Number == commandNumber) && dfileLine.IsContainsCFKeyword(commandNumber)).ToList().ForEach(commandNumber => 
                    {
                        FunctionCommand functionCommand;
                        if (dfileLine.HasIndicator)
                        {
                            functionCommand = new FunctionCommand(commandNumber, dfileLine.Indicator);
                        }
                        else
                        {
                            functionCommand = new FunctionCommand(commandNumber);
                        }

                        functionCommands.Add(functionCommand);
                    });

                    //0053      A                                      ROLLDOWN(07) ROLLUP(08)

                }

                
            }

            return new DisplayFileStructure(dspfSource, AttentionCommands, FunctionCommands, RecordFormatHeaders);

        }

        IDataTypeDefinition GetTypeDefinition(Library library,IDisplayFileLine dfileLine)
        {
            IDataTypeDefinition typeDefinition;
            if (dfileLine.IsREFFLDLine)
            {//REFFLD(CUST1   URIHFL)
                var Refflds = TextClipper.ClipParameter(dfileLine.Keywords, "REFFLD").CompressSpacesToSingleSpace().Split(' ');

                var referFileName = Refflds[1];
                var RefObjectID = ObjectIDFactory.CreateLibraryObjectName(library, referFileName);
                var referDisk = DiskFileStructureFactory.CreatePFStructure(RefObjectID);

                var referFieldName = Refflds[0];
                var referTypeDif = DiskFileStructureFactory.CreateTypeDefinition(referDisk.RecordFormatFieldBy(referFieldName), referDisk.RefObjectID);

                //typeDefinition = referTypeDif.Of(dfileLine.Name);
                typeDefinition = DataTypeDefinition.Of(dfileLine.Name, referTypeDif.Length, referTypeDif.InternalDataType, referTypeDif.DecimalPositions);

            }
            else
            {
                typeDefinition = dfileLine.ToTypeDefinition;
            }
            return typeDefinition;
        }
    }
}
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    class DisplayFileXamlCsFactory
    {
        internal static ClassStructure Create(bool isCLnotRPG,Library mainLibrary, DisplayFileStructure dspf,
            ObjectID serviceObjectID,
            List<Variable> parametersOfService,
            bool IsCallingInService,
            IEnumerable<ObjectID> fileDifinitionObjectIDs, 
            IEnumerable<string> externalDiskFileNames,
            IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines
            )
        {
            var dspfObjectID = dspf.OriginalSource.ObjectID;

            var DisplayFileXamlCs = new ClassStructure(
            NamespaceItemFactory.DeltaOf(dspfObjectID),true,
            $"{dspfObjectID.Name.ToPublicModernName()}ViewModel",
            "PathResolver.PresentatioFileFolderPath(dspfObjectID)",
            $"{dspfObjectID.Name.ToPublicModernName()}View",
            "xaml.cs"
            );

            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.PrismMvvm);
            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.PrismServicesDialogs);

            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.ReactiveBindings);
            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.ReactiveBindingsLinq);

            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.System);
            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.SystemWindowsControls);
            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaAS400Adapters);
            //DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaAS400Misc);
            if (PrinterFileDescriptionLines.Count()>0)
            {
                DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaAS400DDSsPrinters);
            }

            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaOf(dspfObjectID));

            fileDifinitionObjectIDs.ToList().ForEach(repo=> DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaOf(repo)));

            DisplayFileXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaOf(serviceObjectID));

            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"public partial class {dspfObjectID.Name.ToPublicModernName()}View : UserControl");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare("{");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}public {dspfObjectID.Name.ToPublicModernName()}View()");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}{{");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Couple}InitializeComponent();");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}}}");
            DisplayFileXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare("}");

            DisplayFileXamlCs.AddInterface("BindableBase");
            DisplayFileXamlCs.AddInterface("IDialogAware");
            DisplayFileXamlCs.AddInterface($"I{dspfObjectID.Name.ToPublicModernName()}Presenter");

            DisplayFileXamlCs.AddContentLine($"{serviceObjectID.Name.ToPublicModernName()}Service Model {{ get; }}");

            //DisplayFileXamlCs.AddContentLine($"Action<int> I{dspfObjectID.Name.ToPublicModernName()}Presenter.ExfmtHandler {{ get; set; }}");

            var reponames = externalDiskFileNames.ToList();

            //var cp = new List<string>();
            //cp.AddRange(reponames.Select(r => $"I{r}Repository {r.ToCamelCase()}Repository"));
            //if(IsCallingInService) cp.Add("IPgmCaller pgmCaller");
            var constructerParameters = $"I{mainLibrary.Partition.Name.ToPublicModernName()}{mainLibrary.Name.ToPublicModernName()}DependencyInjector dependencyInjector";// string.Join(',',cp.ToArray());

            var sp = new List<string>();
            //if (isCLnotRPG) 
            //{
                sp.Add("dependencyInjector");
                sp.Add("this");
            //}
            //else
            //{
            //    sp.AddRange(reponames.Select(r => $"dependencyInjector.{r}Repository"));
            //    sp.AddRange(PrinterFileDescriptionLines.Select(r => "new Printer()"));
            //    sp.Add("this");
            //    if (IsCallingInService) sp.Add("dependencyInjector.PgmCaller");
            //}
            var serviceConstructerParameters = string.Join(',', sp.ToArray());

            DisplayFileXamlCs.AddContentLine($"internal {dspfObjectID.Name.ToPublicModernName()}ViewModel({constructerParameters})");
            DisplayFileXamlCs.AddContentLine("{");
            DisplayFileXamlCs.AddContentLine($"{Indent.Single}Model = new {serviceObjectID.Name.ToPublicModernName()}Service({serviceConstructerParameters});");
            DisplayFileXamlCs.AddContentLine($"{Indent.Single}AttentionCommand.Subscribe(Model.ExecuteAttentionCommand);");
            DisplayFileXamlCs.AddContentLine($"{Indent.Single}FunctionCommand.Subscribe(Model.ExecuteFunctionCommand);");

            dspf.AllIndicatorsInAttentionCommand().ToList().ForEach(indicator =>
            {
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}AttentionCommandIn{indicator.ValueString} = Model.ReactiveIn{indicator.NumberD2}{(indicator.IsNegation ? ".Select(x =>!x)":string.Empty)}.ToReactiveCommand<string>();");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}AttentionCommandIn{indicator.ValueString}.Subscribe(Model.ExecuteAttentionCommand);");
            });

            dspf.AllIndicatorsInFunctonCommand().ToList().ForEach(indicator =>
            {
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}FunctionCommandIn{indicator.ValueString} = Model.ReactiveIn{indicator.NumberD2}{(indicator.IsNegation ? ".Select(x =>!x)" : string.Empty)}.ToReactiveCommand<string>();");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}FunctionCommandIn{indicator.ValueString}.Subscribe(Model.ExecuteAttentionCommand);");
            });

            DisplayFileXamlCs.AddContentLine($"{Indent.Single}Model.LR.Subscribe(CloseDialog);");

            dspf.RecordFormatHeaderList.ForEach(r =>
            {
                var curRecordFormatName = r.PublicModernName;
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}{curRecordFormatName.ToCamelCase()}ViewModel = {curRecordFormatName}ViewModel.Of(Model);");
            });

            DisplayFileXamlCs.AddContentLine("}");

            DisplayFileXamlCs.AddContentLine("void IDialogAware.OnDialogOpened(IDialogParameters parameters)");
            DisplayFileXamlCs.AddContentLine("{");
            if(parametersOfService.Count>0) {
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}var objectParmeters = new object[{parametersOfService.Count}];");
                for(var i=0;i< parametersOfService.Count; i++)
                {
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}objectParmeters[{i}] = parameters.GetValue<{parametersOfService[i].TypeSpelling}>(\"p{i}\");");
                }
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}Model.SetParameters(objectParmeters);");
            }
            DisplayFileXamlCs.AddContentLine($"{Indent.Single}Model.Main();");
            DisplayFileXamlCs.AddContentLine("}");

            DisplayFileXamlCs.AddContentLine("IDialogResult DialogResult()");
            DisplayFileXamlCs.AddContentLine("{");

            DisplayFileXamlCs.AddContentLine($"{Indent.Single}IDialogParameters parameters = new DialogParameters();");

            if (parametersOfService.Count > 0)
            {
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}var retunedParameters = Model.GetParameters();");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}for (var i = 0; i < retunedParameters.Length; i++)");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}{{");
                DisplayFileXamlCs.AddContentLine($"{Indent.Couple}parameters.Add($\"r{{i}}\", retunedParameters[i]);");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}}}");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}return new DialogResult(ButtonResult.OK, parameters);");
            }
            else
            {
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}return new DialogResult(ButtonResult.OK);");
            }

            DisplayFileXamlCs.AddContentLine("}");

            DisplayFileXamlCs.AddContentLine("string IDialogAware.Title => throw new NotImplementedException();");
            DisplayFileXamlCs.AddContentLine("bool IDialogAware.CanCloseDialog() { return true; }");
            DisplayFileXamlCs.AddContentLine("public event Action<IDialogResult> RequestClose;");

            DisplayFileXamlCs.AddContentLine("void CloseDialog(bool closeDemand)");
            DisplayFileXamlCs.AddContentLine("{");
            DisplayFileXamlCs.AddContentLine($"{Indent.Single}if (closeDemand) this.RequestClose?.Invoke(DialogResult());");
            DisplayFileXamlCs.AddContentLine("}");

            DisplayFileXamlCs.AddContentLine("void IDialogAware.OnDialogClosed(){}");

            DisplayFileXamlCs.AddContentLine("public ReactiveCommand<string> AttentionCommand { get; } = new ReactiveCommand<string>();");

            dspf.AllIndicatorsInAttentionCommand().ToList().ForEach(indicator =>
            {
                DisplayFileXamlCs.AddContentLine($"public ReactiveCommand<string> AttentionCommandIn{indicator.ValueString} {{ get; }} = new ReactiveCommand<string>();");
            });

            DisplayFileXamlCs.AddContentLine("public ReactiveCommand<string> FunctionCommand { get; } = new ReactiveCommand<string>();");
            dspf.AllIndicatorsInFunctonCommand().ToList().ForEach(indicator =>
            {
                DisplayFileXamlCs.AddContentLine($"public ReactiveCommand<string> FunctionCommandIn{indicator.ValueString} {{ get; }} = new ReactiveCommand<string>();");
            });

            var recordFormatNames = dspf.RecordFormatHeaderList.Where(x => !(x is SubFileRecordFormatHeader)).Select(r=>r.PublicModernName).ToList();
            dspf.RecordFormatHeaderList.ForEach(r=>
            {
                var curRecordFormatName= r.PublicModernName;
                DisplayFileXamlCs.AddContentLine($"{curRecordFormatName}ViewModel {curRecordFormatName.ToCamelCase()}ViewModel;");
                DisplayFileXamlCs.AddContentLine($"public {curRecordFormatName}ViewModel {curRecordFormatName}ViewModel");
                DisplayFileXamlCs.AddContentLine("{");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}get {{ return {curRecordFormatName.ToCamelCase()}ViewModel; }}");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}set {{ SetProperty(ref {curRecordFormatName.ToCamelCase()}ViewModel  , value); }}");
                DisplayFileXamlCs.AddContentLine("}");

                DisplayFileXamlCs.AddContentLine($"{curRecordFormatName} I{dspfObjectID.Name.ToPublicModernName()}Presenter.Read{curRecordFormatName}()");
                DisplayFileXamlCs.AddContentLine("{");
                DisplayFileXamlCs.AddContentLine($"{Indent.Single}return {curRecordFormatName}ViewModel.Read();");
                DisplayFileXamlCs.AddContentLine("}");

                if(r is SubFileControlRecordFormatHeader)
                {
                    DisplayFileXamlCs.AddContentLine($"(bool notFound, {((SubFileControlRecordFormatHeader)r).SubFileRecordName} value) I{dspfObjectID.Name.ToPublicModernName()}Presenter.Chain(int recordNumber)");
                    DisplayFileXamlCs.AddContentLine("{");
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}var subfileRecordViewModel = {curRecordFormatName}ViewModel.Chain(recordNumber);");
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}return (subfileRecordViewModel.Rrn == 0, subfileRecordViewModel.Read());");
                    DisplayFileXamlCs.AddContentLine("}");
                }

                if (r is SubFileRecordFormatHeader)
                {
                    DisplayFileXamlCs.AddContentLine($"void I{dspfObjectID.Name.ToPublicModernName()}Presenter.Write({curRecordFormatName} recordFormat,int recordNumber)");
                    DisplayFileXamlCs.AddContentLine("{");

                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{dspf.SubFileControlRecordForamtHeader.PublicModernName}ViewModel.Write(recordFormat,recordNumber);");
                    DisplayFileXamlCs.AddContentLine("}");

                }
                else
                {
                    DisplayFileXamlCs.AddContentLine($"void I{dspfObjectID.Name.ToPublicModernName()}Presenter.Write({curRecordFormatName} recordFormat)");
                    DisplayFileXamlCs.AddContentLine("{");

                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{curRecordFormatName}ViewModel.IsEnabled = true;");
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}if ({curRecordFormatName}ViewModel.IsProtect)");
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{{");
                    recordFormatNames.Where(recordFormatName => recordFormatName != curRecordFormatName).ToList().ForEach(recordFormatName => {
                        DisplayFileXamlCs.AddContentLine($"{Indent.Couple}{recordFormatName}ViewModel.IsEnabled = false;");
                    });
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}}}");

                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{curRecordFormatName}ViewModel.IsVisible = true;");

                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}if(!{curRecordFormatName}ViewModel.IsOverlay)");
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{{");
                    recordFormatNames.Where(recordFormatName => recordFormatName != curRecordFormatName).ToList().ForEach(recordFormatName => {
                        DisplayFileXamlCs.AddContentLine($"{Indent.Couple}{recordFormatName}ViewModel.IsVisible = false;");
                    });
                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}}}");

                    DisplayFileXamlCs.AddContentLine($"{Indent.Single}{curRecordFormatName}ViewModel.Write(recordFormat);");
                    DisplayFileXamlCs.AddContentLine("}");

                }
            });

            DisplayFileXamlCs.AddAppendLinesOfEndOfFile(CommentFactory.OriginalLineCommentLines(dspf.OriginalSource.OriginalLines));

            return DisplayFileXamlCs;
        }
    }
}


 

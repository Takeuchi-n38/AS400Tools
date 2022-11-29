using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    class RecordFormatXamlCsFactory
    {
        internal static ClassStructure Create(PathResolver PathResolver, DisplayFileStructure dspf, ObjectID serviceObjectID, RecordFormatHeader RecordFormatHeader)
        {
            var dspfObjectID = dspf.OriginalSource.ObjectID;

            var RecordFormatXamlCs = new ClassStructure(
            NamespaceItemFactory.DeltaOf(dspfObjectID),true,
            $"{RecordFormatHeader.PublicModernName}ViewModel",
            "",
            $"{RecordFormatHeader.PublicModernName}View",
            "xaml.cs"
            );

            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaAS400Workstations);

            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.PrismMvvm);
            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.ReactiveBindings);
            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.SystemWindowsControls);
            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.System);
            RecordFormatXamlCs.AddUsingNamespace(NamespaceItemFactory.DeltaOf(serviceObjectID));

            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"public partial class {RecordFormatHeader.PublicModernName}View : UserControl");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare("{");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}public {RecordFormatHeader.PublicModernName}View()");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}{{");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Couple}InitializeComponent();");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare($"{Indent.Single}}}");
            RecordFormatXamlCs.AddSingleIndentContentLineAfterNamespaceDeclare("}");

            if (RecordFormatHeader is SubFileControlRecordFormatHeader)
            {
                var nm= ((SubFileControlRecordFormatHeader)RecordFormatHeader).SubFileRecordName;
                RecordFormatXamlCs.AddInterface($"SubFileControlViewModel<{nm}ViewModel,{nm}>");
            }
            else
            if (RecordFormatHeader is SubFileRecordFormatHeader)
            {
                RecordFormatXamlCs.AddInterface("BindableBase");
                //RecordFormatXamlCs.AddInterface("IIsVisible");
                RecordFormatXamlCs.AddInterface("IIsEnabled");
                RecordFormatXamlCs.AddInterface($"ISubFileRecordViewModel<{RecordFormatHeader.PublicModernName}ViewModel,{RecordFormatHeader.PublicModernName}>");
            }
            else
            {
                RecordFormatXamlCs.AddInterface("BindableBase");
                RecordFormatXamlCs.AddInterface("IIsVisible");
                RecordFormatXamlCs.AddInterface("IIsEnabled");
            }
            RecordFormatXamlCs.AddInterface("IIsProtect");
            RecordFormatXamlCs.AddInterface("IIsOverlay");

            RecordFormatXamlCs.AddContentLine($"{serviceObjectID.Name.ToPublicModernName()}Service Model;");
            RecordFormatXamlCs.AddContentLine($"internal static {RecordFormatHeader.PublicModernName}ViewModel Of({serviceObjectID.Name.ToPublicModernName()}Service model)");
            RecordFormatXamlCs.AddContentLine("{");
            RecordFormatXamlCs.AddContentLine($"{Indent.Single}var vm = new {RecordFormatHeader.PublicModernName}ViewModel();");
            RecordFormatXamlCs.AddContentLine($"{Indent.Single}vm.Model = model;");
            RecordFormatXamlCs.AddContentLine($"{Indent.Single}return vm;");
            RecordFormatXamlCs.AddContentLine("}");

            var indicatorValueStrings = RecordFormatHeader.RecordFormatFields.OutputFields.Where(f => f.HasIndicator).Select(f => f.IndicatorValueString).Distinct();

            //if (indicatorValueStrings.Count() > 0)
            //{
            //    RecordFormatXamlCs.AddContentLine($"public {RecordFormatHeader.PublicModernName}ViewModel()");
            //    RecordFormatXamlCs.AddContentLine("{");
            //    indicatorValueStrings.ToList().ForEach(indicatorValueString =>
            //    {
            //        RecordFormatXamlCs.AddContentLine($"{Indent.Single}In{indicatorValueString} = new ReactivePropertySlim<bool>();");
            //    });
            //    RecordFormatXamlCs.AddContentLine("}");
            //}

            indicatorValueStrings.ToList().ForEach(indicatorValueString=>
            {
                RecordFormatXamlCs.AddContentLine($"public ReactivePropertySlim<bool> In{indicatorValueString} {{ get; }} = new ReactivePropertySlim<bool>();");
            });

            if (RecordFormatHeader is SubFileControlRecordFormatHeader)
            {
            }
            else
            if (RecordFormatHeader is SubFileRecordFormatHeader)
            {

                RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.OfBool, "IsVisible")));
                RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.OfBool, "IsEnabled")));

                //RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.Of("Visibility"), "Visibility")));

                RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.OfInt(), "Rrn")));

                var RecordFormatFieldsVariables = VariableFactory.Of(RecordFormatHeader.RecordFormatFields.ITypeDefinitions).ToList();

                RecordFormatXamlCs.AddContentLine($"public void Clear()");
                RecordFormatXamlCs.AddContentLine("{");
                RecordFormatFieldsVariables.ForEach(recordFormatField =>
                {
                    RecordFormatXamlCs.AddContentLine($"{Indent.Single}{recordFormatField.Name}.Value = {recordFormatField.TypeInitialValueSpelling};");
                });
                RecordFormatXamlCs.AddContentLine("}");

                RecordFormatXamlCs.AddContentLine($"public bool IsModified({RecordFormatHeader.PublicModernName} original)");
                RecordFormatXamlCs.AddContentLine("{");
                RecordFormatFieldsVariables.ForEach(recordFormatField =>
                {
                    RecordFormatXamlCs.AddContentLine($"{Indent.Single}if({recordFormatField.Name}.Value != original.{recordFormatField.Name}) return true;");
                });
                RecordFormatXamlCs.AddContentLine($"{Indent.Single}return false;");
                RecordFormatXamlCs.AddContentLine("}");


            }
            else
            {
                RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.OfBool, "IsVisible")));
                RecordFormatXamlCs.AddContentLines(PropertyForBindableBaseContentsFactory.Create(Variable.Of(TypeOfVariable.OfBool, "IsEnabled")));
            }

            RecordFormatXamlCs.AddContentLine($"public bool IsProtect => {(RecordFormatHeader.IsProtect?"true":"false")};");

            RecordFormatXamlCs.AddContentLine($"public bool IsOverlay => {(RecordFormatHeader.IsOverlay ? "true" : "false")};");

            var variables = VariableFactory.Of(RecordFormatHeader.RecordFormatFields.ITypeDefinitions);

            variables.ToList().ForEach(v =>RecordFormatXamlCs.AddContentLine($"public ReactiveProperty<{v.TypeSpelling}> {v.Name} {{ get; }} = new ReactiveProperty<{v.TypeSpelling}>();"));

            var names = variables.Select(v=>v.Name).ToList();

            RecordFormatXamlCs.AddContentLine($"public void Write({RecordFormatHeader.PublicModernName} recordFormat)");
            RecordFormatXamlCs.AddContentLine("{");
            names.ForEach(name => RecordFormatXamlCs.AddContentLine($"{Indent.Single}{name}.Value = recordFormat.{name};"));

            if (RecordFormatHeader is SubFileControlRecordFormatHeader)
            {
                RecordFormatXamlCs.AddContentLine($"{Indent.Single}if (Model.ReactiveIn30.Value) SubFileDisplay();");
                RecordFormatXamlCs.AddContentLine($"{Indent.Single}if (Model.ReactiveIn30.Value) SubFileDisplayControl();");
                RecordFormatXamlCs.AddContentLine($"{Indent.Single}if (Model.ReactiveIn31.Value) SubFileClear();");
            }

            indicatorValueStrings.ToList().ForEach(i =>
            {
                //InN81.Value = Model.ReactiveIn81.Value;
                RecordFormatXamlCs.AddContentLine($"{Indent.Single}In{i}.Value = Model.ReactiveIn{i.Substring((i.StartsWith("N") ? 1 :0))}.Value=={(i.StartsWith("N")?"false":"true")};");
            });

            RecordFormatXamlCs.AddContentLine("}");

            RecordFormatXamlCs.AddContentLine($"public {RecordFormatHeader.PublicModernName} Read()");
            RecordFormatXamlCs.AddContentLine("{");
            RecordFormatXamlCs.AddContentLine($"{Indent.Single}var recordFormat = new {RecordFormatHeader.PublicModernName}();");
            names.ForEach(name => RecordFormatXamlCs.AddContentLine($"{Indent.Single}recordFormat.{name} = {name}.Value;"));
            RecordFormatXamlCs.AddContentLine($"{Indent.Single}return recordFormat;");
            RecordFormatXamlCs.AddContentLine("}");

            RecordFormatXamlCs.AddAppendLinesOfEndOfFile(CommentFactory.OriginalLineCommentLines(dspf.OriginalSource.OriginalLines));

            return RecordFormatXamlCs;
        }
    }
}


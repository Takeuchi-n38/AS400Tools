using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    class DisplayFileXamlFactory
    {
        internal static XamlStructure Create(PathResolver PathResolver,DisplayFileStructure dspf)
        {
            var dspfObjectID = dspf.OriginalSource.ObjectID;

            var XamlStructure = new XamlStructure(
                NamespaceItemFactory.DeltaOf(dspfObjectID),
                $"{dspfObjectID.Name.ToPublicModernName()}View",
                "",
                true
                );

            XamlStructure.AddContentLine("<prism:Dialog.WindowStyle>");
            XamlStructure.AddContentLine($"{Indent.Single}<Style TargetType=\"Window\" BasedOn=\"{{StaticResource DefaultDialogWindowStyle}}\">");
            XamlStructure.AddContentLine($"{Indent.Single}</Style>");
            XamlStructure.AddContentLine($"</prism:Dialog.WindowStyle>");

            XamlStructure.AddContentLine("<UserControl.InputBindings>");
            XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Ctrl\" Key=\"LeftCtrl\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"0\" />");
            XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Ctrl\" Key=\"RightCtrl\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"0\" />");
         
            dspf.AllAttentionCommands().ToList().ForEach(cmd=>
            {
                if (cmd.HasIndicator)
                {
                    XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Key=\"F{cmd.Number}\" Command=\"{{Binding AttentionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                    if (cmd.Number >= 13) XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Shift\" Key=\"F{cmd.Number - 12}\" Command=\"{{Binding AttentionCommandIn{cmd.Indicator.ValueString}}}\" CommandParameter=\"{cmd.Number}\" />");
                }
                else
                {
                    XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Key=\"F{cmd.Number}\" Command=\"{{Binding AttentionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                    if (cmd.Number >= 13) XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Shift\" Key=\"F{cmd.Number - 12}\" Command=\"{{Binding AttentionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                }
            });

            dspf.AllFunctonCommands().ToList().ForEach(cmd =>
            {
                if (cmd.HasIndicator)
                {
                    XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Key=\"F{cmd.Number}\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                    if (cmd.Number >= 13) XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Shift\" Key=\"F{cmd.Number - 12}\" Command=\"{{Binding FunctionCommandIn{cmd.Indicator.ValueString}}}\" CommandParameter=\"{cmd.Number}\" />");
                }
                else
                {
                    XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Key=\"F{cmd.Number}\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                    if (cmd.Number >= 13) XamlStructure.AddContentLine($"{Indent.Single}<KeyBinding Modifiers=\"Shift\" Key=\"F{cmd.Number - 12}\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"{cmd.Number}\" />");
                }
            });

            XamlStructure.AddContentLine($"{Indent.Single}<!--<KeyBinding Key=\"PageDown\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"7\" />-->");
            XamlStructure.AddContentLine($"{Indent.Single}<!--<KeyBinding Key=\"PageUp\" Command=\"{{Binding FunctionCommand}}\" CommandParameter=\"8\" />-->");

            XamlStructure.AddContentLine($"</UserControl.InputBindings>");

            XamlStructure.AddContentLine($"<Viewbox>");

            XamlStructure.AddContentLine($"{Indent.Single}<Grid Style=\"{{StaticResource GridOfDisplayFile}}\">");

            XamlStructure.AddContentLine($"{Indent.Couple}<Grid.ColumnDefinitions>");
            Enumerable.Range(0, 80).ToList().ForEach(i => XamlStructure.AddContentLine($"{Indent.Triple}<ColumnDefinition Width=\"*\"/>"));
            XamlStructure.AddContentLine($"{Indent.Couple}</Grid.ColumnDefinitions>");
            XamlStructure.AddContentLine($"{Indent.Couple}<Grid.RowDefinitions>");
            Enumerable.Range(0, 24).ToList().ForEach(i => XamlStructure.AddContentLine($"{Indent.Triple}<RowDefinition Height=\"*\"/>"));
            XamlStructure.AddContentLine($"{Indent.Couple}</Grid.RowDefinitions>");

            dspf.RecordFormatHeaderList.ForEach(RecordFormatHeader => {
                XamlStructure.AddContentLine($"{Indent.Couple}<local:{RecordFormatHeader.PublicModernName}View Grid.Row=\"{RecordFormatHeader.RecordFormatFields.FirstLine - 1}\" Grid.RowSpan=\"{RecordFormatHeader.LineSpan}\" Grid.Column=\"0\" Grid.ColumnSpan=\"80\" DataContext=\"{{Binding {RecordFormatHeader.PublicModernName}ViewModel}}\" />");
            });

            XamlStructure.AddContentLine($"{Indent.Single}</Grid>");
            XamlStructure.AddContentLine($"</Viewbox>");

            return XamlStructure;
        }

    }
}

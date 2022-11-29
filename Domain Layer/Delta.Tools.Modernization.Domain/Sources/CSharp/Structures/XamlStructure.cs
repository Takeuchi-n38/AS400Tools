using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Delta.Tools.CSharp.Structures
{
    public class XamlStructure : ISourceFile
    {
        readonly NamespaceItem Namespace;

        public readonly string Name;

        readonly string FolderPath;

        readonly bool IsDialog;

        public XamlStructure(NamespaceItem Namespace, string name, string folderPath, bool IsDialog)
        {
            this.Namespace = Namespace;
            Name = name;
            FolderPath = folderPath;
            this.IsDialog = IsDialog;
        }

        public int DesignWidth { private get; set; } = 0;
        public int DesignHeight { private get; set; } = 0;

        List<string> ContentLines = new List<string>();

        public void AddContentLine(string line)
        {
            ContentLines.Add(line);
        }

        string ISourceFile.ToSourceContents()
        {
            var sourceContents = new StringBuilder();
            sourceContents.AppendLine($"<UserControl x:Class=\"{Namespace.Name}.{Name}\"");
            sourceContents.AppendLine($"xmlns:local=\"clr-namespace:{Namespace.Name}\"");
            sourceContents.AppendLine("xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            sourceContents.AppendLine("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            sourceContents.AppendLine("xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"");
            sourceContents.AppendLine("xmlns:sys=\"clr-namespace:System;assembly=mscorlib\"");
            sourceContents.AppendLine("xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"");
            sourceContents.AppendLine("xmlns:i=\"http://schemas.microsoft.com/xaml/behaviors\"");
            if (IsDialog)
            {
                sourceContents.AppendLine("xmlns:prism=\"http://prismlibrary.com/\" prism:ViewModelLocator.AutoWireViewModel=\"True\" prism:Dialog.WindowStartupLocation=\"CenterOwner\"");
                sourceContents.AppendLine("win:InputBindingBehavior.PropagateInputBindingsToWindow=\"True\"");
                //sourceContents.AppendLine("Width=\"960px\" Height=\"576px\"");
            }

            sourceContents.AppendLine("xmlns:win=\"clr-namespace:Delta.Guis.Wpf;assembly=Delta.Guis.Wpf\"");

            sourceContents.AppendLine("xmlns:wrkstn=\"clr-namespace:Delta.AS400.Workstations;assembly=Delta.AS400.Workstations.PrismCore\"");
            sourceContents.AppendLine("xmlns:both=\"clr-namespace:Delta.AS400.Workstations.Fields.Boths;assembly=Delta.AS400.Workstations.PrismCore\"");
            sourceContents.AppendLine("mc:Ignorable=\"d\"");
            if (DesignWidth != 0) sourceContents.AppendLine($"d:DesignWidth=\"{DesignWidth}\"");
            if (DesignHeight != 0) sourceContents.AppendLine($"d:DesignHeight=\"{DesignHeight}\"");

            sourceContents.AppendLine("Background = \"Black\"");
            if (!IsDialog)
            {
                sourceContents.AppendLine("Visibility=\"{Binding IsVisible, Converter={StaticResource BooleanToVisibilityConverter}}\"");
            }

            //sourceContents.AppendLine("xmlns:win=\"clr -namespace:Delta.Utilities.WindowsDesktop;assembly=Delta.Utilities.WindowsDesktop\"");
            //sourceContents.AppendLine("win:CloseWindowAttachedBehavior.Close=\"{ Binding CloseWindow.Value}\"");

            /*
                     xmlns:sys="clr-namespace:System;assembly=mscorlib"
                     Background="Black" Height="576px" Width="960px"
                     xmlns:prism="http://prismlibrary.com/"  prism:ViewModelLocator.AutoWireViewModel="True"
                     xmlns:misc="clr-namespace:Delta.AS400.Misc" misc:InputBindingBehavior.PropagateInputBindingsToWindow="True"
                    */
            sourceContents.AppendLine(">");

            ContentLines.ForEach(contentLine =>
            {
                sourceContents.AppendLine($"{Indent.Single}{contentLine}");
            });

            sourceContents.AppendLine("</UserControl>");

            return sourceContents.ToString();
        }

    }
}

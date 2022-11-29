using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Delta.Tools.CSharp.Structures
{
    public class CsStructure : ISourceFile
    {
        readonly NamespaceItem Namespace;

        readonly bool IsPartial;

        readonly string StructureType;

        readonly string Name;

        public readonly string FolderPath;

        public readonly string FileName;

        readonly string Extension;

        public string ExtendFileName => $"{FileName}.{Extension}";

        protected CsStructure(NamespaceItem namespaceItem, bool IsPartial, string structureType, string name, string folderPath, string fileName, string extension)
        {
            Namespace = namespaceItem;
            this.IsPartial = IsPartial;
            StructureType = structureType;
            Name = name;
            FolderPath = folderPath;
            FileName = fileName;
            Extension = extension;
        }

        protected List<NamespaceItem> Usings = new List<NamespaceItem>();
        public void AddUsingNamespace(NamespaceItem aNamespace)
        {
            Usings.Add(aNamespace);
        }

        // : BindableBase, IIsVisible, IIsEnabled
        protected List<string> Interfaces = new List<string>();
        public void AddInterface(string aInterface)
        {
            Interfaces.Add(aInterface);
        }

        List<string> SingleIndentContentLinesAfterNamespaceDeclare = new List<string>();

        public void AddSingleIndentContentLineAfterNamespaceDeclare(string line)
        {
            SingleIndentContentLinesAfterNamespaceDeclare.Add(line);
        }

        List<string> ContentLines = new List<string>();

        public void AddBlankLine()
        {
            ContentLines.Add(string.Empty);
        }

        public void AddContentLine(string line)
        {
            ContentLines.Add(line);
        }

        public void AddContentLines(IEnumerable<string> lines)
        {
            ContentLines.AddRange(lines);
        }

        List<string> AppendLinesOfEndOfFile = new List<string>();

        public void AddAppendLinesOfEndOfFile(IEnumerable<string> lines)
        {
            AppendLinesOfEndOfFile.AddRange(lines);
        }

        string ISourceFile.ToSourceContents()
        {
            var souceContents = new StringBuilder();

            Usings.ForEach(ns => souceContents.AppendLine(ns.ToUsingLine));

            souceContents.AppendLine();

            souceContents.AppendLine(Namespace.ToNamespaceLine);
            souceContents.AppendLine("{");

            SingleIndentContentLinesAfterNamespaceDeclare.ForEach(line => souceContents.AppendLine($"{Indent.Single}{line}"));

            souceContents.Append($"{Indent.Single}public{(IsPartial ? " partial " : " ")}{StructureType} {Name}");

            if (Interfaces.Count > 0)
            {
                souceContents.Append($" : {string.Join(", ", Interfaces.ToArray())}");
            }

            souceContents.AppendLine();
            souceContents.AppendLine($"{Indent.Single}{{");

            ContentLines.ForEach(line => souceContents.AppendLine($"{Indent.Couple}{line}"));

            souceContents.AppendLine($"{Indent.Single}}}");

            souceContents.AppendLine("}");

            AppendLinesOfEndOfFile.ForEach(line => souceContents.AppendLine(line));

            return souceContents.ToString();
        }

    }
}

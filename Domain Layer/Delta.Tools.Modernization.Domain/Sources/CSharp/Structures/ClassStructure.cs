using Delta.Modernization.Statements.Items.Namespaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Structures
{
    public class ClassStructure : CsStructure
    {
        public ClassStructure(NamespaceItem Namespace, bool IsPartial, string name, string folderPath, string fileName, string extension) : base(Namespace, IsPartial, "class", name, folderPath, fileName, extension)
        {
        }

        public ClassStructure(NamespaceItem Namespace, string name, string folderPath) : this(Namespace, false, name, folderPath, name, "cs")
        {
        }
    }
}

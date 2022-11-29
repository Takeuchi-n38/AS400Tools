using Delta.Modernization.Statements.Items.Namespaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Structures
{
    public class InterfaceStructure : CsStructure
    {

        public InterfaceStructure(NamespaceItem Namespace, string name, string folderPath) : base(Namespace, false, "interface", name, folderPath, name, "cs")
        {

        }

    }
}

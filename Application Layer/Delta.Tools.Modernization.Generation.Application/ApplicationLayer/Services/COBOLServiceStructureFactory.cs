using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services;
using Delta.Tools.AS400.Sources;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Generation.ApplicationLayer.Services
{
    internal class COBOLServiceStructureFactory
    {
        internal ClassStructure Create(Library mainLibrary, Source source)
        {

            var SourceObjectIDPublicModernName = source.ObjectID.Name.ToPublicModernName();

            var serviceClass = new ClassStructure(
            NamespaceItemFactory.DeltaOf(source.ObjectID), true,
            $"{SourceObjectIDPublicModernName}Service",
            "",
            $"{SourceObjectIDPublicModernName}Service",
            "gen.cs"
            );

            ServiceStructureFactory.UsingNamespaces().ToList().ForEach(namespaceItem => serviceClass.AddUsingNamespace(namespaceItem));

            serviceClass.AddContentLine($"I{mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}DependencyInjector DependencyInjector;");

            serviceClass.AddContentLines(
                ConstructorContents(mainLibrary, source.ObjectID));


            serviceClass.AddContentLines(MainMethodContents());

            serviceClass.AddAppendLinesOfEndOfFile(CommentFactory.OriginalLineCommentLines(source.OriginalLines));

            return serviceClass;
        }

        internal static COBOLServiceStructureFactory Instance = new COBOLServiceStructureFactory();

        IEnumerable<string> ConstructorContents(Library mainLibrary, ObjectID sourceObjectID)
        {

            var constructorContents = new List<string>();
            constructorContents.Add($"public {sourceObjectID.Name.ToPublicModernName() + "Service"}(");
            constructorContents.Add(")");
            constructorContents.Add("{");
            constructorContents.Add("}");

            return constructorContents;
        }

        IEnumerable<string> MainMethodContents()
        {
            var mainMethodContents = new List<string>();

            mainMethodContents.Add("public void Main()");
            mainMethodContents.Add("{");
            mainMethodContents.Add("}");

            return mainMethodContents;
        }
    }
}

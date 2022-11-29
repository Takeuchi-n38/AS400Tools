using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using System.Linq;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    class ViewModelDTOFactory
    {
        internal static ClassStructure Create(PathResolver PathResolver, ObjectID objectIDofDSPF, RecordFormatHeader recordFormatHeader)
        {
            var ViewModelDTO = new ClassStructure(
                NamespaceItemFactory.DeltaOf(objectIDofDSPF),
                recordFormatHeader.PublicModernName,
                ""
                );

            ViewModelDTO.AddUsingNamespace(NamespaceItemFactory.System);
            ViewModelDTO.AddUsingNamespace(NamespaceItemFactory.SystemCollectionsGeneric);

            var RecordFormatFieldsVariables = VariableFactory.Of(recordFormatHeader.RecordFormatFields.ITypeDefinitions).ToList();

            RecordFormatFieldsVariables.ForEach(recordFormatField =>
            {
                ViewModelDTO.AddContentLine($"public {recordFormatField.TypeSpelling} {recordFormatField.Name} {{ get; set; }}");
            });

            return ViewModelDTO;
        }
    }
}


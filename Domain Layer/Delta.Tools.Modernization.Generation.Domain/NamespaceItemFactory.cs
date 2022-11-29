using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using System.Collections.Generic;
using System.Linq;
using System;
using Delta.AS400.Partitions;
using Delta.Modernization.Statements.Items.Namespaces;

namespace Delta.CSharp.Statements.Items.Namespaces
{
    public class NamespaceItemFactory
    {
        //public static NamespaceItem Of(IEnumerable<string> parts)
        //{
        //    return Of(string.Join(@".", parts));
        //}

        //public static NamespaceItem DeltaOf(IToClassification aLibrary)
        //{
        //    return Of(Delta.Concat(aLibrary.ToPascalCaseClassification()));
        //}

        //public static NamespaceItem DeltaOf(IToClassification aLibrary, string aLastName)
        //{
        //    return Of(Delta.Concat(aLibrary.ToPascalCaseClassification()).Append(aLastName));
        //}

        //static IEnumerable<string> Delta = new List<string>() { "Delta" };
        public static NamespaceItem Delta = NamespaceItem.Of("Delta");
        public static NamespaceItem DeltaAS400 = NamespaceItem.Of("Delta.AS400");
        public static NamespaceItem DeltaAS400Adapters = NamespaceItem.Of("Delta.AS400.Adapters");
        public static NamespaceItem DeltaAS400DDSs = NamespaceItem.Of("Delta.AS400.DDSs");
        public static NamespaceItem DeltaAS400DDSsPrinters = NamespaceItem.Of("Delta.AS400.DDSs.Printers");
        public static NamespaceItem DeltaAS400DataTypes = NamespaceItem.Of("Delta.AS400.DataTypes");
        public static NamespaceItem DeltaAS400DataTypesCharacters = NamespaceItem.Of("Delta.AS400.DataTypes.Characters");
        public static NamespaceItem DeltaAS400DataTypesNumerics = NamespaceItem.Of("Delta.AS400.DataTypes.Numerics");
        public static NamespaceItem DeltaAS400EmulatorCharacterEncoding = NamespaceItem.Of("Delta.AS400.Emulator.CharacterEncoding");
        public static NamespaceItem DeltaAS400Workstations = NamespaceItem.Of("Delta.AS400.Workstations");

        public static NamespaceItem DeltaAS400WorkstationsSndusrmsgs = NamespaceItem.Of("Delta.AS400.Workstations.Sndusrmsgs");
        public static NamespaceItem DeltaAS400Environments = NamespaceItem.Of("Delta.AS400.Environments");
        public static NamespaceItem DeltaAS400Indicators = NamespaceItem.Of("Delta.AS400.Indicators");
        public static NamespaceItem DeltaAS400Objects = NamespaceItem.Of("Delta.AS400.Objects");
        
        public static NamespaceItem DeltaEntities = NamespaceItem.Of("Delta.Entities");
        public static NamespaceItem DeltaRelationalDatabases = NamespaceItem.Of("Delta.RelationalDatabases");
        public static NamespaceItem DeltaRelationalDatabasesDb2fori = NamespaceItem.Of("Delta.RelationalDatabases.Db2fori");
        public static NamespaceItem DeltaRelationalDatabasesEF = NamespaceItem.Of("Delta.RelationalDatabases.EF");
        public static NamespaceItem DeltaToolsModernizationTest = NamespaceItem.Of("Delta.Tools.Modernization.Test");
        

        public static NamespaceItem DeltaOf(Partition partition)
        {
            return NamespaceItem.Of($"Delta.{partition.Name.ToPascalCase()}");
        }
        public static NamespaceItem DeltaOf(Library aLibrary)
        {
            return NamespaceItem.Of($"Delta.{aLibrary.Partition.Name.ToPascalCase()}.{aLibrary.Name.ToPascalCase()}");
        }
        public static NamespaceItem DeltaOf(ObjectID objectID)
        {
            return DeltaOf(objectID.Library,objectID.Name.ToPascalCase());
        }
        public static NamespaceItem DeltaOf(Library aLibrary,string aName)
        {
            return NamespaceItem.Of($"Delta.{aLibrary.Partition.Name.ToPascalCase()}.{aLibrary.Name.ToPascalCase()}.{aName}s");
        }

        public static NamespaceItem MicrosoftEntityFrameworkCore = NamespaceItem.Of("Microsoft.EntityFrameworkCore");

        public static NamespaceItem PrismMvvm = NamespaceItem.Of("Prism.Mvvm");
        public static NamespaceItem PrismServicesDialogs = NamespaceItem.Of("Prism.Services.Dialogs");
        public static NamespaceItem ReactiveBindings = NamespaceItem.Of("Reactive.Bindings");
        public static NamespaceItem ReactiveBindingsLinq = NamespaceItem.Of("System.Reactive.Linq");

        public static NamespaceItem ReactiveBindingsNotifiers = NamespaceItem.Of("Reactive.Bindings.Notifiers");
        public static NamespaceItem System = NamespaceItem.Of("System");
        public static NamespaceItem SystemCollectionsGeneric = NamespaceItem.Of("System.Collections.Generic");
        public static NamespaceItem SystemDataCommon = NamespaceItem.Of("System.Data.Common");

        public static NamespaceItem SystemIO = NamespaceItem.Of("System.IO");

        public static NamespaceItem SystemLinq = NamespaceItem.Of("System.Linq");

        public static NamespaceItem SystemLinqExpressions = NamespaceItem.Of("System.Linq.Expressions");
        public static NamespaceItem SystemText = NamespaceItem.Of("System.Text");

        public static NamespaceItem SystemWindowsControls = NamespaceItem.Of("System.Windows.Controls");

        public static NamespaceItem Xunit = NamespaceItem.Of("Xunit");
        public static NamespaceItem XunitAbstractions = NamespaceItem.Of("Xunit.Abstractions");



    }
}

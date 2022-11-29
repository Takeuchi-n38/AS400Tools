using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.Modernization.Generation
{
    public class DIGenerator
    {
        PathResolverForTool PathResolver;
        DIGenerator(PathResolverForTool aPathResolver)
        {
            this.PathResolver= aPathResolver;
        }

        public static DIGenerator Of(PathResolverForTool aPathResolver)
        {
            return new DIGenerator(aPathResolver);
        }

        public void CreateDependencyInjector(List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris)
        {
            var contents = DIGenerator.CreateIDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDbContexts, objectIDForDB2foris);
            PathResolver.WriteIDependencyInjectorSource(contents);

            //contents = DIGenerator.CreateDbContextDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDbContexts);
            //PathResolver.WriteDbContextDependencyInjectorSource(contents);

            contents = CreateDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDbContexts, objectIDForDB2foris);
            PathResolver.WriteDependencyInjectorSource(contents);

        }

        static IEnumerable<string> ObjectIDsUsingLines(List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris)
        {
            return objectIDForDbContexts.Where(item => !item.isLF)
                 .Concat(objectIDForDB2foris.Where(item => !item.isLF))
                 .Select(item => NamespaceItemFactory.DeltaOf(item.objectID).ToUsingLine)
            .OrderBy(item => item);
        }

        public static string CreateIDependencyInjectorContents(string mainLibraryPascalName, List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris)
        {

            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.DeltaAS400Adapters.ToUsingLine);

            ObjectIDsUsingLines(objectIDForDbContexts, objectIDForDB2foris).ToList().ForEach(item => contents.AppendLine(item));

            contents.AppendLine(NamespaceItemFactory.Delta.ToNamespaceLine);
            contents.AppendLine("{");

            var indent = new Indent();

            contents.AppendLine($"{Indent.Single}public class I{mainLibraryPascalName}DependencyInjector");
            contents.AppendLine($"{Indent.Single}{{");
            contents.AppendLine($"{Indent.Couple}public IPgmCaller PgmCaller {{ get; set; }}");

            objectIDForDbContexts.Select(item => item.objectID)
                .OrderBy(item => item.Library.Partition.Name).ThenBy(item => item.Library.Name).ThenBy(item => item.Name)
                .ToList().ForEach(objectID => contents.AppendLine($"{Indent.Couple}public I{objectID.Name.ToPublicModernName()}Repository {objectID.Name.ToPublicModernName()}Repository {{ get; set; }}"));

            objectIDForDB2foris.Select(item => item.objectID)
                .OrderBy(item => item.Library.Partition.Name).ThenBy(item => item.Library.Name).ThenBy(item => item.Name)
                .ToList().ForEach(objectID => contents.AppendLine($"{Indent.Couple}public I{objectID.Name.ToPublicModernName()}Repository {objectID.Name.ToPublicModernName()}Repository {{ get; set; }}"));

            contents.AppendLine($"{Indent.Single}}}");
            contents.AppendLine("}");

            return contents.ToString();

        }

        //static string CreateDbContextDependencyInjectorContents(string mainLibraryPascalName, List<(bool isLF, ObjectID objectID)> objectIDForDbContexts)
        //{
        //    var contents = new StringBuilder();

        //    contents.AppendLine(NamespaceItemFactory.DeltaAS400Adapters.ToUsingLine);
        //    contents.AppendLine(NamespaceItemFactory.DeltaRelationalDatabases.ToUsingLine);
        //    contents.AppendLine(NamespaceItemFactory.MicrosoftEntityFrameworkCore.ToUsingLine);
        //    contents.AppendLine(NamespaceItemFactory.SystemDataCommon.ToUsingLine);

        //    objectIDForDbContexts.Where(item => !item.isLF).ToList().ForEach(item => contents.AppendLine(NamespaceItemFactory.DeltaOf(item.objectID).ToUsingLine));

        //    contents.AppendLine(NamespaceItemFactory.Delta.ToNamespaceLine);
        //    contents.AppendLine("{");

        //    var indent = new Indent();

        //    contents.AppendLine($"{Indent.Single}public class {mainLibraryPascalName}DbContextDependencyInjector : I{mainLibraryPascalName}DependencyInjector");
        //    contents.AppendLine($"{Indent.Single}{{");

        //    contents.AppendLine($"{Indent.Couple}public {mainLibraryPascalName}DbContextDependencyInjector(DbContext context)");
        //    contents.AppendLine($"{Indent.Couple}{{");

        //    objectIDForDbContexts.Select(item => item.objectID)
        //        .OrderBy(item => item.Library.Partition.Name).ThenBy(item => item.Library.Name).ThenBy(item => item.Name)
        //        .ToList().ForEach(objectID => contents.AppendLine($"{Indent.Triple}{objectID.Name.ToPublicModernName()}Repository = new {objectID.Name.ToPublicModernName()}DbContextRepository(context);"));
            
        //    contents.AppendLine($"{Indent.Couple}}}");

        //    contents.AppendLine($"{Indent.Single}}}");
        //    contents.AppendLine("}");

        //    return contents.ToString();

        //}

        static string CreateDependencyInjectorContents(string mainLibraryPascalName, List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris)
        {
            IEnumerable<ObjectID> objectIDForDB2s = objectIDForDB2foris.Select(item => item.objectID)
                .OrderBy(item => item.Library.Partition.Name).ThenBy(item => item.Library.Name).ThenBy(item => item.Name);

            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.DeltaAS400Adapters.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaRelationalDatabases.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.MicrosoftEntityFrameworkCore.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemDataCommon.ToUsingLine);

            objectIDForDB2foris.Where(item => !item.isLF).ToList().ForEach(item => contents.AppendLine(NamespaceItemFactory.DeltaOf(item.objectID).ToUsingLine));

            contents.AppendLine(NamespaceItemFactory.Delta.ToNamespaceLine);
            contents.AppendLine("{");

            var indent = new Indent();

            contents.AppendLine($"{Indent.Single}public class {mainLibraryPascalName}DependencyInjector : I{mainLibraryPascalName}DependencyInjector");
            contents.AppendLine($"{Indent.Single}{{");

            contents.AppendLine($"{Indent.Couple}public {mainLibraryPascalName}DependencyInjector(DbContext context, IDatabaseOperatedBySQL aDB2foriOperatedBySQL)");
            contents.AppendLine($"{Indent.Couple}{{");

            objectIDForDbContexts.Select(item => item.objectID)
    .OrderBy(item => item.Library.Partition.Name).ThenBy(item => item.Library.Name).ThenBy(item => item.Name)
    .ToList().ForEach(objectID => contents.AppendLine($"{Indent.Triple}{objectID.Name.ToPublicModernName()}Repository = new {objectID.Name.ToPublicModernName()}DbContextRepository(context);"));

            objectIDForDB2s
                .ToList().ForEach(objectID => contents.AppendLine($"{Indent.Triple}{objectID.Name.ToPublicModernName()}Repository = {objectID.Name.ToPublicModernName()}DB2Repository.Of(aDB2foriOperatedBySQL);"));
            
            contents.AppendLine($"{Indent.Couple}}}");

            contents.AppendLine($"{Indent.Single}}}");
            contents.AppendLine("}");

            return contents.ToString();

        }

    }
}

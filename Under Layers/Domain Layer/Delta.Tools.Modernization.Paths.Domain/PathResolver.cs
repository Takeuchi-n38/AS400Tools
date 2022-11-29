using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Delta.Tools.Modernization
{
    public class PathResolver
    {

        public string CreatePostgresTableScript(string libraryName,string objectName)
        {
            return File.ReadAllText(Path.Combine(CreatePostgresTableScriptSqlPath(libraryName), $"{objectName}.sql")).ToString();
        }
        public string CreateMssqlTableScript(string libraryName, string objectName)
        {
            return File.ReadAllText(Path.Combine(CreateMssqlTableScriptSqlPath(libraryName), $"{objectName}.sql")).ToString();
        }
        public string CreatePostgresTableScriptSqlPath(string schemaName) => Path.Combine(PostgresProjectFolderPathOfMainLibrary, "DatabaseScripts", schemaName, "Tables");
        public string PostgresProjectFolderPathOfMainLibrary => Path.Combine(SolutionFolderPathOfMainLibrary, DeltaOfName(PartitionName, LibraryName, "Postgres"));
        public string CreateMssqlTableScriptSqlPath(string schemaName) => Path.Combine(MssqlProjectFolderPathOfMainLibrary, "DatabaseScripts", schemaName, "Tables");
        public string MssqlProjectFolderPathOfMainLibrary => Path.Combine(SolutionFolderPathOfMainLibrary, DeltaOfName(PartitionName, LibraryName, "Mssql"));
        public string DbContextProjectFolderPathOfMainLibrary => Path.Combine(SolutionFolderPathOfMainLibrary, DeltaOfName(PartitionName, LibraryName, "DbContext"));
        public string SolutionFolderPathOfMainLibrary => SolutionFolderPathOf(PartitionName, LibraryName);
        public string SolutionFolderPathOf(string aPartitionName, string aLibraryName) => Path.Combine(SolutionsDirectory.FullName, DeltaOfName(aPartitionName,aLibraryName));
        public DirectoryInfo SolutionsDirectory => ModernaizationRootDirectory.CreateIfNotExists("Solutions");

        public DirectoryInfo ModernaizationRootDirectory { get; }

        public DirectoryInfo CometSourcesDirectory => ModernaizationRootDirectory.CreateIfNotExists("CometSources");

        string PartitionName { get; }

        string LibraryName { get; }
        protected PathResolver(string aModernaizationRootDirectoryPath, string aPartitionName, string aLibraryName)
        {
            this.ModernaizationRootDirectory = DirectoryInfoExtension.CreateIfNotExists(aModernaizationRootDirectoryPath);
            this.PartitionName = aPartitionName;
            this.LibraryName = aLibraryName;
        }

        public static PathResolver Of(string aModernaizationRootDirectoryPath, string aPartitionName, string aLibraryName)
        {
            return new PathResolver(aModernaizationRootDirectoryPath, aPartitionName, aLibraryName);
        }

        public static string DeltaOfName(string aPartitionName,string aLibraryName, string aLastName)
        {
            return $"Delta.{aPartitionName.ToPascalCase()}.{aLibraryName.ToPascalCase()}.{aLastName}";
        }
        public static string DeltaOfName(string aPartitionName, string aLibraryName)
        {
            return $"Delta.{aPartitionName.ToPascalCase()}.{aLibraryName.ToPascalCase()}";
        }
        public static IEnumerable<string> Delta = new List<string>() { "Delta" };

    }

}

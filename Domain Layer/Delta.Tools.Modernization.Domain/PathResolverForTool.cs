using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization
{
    public class PathResolverForTool : PathResolver
    {
        public string SolutionFolderPathOf(Library aLibrary) => SolutionFolderPathOf(aLibrary.Partition.Name,aLibrary.Name);

        PathResolverForTool(string aModernaizationRootDirectoryPath, Library aMainLibrary) : base(aModernaizationRootDirectoryPath, aMainLibrary.Partition.Name, aMainLibrary.Name)
        {
            this.MainLibrary = aMainLibrary;
        }

        public static PathResolverForTool Of(string aModernaizationRootDirectoryPath, Library aMainLibrary)
        {
            return new PathResolverForTool(aModernaizationRootDirectoryPath, aMainLibrary);
        }

        public string MainLibraryPascalName => $"{MainLibrary.Partition.Name.ToPascalCase()}{MainLibrary.Name.ToPascalCase()}";

        public Library MainLibrary { get; }
        public static string DeltaOfName(Library aMainLibrary, string aLastName)
        {
            return DeltaOfName(aMainLibrary.Partition.Name, aMainLibrary.Name, aLastName);
        }

        public string DomainProjectFolderPathOf(Library aMainLibrary) => Path.Combine(SolutionFolderPathOf(aMainLibrary), DeltaOfName(aMainLibrary, "Domain"));
        public string DomainProjectFolderPathOfMainLibrary => DomainProjectFolderPathOf(MainLibrary);

        public string DomainFolderPath(Library aMainLibrary, Library library) => Path.Combine(DomainProjectFolderPathOf(aMainLibrary), FolderPath(library));
        public string DomainFolderPath(Library library) => DomainFolderPath(MainLibrary, library);

        public string ApplicationProjectFolderPathOf(Library aLibrary) => Path.Combine(SolutionFolderPathOf(aLibrary), DeltaOfName(aLibrary, "Application"));
        public string ApplicationProjectFolderPathOfMainLibrary => ApplicationProjectFolderPathOf(MainLibrary);
        public string ApplicationServiceFolderPath(ObjectID objectID) => Path.Combine(ApplicationProjectFolderPathOfMainLibrary, FolderPath(objectID));

        string FolderPath(ObjectID objectID) => $"{objectID.Library.Partition.Name.ToPascalCase()}\\{objectID.Library.Name.ToPascalCase()}\\{objectID.Name.ToPascalCase()}s";

        string FolderPath(Library library) => $"{library.Partition.Name.ToPascalCase()}\\{library.Name.ToPascalCase()}";

        public string ApplicationTestProjectFolderPathOf(Library aLibrary) => $"{ApplicationProjectFolderPathOf(aLibrary)}.Tests";
        public string ApplicationTestProjectFolderPathOfMainLibrary => ApplicationTestProjectFolderPathOf(MainLibrary);

        public string ApplicationTestFolderPath(ObjectID objectID) => Path.Combine(ApplicationTestProjectFolderPathOfMainLibrary, FolderPath(objectID));

        string PresentationProjectFolderPathOfMainLibrary => Path.Combine(SolutionFolderPathOfMainLibrary, DeltaOfName(MainLibrary, "PrismCoreApp"));

        public string PresentatioFileFolderPath(ObjectID objectID) => Path.Combine(PresentationProjectFolderPathOfMainLibrary, FolderPath(objectID));

        public string InfrastructureProjectFolderPathOfMainLibrary => Path.Combine(SolutionFolderPathOfMainLibrary, DeltaOfName(MainLibrary, "Infrastructure"));

        public string Db2foriProjectFolderPathOf(Library aMainLibrary) => Path.Combine(SolutionFolderPathOf(aMainLibrary), DeltaOfName(aMainLibrary, "Db2fori"));

        public string DbContextFolderPath(Library library) => Path.Combine(DbContextProjectFolderPathOfMainLibrary, FolderPath(library));
        public string Db2foriFolderPath(Library aMainLibrary, Library library) => Path.Combine(Db2foriProjectFolderPathOf(aMainLibrary), FolderPath(library));

        public string Db2TestHelperProjectFolderPathOf(Library aLibrary) => Path.Combine(SolutionFolderPathOf(aLibrary), DeltaOfName(aLibrary, "Test"));




        public void WriteServiceTestSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationTestFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteFormatterTestSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationTestFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteRecordFormatXamlSource(ObjectID objectID, string className, string contents)
        {
            string folder = PresentatioFileFolderPath(objectID);

            FileHelper.WriteAllText(folder, className, "gen.xaml", $"{contents}{Environment.NewLine}<!-- 1.0.0.1 -->");
        }

        public void WriteRecordFormatXamlCsSource(ObjectID objectID, string className, string contents)
        {
            string folder = PresentatioFileFolderPath(objectID);

            FileHelper.WriteAllText(folder, className, "gen.xaml.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDisplayFileXamlSource(ObjectID objectID, string className, string contents)
        {
            string folder = PresentatioFileFolderPath(objectID);

            FileHelper.WriteAllText(folder, className, "gen.xaml", $"{contents}{Environment.NewLine}<!-- 1.0.0.1 -->");
        }

        public void WriteDisplayFileXamlCsSource(ObjectID objectID, string className, string contents)
        {
            string folder = PresentatioFileFolderPath(objectID);

            FileHelper.WriteAllText(folder, className, "gen.xaml.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDbContextDependencyInjectorSource(string contents)
        {
            string fileName = $"{MainLibraryPascalName}DbContextDependencyInjector.gen.cs";
            string toFolderPath = InfrastructureProjectFolderPathOfMainLibrary;

            FileHelper.WriteAllText(toFolderPath, fileName, $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDependencyInjectorSource(string contents)
        {
            string fileName = $"{MainLibraryPascalName}DependencyInjector.gen.cs";
            string toFolderPath = InfrastructureProjectFolderPathOfMainLibrary;

            FileHelper.WriteAllText(toFolderPath, fileName, $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteIDependencyInjectorSource(string contents)
        {
            string toFolderPath = ApplicationProjectFolderPathOfMainLibrary;
            string fileName = $"I{MainLibraryPascalName}DependencyInjector.gen.cs";

            FileHelper.WriteAllText(toFolderPath, fileName, $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteCLServiceSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteRPGServiceSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteCOBOLServiceSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteFormatterSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }


        public void WriteProgramDescribedFileSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteIPresenterSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteViewModelDTOSource(ObjectID objectID, string className, string contents)
        {
            var folder = ApplicationServiceFolderPath(objectID);
            FileHelper.WriteAllText(folder, className, "gen.cs", $"{contents}{Environment.NewLine}//1.0.0.1");
        }
        public void WriteEntitySource(ObjectID objectID, string contents, string originalComments)
        {
            var EntityName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(DomainFolderPath(objectID.Library), $"{EntityName}s");
            FileHelper.WriteAllText(toFolderPath, $"{EntityName}.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDB2EntitySource(ObjectID objectID, string contents, string originalComments)
        {
            var EntityName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(DomainFolderPath(objectID.Library, objectID.Library), $"{EntityName}s");
            FileHelper.WriteAllText(toFolderPath, $"{EntityName}.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteIRepositorySource(string entityName, ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(DomainFolderPath(objectID.Library), $"{entityName}s");
            FileHelper.WriteAllText(toFolderPath, $"I{fileName}Repository.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDB2IRepositorySource(string entityName, ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(DomainFolderPath(objectID.Library, objectID.Library), $"{entityName}s");
            FileHelper.WriteAllText(toFolderPath, $"I{fileName}Repository.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDbContextRepositorySource(string entityName, ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(DbContextFolderPath(objectID.Library), $"{entityName}s");
            FileHelper.WriteAllText(toFolderPath, $"{fileName}DbContextRepository.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WritePostgresTableDDSSource(ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToLower();
            string toFolderPath = CreatePostgresTableScriptSqlPath(objectID.Library.Name.ToPascalCase());
            FileHelper.WriteAllText(toFolderPath, $"{fileName}.gen.sql", $"{contents}{Environment.NewLine}/*{Environment.NewLine}{originalComments}{Environment.NewLine}*/{Environment.NewLine}--1.0.0.1");
        }
        public void WriteMssqlTableDDSSource(ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToLower();
            string toFolderPath = CreateMssqlTableScriptSqlPath(objectID.Library.Name.ToPascalCase());
            FileHelper.WriteAllText(toFolderPath, $"{fileName}.gen.sql", $"{contents}{Environment.NewLine}/*{Environment.NewLine}{originalComments}{Environment.NewLine}*/{Environment.NewLine}--1.0.0.1");
        }
        public void WriteDB2RepositorySource(string entityName, ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(Db2foriFolderPath(objectID.Library, objectID.Library), $"{entityName}s");
            FileHelper.WriteAllText(toFolderPath, $"{fileName}DB2Repository.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteEntityTestHelper(ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            var toFolderPath = ApplicationTestFolderPath(objectID);
            FileHelper.WriteAllText(toFolderPath, $"{fileName}TestHelper.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteDB2EntityTestHelperSource(ObjectID objectID, string contents, string originalComments)
        {
            string fileName = objectID.Name.ToPascalCase();
            string toFolderPath = Path.Combine(Db2TestHelperProjectFolderPathOf(objectID.Library), $"{fileName}s");
            FileHelper.WriteAllText(toFolderPath, $"{fileName}TestHelper.gen.cs", $"{contents}{Environment.NewLine}{originalComments}{Environment.NewLine}//1.0.0.1");
        }

        public void WriteAllText(string toFolderPath, string fileName, string contents, string originalComments)
        {
            WriteAllText(toFolderPath, fileName, $"{contents}{Environment.NewLine}{originalComments}");
        }

        void WriteAllText(string toFolderPath, string fileName, string contents)
        {
            FileHelper.WriteAllText(toFolderPath, fileName, $"{contents}{Environment.NewLine}//1.0.0.1");
        }


    }

}

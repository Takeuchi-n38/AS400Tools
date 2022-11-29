using Delta.AS400.Objects;
using Delta.RelationalDatabases.Db2fori;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Delta.Tools.Modernization.Test
{
    public class TestFilesHelper
    {
        [Obsolete("廃止予定です。DB2foriOperatorを使ってください。")]
        public static DB2foriOperator TestDB2foriOperatedByFTP = DB2foriOperator.TestOf();

        [Obsolete("廃止予定です。DB2foriOperatorを使ってください。")]
        public static DB2foriOperator TestDB2foriOperatedBySQL = DB2foriOperator.TestOf();

        public static DB2foriOperator DB2foriOperator = DB2foriOperator.TestOf();


        public readonly static string ExtensionOfAS400BinaryFile = "ccsid930";

        readonly PathResolver PathResolver;
        readonly TestTarget TestTarget;
        readonly string TargetName;
        DirectoryInfo TestDatasDirectory => PathResolver.ModernaizationRootDirectory.CreateIfNotExists("TestDatas");

        //public IEnumerable<(string tableName, string ddlScript)> CreateTableScripts(string schemaName, IEnumerable<string> tableNames)
        //{
        //    return PathResolver.CreateTableScripts(schemaName, tableNames);
        //}

        public string CreateMssqlTableScript(ObjectID objectID)
        {
            return PathResolver.CreateMssqlTableScript(objectID.Library.Name, objectID.Name);
        }

        public string CreatePostgresTableScript(ObjectID objectID)
        {
            return PathResolver.CreatePostgresTableScript(objectID.Library.Name, objectID.Name);
        }

        //public TestFilesHelper(Library aMainSchemaLibrary, string aTargetName)
        //{
        //    //this.MainSchemaLibrary = aMainSchemaLibrary;
        //    this.TargetName = aTargetName;
        //}

        public TestFilesHelper(PathResolver PathResolver, TestTarget aTestTarget, string aTargetName)
        {
            this.PathResolver = PathResolver;
            this.TestTarget= aTestTarget;
            this.TargetName = aTargetName;
        }

        public static TestFilesHelper ForServiceOf(PathResolver aPathResolver, TestTarget aTestTarget)
        {
            return new TestFilesHelper(aPathResolver, aTestTarget, aTestTarget.ServiceName);
        }
        public static TestFilesHelper ForFormatterOf(PathResolver aPathResolver, TestTarget aTestTarget)
        {
            return new TestFilesHelper(aPathResolver, aTestTarget, aTestTarget.FormatterName);
        }

         string SavfsFolder(string caseName) => Path.Combine(TestDatasDirectory.FullName, $"{TargetName}.{caseName}.Savfs");

        public string CaseFolder(string caseName) => Path.Combine(TestDatasDirectory.FullName, $"{TargetName}.{caseName}");

        public string SetupFolder(string caseName) => Path.Combine(CaseFolder(caseName), "Setup");

        public string ActualFolder(string caseName) => Path.Combine(CaseFolder(caseName), "Actual");

        public string ExpectedFolder(string caseName) => Path.Combine(CaseFolder(caseName), "Expected");

        public void WriteAllBytesToActualFolder(string caseName, IEnumerable<byte[]> aBytes, ObjectID objectID)
        {
            WriteAllBytesToActualFolder( aBytes,caseName,FileName(objectID));
        }

        public void WriteAllBytesToActualFolder(IEnumerable<byte[]> aBytes, string aCaseName, string aFileName)
        {
            var first = aBytes.FirstOrDefault();
            var fileLength = first == null ? 0 : first.Length;
            var saveFileName = $"{aFileName}.{fileLength}";
            WriteAllBytes(aBytes, ActualFolder(aCaseName), saveFileName, ExtensionOfAS400BinaryFile);
        }

        public static void WriteAllBytes(IEnumerable<byte[]> aEnumerableBytes, string aFolderPath, string aFileName, string aFileExtension)
        {
            var bytes = new List<byte>();
            aEnumerableBytes.ToList().ForEach(a =>
            {
                bytes.AddRange(a);
            });

            Directory.CreateDirectory(aFolderPath);

            File.WriteAllBytes(Path.Combine(aFolderPath, $"{aFileName}.{aFileExtension}"), bytes.ToArray());

        }

        public void WriteAllCsvLinesToActualFolder(string aCaseName, IEnumerable<string> csvLines, ObjectID objectID)
        {
            var folderPath = ActualFolder(aCaseName);
            Directory.CreateDirectory(folderPath);
            File.WriteAllLines(Path.Combine(folderPath, $"{objectID.Library.Partition.Name}.{objectID.Library.Name}.{objectID.Name}.csv"), csvLines);
        }


        string FileName(ObjectID objectID) => $"{objectID.Library.Partition.Name}.{objectID.Library.Name}.{objectID.Name}";


        public static (string filePath, int fileLength) FileInfo(string aFolderName, string aFileName)
        {
            var filePath = Directory.GetFiles(aFolderName)
                .Where(fn => fn.EndsWith($".{ExtensionOfAS400BinaryFile}") && fn.Contains(aFileName)).First();
            var fileLength = int.Parse(filePath.Replace($".{ExtensionOfAS400BinaryFile}", string.Empty).Split('.').Last());
            return (filePath, fileLength);
        }

        public IEnumerable<byte[]> ReadBytesFromSetupFolder(string caseName, ObjectID objectID)
        {
            return ReadBytesFromSetupFolder(caseName, FileName(objectID));
        }

        public IEnumerable<byte[]> ReadBytesFromSetupFolder(string caseName, string aFileName)
        {
            return ReadAllBytes(SetupFolder(caseName), aFileName);
        }

        public IEnumerable<byte[]> ReadBytesFromActualFolder(string caseName, ObjectID objectID)
        {
            return ReadAllBytes(ActualFolder(caseName), FileName(objectID));
        }

        public IEnumerable<byte[]> ReadBytesFromExpectedFolder(string caseName, ObjectID objectID)
        {
            return ReadAllBytes(ExpectedFolder(caseName), FileName(objectID));
        }

        public IEnumerable<byte[]> ReadBytesFromExpectedFolder(string caseName, string aFileName)
        {
            return ReadAllBytes(ExpectedFolder(caseName), aFileName);
        }

        //HONSHA01.MASTER9.PIF305DK.170.ccsid930
        static IEnumerable<byte[]> ReadAllBytes(string aFolderName, string aFileName)//string aFolderPath, string aFileName, string aFileExtension
        {
            var fileInfo = FileInfo(aFolderName, aFileName);

            var allBytes = File.ReadAllBytes(fileInfo.filePath);

            return allBytes.Select((v, i) => new { v, i })
                .GroupBy(x => x.i / fileInfo.fileLength)
                .Select(g => g.Select(x => x.v)).Select(x => x.ToArray());

        }

        public void DownloadSavfFile(string caseName)
        {
            // MIGRLIB/CIID015S →　"C:\Delta\TestDatas\HONSHA01.IIDLIB.CIID015s.Ciid015Service.1.Savfs\CIID015S.savf");
            // MIGRLIB/CIID015E →　"C:\Delta\TestDatas\HONSHA01.IIDLIB.CIID015s.Ciid015Service.1.Savfs\CIID015E.savf");

            DB2foriOperator.DownloadFile("MIGRLIB", TestTarget.SetupSavfName, SavfsFolder(caseName), TestTarget.SetupSavfName, "savf");
            DB2foriOperator.DownloadFile("MIGRLIB", TestTarget.ExpectedSavfName, SavfsFolder(caseName), TestTarget.ExpectedSavfName, "savf");

        }
        public void UploadSavfFile(string caseName)
        {

            DB2foriOperator.UploadFile("UPLDLIB", TestTarget.SetupSavfName, SavfsFolder(caseName), TestTarget.SetupSavfName, "savf");
            DB2foriOperator.UploadFile("UPLDLIB", TestTarget.ExpectedSavfName, SavfsFolder(caseName), TestTarget.ExpectedSavfName, "savf");
            DB2foriOperator.UploadFile("UPLDLIB", TestTarget.ActualSavfName, SavfsFolder(caseName), TestTarget.ActualSavfName, "savf");
        }

        //public void DownloadActualSavfFileToCase1Folder()
        //{
        //    DownloadActualSavfFile("1");
        //}

        public void DownloadActualSavfFile(string caseName)
        {
            DB2foriOperator.DownloadFile("DWLDLIB", TestTarget.ActualSavfName, SavfsFolder(caseName), TestTarget.ActualSavfName, "savf");
        }

    }
}

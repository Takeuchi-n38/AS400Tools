using Delta.AS400.Objects;
using Delta.RelationalDatabases;
using Delta.RelationalDatabases.Db2fori;
using Delta.RelationalDatabases.Mssql;
using Delta.RelationalDatabases.Postgres;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;

//using Delta.AS400.DDSs.Printers;
//using Delta.AS400.Printers.Pdf;

namespace Delta.Tools.Modernization.Test
{
    public partial class TestHelper
    {
        //static Partition PartitionOfTest = Partition.Test;

        //public static readonly DB2foriOperatedBySQL TestDB2foriDbContext = DB2foriOperatedBySQL.TestDB2foriDbContext;
        //static string UserIDofTest = "QUSER";
        //static string PasswordOfTest = "QUSER";
        public static string IPofTestDB2fori = "192.168.10.229";

        [Obsolete("廃止予定です。DB2foriOperatorを使ってください。")]
        public static DB2foriOperator TestDB2foriOperatedByFTP = DB2foriOperator.TestOf();

        [Obsolete("廃止予定です。DB2foriOperatorを使ってください。")]
        public static DB2foriOperator TestDB2foriOperatedBySQL = DB2foriOperator.TestOf();


        public static DB2foriOperator DB2foriOperator = DB2foriOperator.TestOf();

        //public static DB2foriDatabaseService TestDB2foriDatabaseService = DB2foriDatabaseService.Of(TestDB2foriOperatedBySQL);

        PostgresDatabaseService TestPostgresDatabaseService;

        MssqlDatabaseService TestMssqlDatabaseService;

        static string ExtensionOfAS400BinaryFile => TestFilesHelper.ExtensionOfAS400BinaryFile;

        readonly TestTarget TestTarget;

        readonly TestFilesHelper TestFilesHelper;

        //readonly DBAssertionTestCaseService TestService;
        public Schema ActualSchema() => TestTarget.ActualSchema();
        public string ActualSchemaName(string caseName) => TestTarget.ActualSchemaName(caseName);
        public Schema ActualSchema(string caseName) => TestTarget.ActualSchema(caseName);

        public string SetupSchemaName(string caseName) => TestTarget.SetupSchemaName(caseName);
        public Schema SetupSchema(string caseName) => TestTarget.SetupSchema(caseName);

        //public string ActualLibraryName(string caseName) => $"{TestTarget.Name}A{caseName}";
        //public Schema ActualSchema => Schema.Of(ActualLibraryName);

        string ExpectedSchemaName(string caseName) => TestTarget.ExpectedSchemaName(caseName);
        public Schema ExpectedSchema(string caseName) => TestTarget.ExpectedSchema(caseName);

        readonly int BulkCount;
        TestHelper(TestTarget aTestTarget, TestFilesHelper aTestFilesHelper, 
            PostgresDatabaseService aTestPostgresDatabaseService,
            MssqlDatabaseService aTestMssqlDatabaseService,
            int aBulkCount) //:this(null, targetObjectID)
        {
            TestTarget = aTestTarget;
            TestFilesHelper = aTestFilesHelper;
            TestPostgresDatabaseService = aTestPostgresDatabaseService;
            TestMssqlDatabaseService = aTestMssqlDatabaseService;
            BulkCount = aBulkCount;
        }

        //static TestHelper Of(TestTarget aTestTarget, string targetName, DbConnectionStringBuilder aDbConnectionStringBuilder, Library aMainLibrary)
        //{
        //    var TestFilesHelper = new TestFilesHelper(PathInfo.Of(aMainLibrary), targetName);
        //    return new TestHelper(aTestTarget, TestFilesHelper, PostgresDatabaseService.Of(aDbConnectionStringBuilder));
        //}
        public static TestHelper ForServiceOf(TestTarget aTestTarget,
            DbConnectionStringBuilder aPostgresDbConnectionStringBuilder,
            DbConnectionStringBuilder aMssqlDbConnectionStringBuilder,
            PathResolver aPathResolver)
        {
            return ForServiceOf(aTestTarget, aPostgresDbConnectionStringBuilder, aMssqlDbConnectionStringBuilder, aPathResolver, 10000);
        }

        public static TestHelper ForServiceOf(TestTarget aTestTarget, 
            DbConnectionStringBuilder aPostgresDbConnectionStringBuilder,
            DbConnectionStringBuilder aMssqlDbConnectionStringBuilder,
            PathResolver aPathResolver, int aBulkCount)
        {
            var testFilesHelper = TestFilesHelper.ForServiceOf(aPathResolver,aTestTarget);
            return new TestHelper(aTestTarget, testFilesHelper, 
                PostgresDatabaseService.Of(aPostgresDbConnectionStringBuilder),
                MssqlDatabaseService.Of(aMssqlDbConnectionStringBuilder),
                aBulkCount);
        }

        public static TestHelper ForFormatterOf(TestTarget aTestTarget, 
            DbConnectionStringBuilder aPostgresDbConnectionStringBuilder,
            DbConnectionStringBuilder aMssqlDbConnectionStringBuilder, PathResolver aPathResolver)
        {
            return ForFormatterOf(aTestTarget, aPostgresDbConnectionStringBuilder, aMssqlDbConnectionStringBuilder, aPathResolver,10000);
        }

        public static TestHelper ForFormatterOf(TestTarget aTestTarget, 
            DbConnectionStringBuilder aPostgresDbConnectionStringBuilder,
            DbConnectionStringBuilder aMssqlDbConnectionStringBuilder,
            PathResolver aPathResolver, int aBulkCount)
        {
            var testFilesHelper = TestFilesHelper.ForFormatterOf(aPathResolver, aTestTarget);
            return new TestHelper(aTestTarget, testFilesHelper, 
                PostgresDatabaseService.Of(aPostgresDbConnectionStringBuilder),
                MssqlDatabaseService.Of(aMssqlDbConnectionStringBuilder),
                aBulkCount);
        }

        public void DownloadSavfFile(string caseName)
        {
            TestFilesHelper.DownloadSavfFile(caseName);
        }

        public void UploadSavfFile(string caseName)
        {
            TestFilesHelper.UploadSavfFile(caseName);
        }

        public void DownloadActualSavfFileToCase1Folder()
        {
            DownloadActualSavfFile("1");
        }

        public void DownloadActualSavfFile(string caseName)
        {
            TestFilesHelper.DownloadActualSavfFile(caseName);
        }

        public void ConvertActualDataBinaryToCsv(string caseName, EntityTestHelper actualEntityTestHelper)
        {
            ConvertActualDataBinaryToCsv(caseName, new List<EntityTestHelper>(){ actualEntityTestHelper });
        }

        public void ConvertActualDataBinaryToCsv(string caseName, IEnumerable<EntityTestHelper> actualEntityTestHelpers)
        {
            ConvertCcsid930ToCsv(caseName, ActualFolder, actualEntityTestHelpers);
        }

        public void ConvertTestDataBinaryToCsv(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2SetupEntityTestHelpers = setupEntityTestHelpers.Where(t => t.IsCreateTable);
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            RecreatePostgresSchemas(caseName, removeDb2SetupEntityTestHelpers.Select(t => t.ObjectID), removeDb2ExpectedEntityTestHelpers.Select(t => t.ObjectID));

            ConvertCcsid930ToCsv(caseName, SetupFolder, setupEntityTestHelpers);
            ConvertCcsid930ToCsv(caseName, ExpectedFolder, expectedEntityTestHelpers);
        }

        void ConvertCcsid930ToCsv(string caseName, Func<string, string> getFolder, IEnumerable<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.ToList().ForEach(entityTestHelper => ConvertCcsid930ToCsv(getFolder(caseName), entityTestHelper));
        }

        public void ConvertCcsid930ToCsv(string folder, EntityTestHelper entityTestHelper)
        {

            var fileName = FileName(entityTestHelper.ObjectID);

            var fileInfo = TestFilesHelper.FileInfo(folder, fileName);

            using (var bynaryFile = new FileStream(fileInfo.filePath, FileMode.Open, FileAccess.Read))
            using (var csvFile = new StreamWriter(Path.Combine(folder, $"{fileName}.csv")))
            {
                if (entityTestHelper.IsOutputHeader)
                {
                    var columnNames = entityTestHelper.ColumnNames();
                    if (columnNames.Count() == 0)
                    {
                        columnNames = 
                            TestPostgresDatabaseService.FindColumns(
                            Table.Of(Schema.Of(entityTestHelper.ObjectID.Library.Name), entityTestHelper.ObjectID.Name)
                            ).ColumnNames;
                    }
                    var joinedColumnNames = string.Join(",", columnNames.ToArray());
                    csvFile.WriteLine(joinedColumnNames);
                }
                var lineNumber = 0;
                while (true)
                {
                    byte[] bytes = new byte[fileInfo.fileLength];
                    int readSize = bynaryFile.Read(bytes, 0, bytes.Length);
                    if (readSize == 0) break;
                    //var values= entityTestHelper.ToStringValues(bytes);
                    //var csvLine= $"\"{string.Join("\",\"", values)}\"";
                    lineNumber++;
                    var values = entityTestHelper.ToStringValuesWithQuote(lineNumber.ToString(), bytes, "\"");
                    var csvLine = $"{string.Join(",", values)}";
                    csvFile.WriteLine(csvLine);
                }
            }
        }

        string FileName(ObjectID objectID) => $"{objectID.Library.Partition.Name}.{objectID.Library.Name}.{objectID.Name}";

        public void WriteAllBytesToActualFolderInUtf8Csv(string caseName, IEnumerable<byte[]> aBytes, EntityTestHelper entityTestHelper)
        {
            var fileName = FileName(entityTestHelper.ObjectID);
            WriteAllBytesToActualFolder(caseName,aBytes, entityTestHelper.ObjectID);
            ConvertCcsid930ToCsv(caseName, ActualFolder, new List<EntityTestHelper>(){ entityTestHelper });
        }

        public void InsertTestDataBinarySetupAndExpectedSchema(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2SetupEntityTestHelpers = setupEntityTestHelpers.Where(t => t.IsCreateTable);
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            RecreateMssqlSchemas(caseName, removeDb2SetupEntityTestHelpers.Select(t => t.ObjectID), removeDb2ExpectedEntityTestHelpers.Select(t => t.ObjectID));

            InsertCcsid930ToSetupDB(caseName, removeDb2SetupEntityTestHelpers);
            InsertCcsid930ToExpectedDB(caseName, removeDb2ExpectedEntityTestHelpers);
        }

        public void InsertTestDataBinarySetupAndExpectedPostgresSchema(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2SetupEntityTestHelpers = setupEntityTestHelpers.Where(t => t.IsCreateTable);
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            RecreatePostgresSchemas(caseName, removeDb2SetupEntityTestHelpers.Select(t => t.ObjectID), removeDb2ExpectedEntityTestHelpers.Select(t => t.ObjectID));

            InsertCcsid930ToSetupPostgresDB(caseName, removeDb2SetupEntityTestHelpers);
            InsertCcsid930ToExpectedPostgresDB(caseName, removeDb2ExpectedEntityTestHelpers);
        }

        void InsertCcsid930ToSetupPostgresDB(string caseName, IEnumerable<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.ToList().ForEach(entityTestHelper => InsertCcsid930ToPostgresDB(caseName, SetupFolder, SetupSchema, entityTestHelper));
        }

        void InsertCcsid930ToSetupDB(string caseName, IEnumerable<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.ToList().ForEach(entityTestHelper => InsertCcsid930ToDB(caseName, SetupFolder, SetupSchema, entityTestHelper));
        }

        void InsertCcsid930ToExpectedPostgresDB(string caseName, IEnumerable<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.ToList().ForEach(entityTestHelper => InsertCcsid930ToPostgresDB(caseName, ExpectedFolder, ExpectedSchema, entityTestHelper));
        }

        void InsertCcsid930ToExpectedDB(string caseName, IEnumerable<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.ToList().ForEach(entityTestHelper => InsertCcsid930ToDB(caseName, ExpectedFolder, ExpectedSchema, entityTestHelper));
        }

        void InsertCcsid930ToDB(string caseName, Func<string, string> getFolder, Func<string, Schema> getSchema, EntityTestHelper entityTestHelper)
        {
            var schema = getSchema(caseName);
            var table = Table.Of(Schema.Of(entityTestHelper.ObjectID.Library.Name), entityTestHelper.ObjectID.Name).CreateWithChangingSchema(schema);
            TestMssqlDatabaseService.Truncate(table);

            var folder = getFolder(caseName);
            var fileName = FileName(entityTestHelper.ObjectID);
            var fileInfo = TestFilesHelper.FileInfo(folder, fileName);

            using (var bynaryFile = new FileStream(fileInfo.filePath, FileMode.Open, FileAccess.Read))
            {
                var valuesLines = new List<IEnumerable<string>>();
                for (var lineNumber = 1; ; lineNumber++)
                {
                    byte[] bytes = new byte[fileInfo.fileLength];
                    int readSize = bynaryFile.Read(bytes, 0, bytes.Length);
                    if (readSize == 0)
                    {
                        if(valuesLines.Count>0) TestMssqlDatabaseService.InsertValues(table, valuesLines);
                        break;
                    }
                    var values = entityTestHelper.ToStringValuesWithQuote(lineNumber.ToString(), bytes, "'");
                    valuesLines.Add(values);
                    if (lineNumber % BulkCount == 0)
                    {
                        TestMssqlDatabaseService.InsertValues(table, valuesLines);
                        valuesLines.Clear();
                    }
                }
            }
        }

        void InsertCcsid930ToPostgresDB(string caseName, Func<string, string> getFolder, Func<string, Schema> getSchema, EntityTestHelper entityTestHelper)
        {
            var schema = getSchema(caseName);
            var table = Table.Of(Schema.Of(entityTestHelper.ObjectID.Library.Name), entityTestHelper.ObjectID.Name).CreateWithChangingSchema(schema);
            TestPostgresDatabaseService.Truncate(table);

            var folder = getFolder(caseName);
            var fileName = FileName(entityTestHelper.ObjectID);
            var fileInfo = TestFilesHelper.FileInfo(folder, fileName);

            using (var bynaryFile = new FileStream(fileInfo.filePath, FileMode.Open, FileAccess.Read))
            {
                var valuesLines = new List<IEnumerable<string>>();
                for (var lineNumber = 1; ; lineNumber++)
                {
                    byte[] bytes = new byte[fileInfo.fileLength];
                    int readSize = bynaryFile.Read(bytes, 0, bytes.Length);
                    if (readSize == 0)
                    {
                        if (valuesLines.Count > 0) TestPostgresDatabaseService.InsertValues(table, valuesLines);
                        break;
                    }
                    var values = entityTestHelper.ToStringValuesWithQuote(lineNumber.ToString(), bytes, "'");
                    valuesLines.Add(values);
                    if (lineNumber % BulkCount == 0)
                    {
                        TestPostgresDatabaseService.InsertValues(table, valuesLines);
                        valuesLines.Clear();
                    }
                }
            }
        }
        void RegistTestDataByCsvToMssql(string caseName, IEnumerable<ObjectID> SetupEntities, IEnumerable<ObjectID> ExpectedEntities)
        {
            RegistTestDataByCsvToMssql(SetupFolder(caseName), SetupSchema(caseName), SetupEntities);

            RegistTestDataByCsvToMssql(ExpectedFolder(caseName), ExpectedSchema(caseName), ExpectedEntities);
        }

        void RegistTestDataByCsvToPostgres(string caseName, IEnumerable<ObjectID> SetupEntities, IEnumerable<ObjectID> ExpectedEntities)
        {
            RegistTestDataByCsvToPostgres(SetupFolder(caseName), SetupSchema(caseName), SetupEntities);

            RegistTestDataByCsvToPostgres(ExpectedFolder(caseName), ExpectedSchema(caseName), ExpectedEntities);
        }

        void RegistTestDataByCsvToMssql(string folder, Schema schema, IEnumerable<ObjectID> entityObjectIDs)
        {
            entityObjectIDs.ToList().ForEach(entityObjectID =>
            {
                var fileName = FileName(entityObjectID);
                string csv_file_path = Path.Combine(folder, $"{fileName}.csv");
                TestMssqlDatabaseService.ReplaceDataByCsv(csv_file_path, Table.Of(schema, entityObjectID.Name));
            });
        }

        void RegistTestDataByCsvToPostgres(string folder, Schema schema, IEnumerable<ObjectID> entityObjectIDs)
        {
            entityObjectIDs.ToList().ForEach(entityObjectID =>
            {
                var fileName = FileName(entityObjectID);
                string csv_file_path = Path.Combine(folder, $"{fileName}.csv");
                TestPostgresDatabaseService.ReplaceDataByCsv(csv_file_path, Table.Of(schema, entityObjectID.Name));
            });
        }

        public void InsertTestDataCsvToSetupAndExpectedSchema(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2SetupEntityTestHelpers = setupEntityTestHelpers.Where(t => t.IsCreateTable);
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            var SetupEntities = removeDb2SetupEntityTestHelpers.Select(t => t.ObjectID);
            var ExpectedEntities = removeDb2ExpectedEntityTestHelpers.Select(t => t.ObjectID);

            RecreateMssqlSchemas(caseName, SetupEntities, ExpectedEntities);
            RegistTestDataByCsvToMssql(caseName, SetupEntities, ExpectedEntities);
        }

        public void InsertTestDataCsvToSetupAndExpectedPostgresSchema(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2SetupEntityTestHelpers = setupEntityTestHelpers.Where(t => t.IsCreateTable);
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            var SetupEntities = removeDb2SetupEntityTestHelpers.Select(t => t.ObjectID);
            var ExpectedEntities = removeDb2ExpectedEntityTestHelpers.Select(t => t.ObjectID);

            RecreatePostgresSchemas(caseName, SetupEntities, ExpectedEntities);
            RegistTestDataByCsvToPostgres(caseName, SetupEntities, ExpectedEntities);
        }

        void RecreateMssqlSchemas(string caseName, IEnumerable<ObjectID> SetupEntities, IEnumerable<ObjectID> ExpectedEntities)
        {

            var setupSchema = SetupSchema(caseName);
            ReCreateMssqlSchema(setupSchema, SetupEntities);

            var expectedSchema = ExpectedSchema(caseName);
            ReCreateMssqlSchema(expectedSchema, ExpectedEntities);

            var actualEntities = SetupEntities.Concat(ExpectedEntities).Distinct();
            ReplaceMssqlTables(actualEntities);

            CreateAssertMssqlViews(expectedSchema, ExpectedEntities.Select(objectID=>ToTableOf(objectID)));

        }

        void RecreatePostgresSchemas(string caseName, IEnumerable<ObjectID> SetupEntities, IEnumerable<ObjectID> ExpectedEntities)
        {

            var setupSchema = SetupSchema(caseName);
            ReCreatePostgresSchema(setupSchema, SetupEntities);

            var expectedSchema = ExpectedSchema(caseName);
            ReCreatePostgresSchema(expectedSchema, ExpectedEntities);

            var actualEntities = SetupEntities.Concat(ExpectedEntities).Distinct();
            ReplacePostgresTables(actualEntities);

            CreateAssertPostgresViews(expectedSchema, ExpectedEntities.Select(objectID => ToTableOf(objectID)));

        }

        void ReCreateMssqlSchema(Schema targetSchema, IEnumerable<ObjectID> objectIDs)            
        {
            objectIDs.ToList().ForEach(objectID =>
            {
                var createTableScript = TestFilesHelper.CreateMssqlTableScript(objectID).Replace(objectID.Library.Name.ToLower(), targetSchema.Name.ToLower());
                var table = targetSchema.CreateTableOf(objectID.Name.ToLower());
                TestMssqlDatabaseService.Create(table, createTableScript, true);
            });
        }

        void ReCreatePostgresSchema(Schema targetSchema, IEnumerable<ObjectID> objectIDs)
        {
            objectIDs.ToList().ForEach(objectID =>
            {
                var createTableScript = TestFilesHelper.CreatePostgresTableScript(objectID);
                var table = targetSchema.CreateTableOf(objectID.Name.ToLower());
                TestPostgresDatabaseService.Create(table, createTableScript, true);
            });
        }

        void ReplaceMssqlTables(IEnumerable<ObjectID> objectIDs)
        {
            objectIDs.ToList().ForEach(objectID =>
            {
                var createTableScript = TestFilesHelper.CreateMssqlTableScript(objectID);
                var table = ToTableOf(objectID);
                TestMssqlDatabaseService.Create(table, createTableScript, false);
            });
        }

        void ReplacePostgresTables(IEnumerable<ObjectID> objectIDs)
        {
            objectIDs.ToList().ForEach(objectID =>
            {
                var createTableScript = TestFilesHelper.CreatePostgresTableScript(objectID);
                var table = ToTableOf(objectID);
                TestPostgresDatabaseService.Create(table, createTableScript, false);
            });
        }

        public void ReCreateTable(ObjectID objectID)
        {
            var createTableScript = TestFilesHelper.CreatePostgresTableScript(objectID);
            var table = ToTableOf(objectID);
            TestPostgresDatabaseService.Create(table, createTableScript, true);
        }

        public static Table ToTableOf(ObjectID objectID)=> Table.Of(objectID.Library.Name.ToLower(), objectID.Name.ToLower());
        //void CreateSchema(Schema targetSchema, IEnumerable<string> tableNames, bool isRecreate)
        //{
        //    var createTableScripts = TestFilesHelper.CreateTableScripts(ActualSchema().Name, tableNames);
        //    TestPostgresDatabaseService.Create(targetSchema, createTableScripts, isRecreate);
        //}

        //void CreateSchema(Schema targetSchema,IEnumerable<ObjectID> objectIDs)
        //{
        //    objectIDs.ToList().ForEach(objectID =>
        //    {
        //        var createTableScript = TestFilesHelper.CreateTableScript(objectID);
        //        var table = targetSchema.CreateTableOf(objectID.Name.ToLower());
        //        TestPostgresDatabaseService.Create(table, createTableScript, true);
        //    });
        //}

        void CreateAssertMssqlViews(Schema expectedSchema, IEnumerable<Table> assertTargetTables)
        {
            var expectedSchemaName = expectedSchema.Name;

            assertTargetTables.ToList().ForEach(assertTargetTable =>
            {
                var expectedTable = assertTargetTable.CreateWithChangingSchema(expectedSchema);

                TestMssqlDatabaseService.CreateViewForDifferenceByExceptTable(
                 TestTarget.ExcludeViewOf(expectedTable), assertTargetTable, expectedTable);
                TestMssqlDatabaseService.CreateViewForDifferenceByExceptTable(
                 TestTarget.IncludeViewOf(expectedTable), expectedTable, assertTargetTable);
            });
        }

        void CreateAssertPostgresViews(Schema expectedSchema, IEnumerable<Table> assertTargetTables)
        {
            var expectedSchemaName = expectedSchema.Name;

            assertTargetTables.ToList().ForEach(assertTargetTable =>
            {
                var expectedTable = assertTargetTable.CreateWithChangingSchema(expectedSchema);

                TestPostgresDatabaseService.CreateViewForDifferenceByExceptTable(
                 TestTarget.ExcludeViewOf(expectedTable), assertTargetTable, expectedTable);
                TestPostgresDatabaseService.CreateViewForDifferenceByExceptTable(
                 TestTarget.IncludeViewOf(expectedTable), expectedTable, assertTargetTable);
            });
        }

        public void InsertSetupTablesToActualTables(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers)
        {
            InsertSetupTablesToActualTables(caseName, setupEntityTestHelpers.Where(t => t.IsCreateTable).Select(t => ToTableOf(t.ObjectID)));
        }

        public void InsertSetupPostgresTablesToActualTables(string caseName, IEnumerable<EntityTestHelper> setupEntityTestHelpers)
        {
            InsertSetupPostgresTablesToActualTables(caseName, setupEntityTestHelpers.Where(t => t.IsCreateTable).Select(t => ToTableOf(t.ObjectID)));
        }

        public void InsertSetupTablesToActualTables(string caseName, IEnumerable<Table> actualTables)
        {
            var setupSchema = SetupSchema(caseName);

            actualTables.ToList().ForEach(actualTable =>
            {
                var setupTable = actualTable.CreateWithChangingSchema(setupSchema);
                TestMssqlDatabaseService.ReplaceWith(setupTable, actualTable);
            });

        }

        public void InsertSetupPostgresTablesToActualTables(string caseName, IEnumerable<Table> actualTables)
        {
            var setupSchema = SetupSchema(caseName);

            actualTables.ToList().ForEach(actualTable =>
            {
                var setupTable = actualTable.CreateWithChangingSchema(setupSchema);
                TestPostgresDatabaseService.ReplaceWith(setupTable, actualTable);
            });

        }

        public void ClearTableData(List<string> actualTableNames)
        {
            var actualSchema = ActualSchema().AddTables(actualTableNames);
            TestPostgresDatabaseService.Clear(actualSchema);
        }

        public void SetupTableData(Table expectedTable)
        {
            Table actualTable = expectedTable.CreateWithChangingSchema(ActualSchema());

            TestPostgresDatabaseService.ReplaceWith(expectedTable, actualTable);
        }

        public void DownloadTestDataBinary(string caseName, List<EntityTestHelper> SetupEntityTestHelpers, List<EntityTestHelper> ExpectedTestHelpers)
        {

            SetupEntityTestHelpers.ForEach(setup => DownloadSetupFileFromTestDB2fori(caseName, setup.ObjectID, setup is NonDDSPhycicalFileHelper));

            ExpectedTestHelpers.ForEach(expected => DownloadExpectedFileFromTestDB2fori(caseName, expected.ObjectID, expected is NonDDSPhycicalFileHelper));
        }

        public string CaseFolder(string caseName) => TestFilesHelper.CaseFolder(caseName);

        public string SetupFolder(string caseName) => TestFilesHelper.SetupFolder(caseName);

        public string ActualFolder(string caseName) => TestFilesHelper.ActualFolder(caseName);

        public string ExpectedFolder(string caseName) => TestFilesHelper.ExpectedFolder(caseName);

        public void DownloadSetupFileFromTestDB2fori(string caseName, ObjectID aObjectID, bool isNonDDSPF)
        {
            DownloadBinaryFileFromTestDB2fori(isNonDDSPF, SetupSchemaName(caseName), aObjectID.Name, FileName(aObjectID), SetupFolder(caseName));
        }

        public void DownloadActualFileFromTestDB2fori(string caseName, ObjectID aObjectID)
        {
            DownloadActualFileFromTestDB2fori(caseName,aObjectID,false);
        }

        void DownloadActualFileFromTestDB2fori(string caseName, ObjectID aObjectID, bool isNonDDSPF)
        {
            DownloadBinaryFileFromTestDB2fori(isNonDDSPF, ActualSchemaName(caseName), aObjectID.Name, FileName(aObjectID), ActualFolder(caseName));
        }

        void DownloadExpectedFileFromTestDB2fori(string caseName, ObjectID aObjectID, bool isNonDDSPF)
        {
            DownloadBinaryFileFromTestDB2fori(isNonDDSPF, ExpectedSchemaName(caseName), aObjectID.Name, FileName(aObjectID), ExpectedFolder(caseName));
        }

        void DownloadBinaryFileFromTestDB2fori(bool isNonDDSPF,string aTestLibraryName, string aTestFileName, string aRealFileFullName, string aSaveFolderName)
        {
            var table = Table.Of(Schema.Of(aTestLibraryName), aTestFileName);

            var fileLength = DB2foriOperator.GetFileLength(table);
            var saveFileName = $"{aRealFileFullName}.{fileLength}";

            if (!isNonDDSPF)
            {
                var dt = DB2foriOperator.FillSchema(table);
                if (dt.Columns.Count == 1 && dt.Columns[0].DataType == typeof(byte[]))
                {
                    //r.ItemArray[0] == null ? string.Empty : (string)r.ItemArray[0]
                    var bytesList = dt.Rows.Cast<DataRow>().Select(r => r.ItemArray[0] == null ? Array.Empty<byte>(): (byte[])r.ItemArray[0]);
                    TestFilesHelper.WriteAllBytes(bytesList, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
                }
                else
                {
                    //var bytesList = TestDB2foriOperatedBySQL.GetHexStrings(table).Select(hexString => CCSID930.ToBytesFrom(hexString));
                    //TestFilesHelper.WriteAllBytes(bytesList, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
                    DB2foriOperator.DownloadFile(aTestLibraryName, aTestFileName, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
                }
            }
            else
            {
                DB2foriOperator.DownloadFile(aTestLibraryName, aTestFileName, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
            }
        }

        //IEnumerable<byte[]> GetBytes(Table aTable)
        //{
        //    var dt = TestDB2foriOperatedBySQL.FillSchema(aTable);
        //    if (dt.Columns.Count == 1 && dt.Columns[0].DataType == typeof(byte[]))
        //    {
        //        return dt.AsEnumerable().Select(r => r.Field<byte[]>(0)??Array.Empty<byte>());
        //    }

        //    return TestDB2foriOperatedBySQL.GetHexStrings(aTable).Select(hexString => CCSID930.ToBytesFrom(hexString));
        //}

        public void UploadSetupFileToTestDB2fori(string caseName, string aTestLibraryName, ObjectID aObjectID, int fileLength)
        {
            UploadSetupFileToTestDB2fori(caseName, aTestLibraryName, aObjectID.Name, FileName(aObjectID), fileLength);
        }

        void UploadSetupFileToTestDB2fori(string caseName, string aTestLibraryName, string aTestFileName, string aRealFileFullName, int fileLength)
        {
            var saveFolderName = SetupFolder(caseName);
            UploadBinaryFileToTestDB2fori(aTestLibraryName, aTestFileName, aRealFileFullName, fileLength, saveFolderName);
        }

        void UploadExpectedFileToTestDB2fori(string caseName, string aTestLibraryName, string aTestFileName, string aRealFileFullName, int fileLength)
        {
            var saveFolderName = ExpectedFolder(caseName);
            UploadBinaryFileToTestDB2fori(aTestLibraryName, aTestFileName, aRealFileFullName, fileLength, saveFolderName);
        }

        void UploadBinaryFileToTestDB2fori(string aTestLibraryName, string aTestFileName, string aRealFileFullName, int fileLength, string aSaveFolderName)
        {
            //var fileLength = TestDB2Repository.GetFileLength(aTestLibraryName, aTestFileName);
            var saveFileName = $"{aRealFileFullName}.{fileLength}";

            try
            {
                DB2foriOperator.UploadFile(aTestLibraryName, aTestFileName, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
            }
            catch (Exception ex)//FileName!=MemberName
            {
                Console.WriteLine(ex.ToString());
                //var bytesList = TestDB2Repository.GetBytes(aTestLibraryName, aTestFileName);
                //WriteAllBytes(bytesList, aSaveFolderName, saveFileName, ExtensionOfAS400BinaryFile);
            }

        }
        public void DownloadFile(string aTargetLibraryName, string aTargetFileName, string aSaveFolderPath, string aSaveFileName, string aSaveFileExtension)
        {
            DB2foriOperator.DownloadFile(aTargetLibraryName, aTargetFileName, aSaveFolderPath, aSaveFileName, aSaveFileExtension);
        }

        public void UploadFile(string aTargetLibraryName, string aTargetFileName, string aSaveFolderPath, string aSaveFileName, string aSaveFileExtension)
        {
            DB2foriOperator.UploadFile(aTargetLibraryName, aTargetFileName, aSaveFolderPath, aSaveFileName, aSaveFileExtension);
        }

        public IEnumerable<string> ReadLinesFromSetupFolder(string caseName, string fileName) => File.ReadLines(Path.Combine(SetupFolder(caseName), fileName));

        public IEnumerable<string> ReadLinesOfTxtFileFromSetupFolder(string caseName, string fileName) => File.ReadLines(Path.Combine(SetupFolder(caseName), fileName)).Skip(1).Select(l => l.Split(',')[1].Replace("\"", string.Empty));

        public IEnumerable<string[]> ReadLinesOfPFFileFromSetupFolder(string caseName, string fileName) => File.ReadLines(Path.Combine(SetupFolder(caseName), fileName)).Skip(1).Select(l => l.Split(',').Select(c => c.Replace("\"", string.Empty)).ToArray());

        public void WriteAllLinesToActualFolder(string caseName, string fileName, IEnumerable<(int, IEnumerable<string>)> AllPageLines)
        {
            AllPageLines.ToList().ForEach(p =>
            {
                WriteAllLinesToActualFolder(caseName, $"{fileName}.{p.Item1}.txt", p.Item2);
            });
        }

        public void WriteAllLinesToActualFolder(string caseName, string fileName, IEnumerable<string> actual)
        {
            var actualFolder = ActualFolder(caseName);
            Directory.CreateDirectory(actualFolder);
            File.WriteAllLines(Path.Combine(actualFolder, fileName), actual);
        }

        public IEnumerable<byte[]> ReadBytesFromSetupFolder(string caseName, ObjectID objectID)
        {
            return TestFilesHelper.ReadBytesFromSetupFolder(caseName, objectID);
        }

        //public IEnumerable<byte[]> ReadBytesFromSetupFolder(string caseName, string aFileName)
        //{
        //    return TestFilesHelper.ReadBytesFromSetupFolder(caseName, aFileName);
        //}

        public IEnumerable<byte[]> ReadBytesFromActualFolder(string caseName, ObjectID objectID)
        {
            return TestFilesHelper.ReadBytesFromActualFolder(caseName, objectID);
        }

        public IEnumerable<byte[]> ReadBytesFromExpectedFolder(string caseName, ObjectID objectID)
        {
            return TestFilesHelper.ReadBytesFromExpectedFolder(caseName, objectID);
        }

        //public IEnumerable<byte[]> ReadBytesFromExpectedFolder(string caseName, string aFileName)
        //{
        //    return TestFilesHelper.ReadBytesFromExpectedFolder(caseName, aFileName);
        //}


        public void WriteAllBytesToActualFolder(string aCaseName, IEnumerable<byte[]> aBytes, ObjectID objectID)
        {
            TestFilesHelper.WriteAllBytesToActualFolder(aCaseName, aBytes, objectID);
        }

        public void WriteAllCsvLinesToActualFolder(string aCaseName, IEnumerable<string> csvLines, ObjectID objectID)
        {
            TestFilesHelper.WriteAllCsvLinesToActualFolder(aCaseName, csvLines, objectID);
        }

        //public void WriteAllBytesToActualFolder(string caseName, IEnumerable<byte[]> aBytes, string aFileName)
        //{
        //    TestFilesHelper.WriteAllBytesToActualFolder(aBytes, caseName, aFileName);
        //}

        //public void WritePdfToActalFolder(string caseName, Printer printer, string aFileName)
        //{
        //    var pdfPath = Path.Combine(ActualFolder(caseName), $"{aFileName}.pdf");
        //    PdfFileWriterForQprint.Output(pdfPath, printer);
        //}

        //public void WriteAllBytes(IEnumerable<byte[]> aBytes,string aFolderPath, string aFileName, string aFileExtension)
        //{
        //    var bytes = new List<byte>();
        //    aBytes.ToList().ForEach(a => {
        //        bytes.AddRange(a);
        //        //bytes.Add(CP290Character.LineFeed.CP290ByteOfCharacter);
        //    });

        //    Directory.CreateDirectory(aFolderPath);

        //    File.WriteAllBytes(Path.Combine(aFolderPath, $"{aFileName}.{aFileExtension}"), bytes.ToArray());

        //}

        public IEnumerable<string> ReadLinesOfTxtFileFromExpectedFolder(string caseName, string fileName) => File.ReadLines(Path.Combine(ExpectedFolder(caseName), fileName)).Skip(1).Select(l => l.Split(',')[1].Replace("\"", string.Empty));

        //void CreateTableLike(Table aSourceTable, Schema aTargetSchema)
        //{
        //    TestPostgresDatabaseService.CreateTableLike(aSourceTable, aTargetSchema);
        //}
        //public void ReCreateSetupTable(string aTableName)
        //{
        //    TestPostgresDatabaseService.ReCreateTableLike(Table.Of(OriginalSchema, aTableName), SetupSchema);
        //}

        //public void ReCreateExpectedTable(string aTableName)
        //{
        //    TestPostgresDatabaseService.ReCreateTableLike(Table.Of(OriginalSchema,aTableName), ExpectedSchema);
        //}


        public long DifferenceCountOfExceptPostgresView(string caseName, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            return DifferenceCountOfExceptPostgresView(removeDb2ExpectedEntityTestHelpers.Select(o => ExpectedSchema(caseName).CreateTableOf(o.ObjectID.Name)));
        }

        public long DifferenceCountOfExceptView(string caseName, IEnumerable<EntityTestHelper> expectedEntityTestHelpers)
        {
            var removeDb2ExpectedEntityTestHelpers = expectedEntityTestHelpers.Where(t => t.IsCreateTable);
            return DifferenceCountOfExceptView(removeDb2ExpectedEntityTestHelpers.Select(o => ExpectedSchema(caseName).CreateTableOf(o.ObjectID.Name)));
        }

        public long DifferenceCountOfExceptView(IEnumerable<Table> tables)
        {

            var differentViews = new List<View>();

            tables.ToList().ForEach(table =>
            {
                var excludeView = TestTarget.ExcludeViewOf(table);
                differentViews.Add(excludeView);
                var includeView = TestTarget.IncludeViewOf(table);
                differentViews.Add(includeView);
            }
            );

            return DifferenceCountOfExceptView(differentViews);

        }

        public long DifferenceCountOfExceptPostgresView(IEnumerable<Table> tables)
        {

            var differentViews = new List<View>();

            tables.ToList().ForEach(table =>
            {
                var excludeView = TestTarget.ExcludeViewOf(table);
                differentViews.Add(excludeView);
                var includeView = TestTarget.IncludeViewOf(table);
                differentViews.Add(includeView);
            }
            );

            return DifferenceCountOfExceptPostgresView(differentViews);

        }

        public long DifferenceCountOfExceptView(IEnumerable<View> differentViews)
        {
            long differenceCount = 0;

            differentViews.ToList().ForEach(differentView =>
            {
                var viewCount = TestMssqlDatabaseService.Count(differentView);
                differenceCount += viewCount;
                Debug.Write($"{differentView.FullName}:{viewCount}");
            }
            );

            return differenceCount;

        }

        public long DifferenceCountOfExceptPostgresView(IEnumerable<View> differentViews)
        {
            long differenceCount = 0;

            differentViews.ToList().ForEach(differentView =>
            {
                var viewCount = TestPostgresDatabaseService.Count(differentView);
                differenceCount += viewCount;
                Debug.Write($"{differentView.FullName}:{viewCount}");
            }
            );

            return differenceCount;

        }


        //void TestForConversion(DBAssertionTestCase aDCSTestCase, Action stu)
        //{
        //    TestService.SetupData(aDCSTestCase.dcsTestCaseDifinitionOfSetup);
        //    TestService.ExectueWithLoggingTime(aDCSTestCase.ExecuteLogFilePath(), stu);
        //    //while (true)
        //    //{
        //    //    if (!stu.IsAlive)
        //    //    {
        //    //        assertHandle.Start();
        //    //        assertHandle.Join();
        //    //        break;
        //    //    }
        //    //}
        //    //assertHandle.Invoke();
        //    var diffrenceRecordCount =
        //    TestService.CountOfDifferenceSummaryView(aDCSTestCase.dcsTestCaseDifinitionOfExpected);
        //    Assert.Equal(0, diffrenceRecordCount);
        //}

    }

}

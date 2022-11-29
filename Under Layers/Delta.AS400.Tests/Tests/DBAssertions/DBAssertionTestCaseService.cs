using Delta.AS400.Libraries;
using Delta.RelationalDatabases.Postgres;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Delta.RelationalDatabases;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCaseService
    {
        readonly Library MainSchemaLibrary;
        string NameSpaceOfMainChemaLibrary=> $"Delta.AS400.{MainSchemaLibrary.Partition.Name.ToPascalCase()}.{MainSchemaLibrary.Name.ToPascalCase()}";
        string DatabaseScriptsFolder => Path.Combine(@"C:\Delta", "Solutions",
            NameSpaceOfMainChemaLibrary,
            "Infrastructure Layer",
            $"{NameSpaceOfMainChemaLibrary}.Persistence",
            "DatabaseScripts");

        IDatabaseOperatedBySQL TestDatabase;

        PostgresDatabaseService DatabaseService;
        //readonly ISchemaRepository schemaRepository;
        //readonly ITableRepository tableRepository;
        //readonly IViewRepository viewRepository;
        PostgresSchemaService SchemaService;
        PostgresObjectOfSchemaService ObjectOfSchemaRepository;
        //PostgresViewRepository ViewService;

        //public DcsTestCaseService(
        //    ISchemaRepository schemaRepository,
        //    ITableRepository tableRepository,
        //    IViewRepository viewRepository)
        //{
        //    this.schemaRepository = schemaRepository;
        //    this.tableRepository = tableRepository;
        //    this.viewRepository = viewRepository;
        //}

        public DBAssertionTestCaseService(string aDatabaseName,IDatabaseOperatedBySQL aTestDatabase, Library aMainSchemaLibrary)
        {
            //DatabaseService = PostgresDatabaseService.OfLocal(aDatabaseName);

            MainSchemaLibrary = aMainSchemaLibrary;
            TestDatabase = aTestDatabase;
            //SchemaService = new PostgresSchemaRepository(aTestDatabase);
            ObjectOfSchemaRepository = PostgresObjectOfSchemaService.Of(aTestDatabase);
            //ViewService = new PostgresViewRepository(aTestDatabase);
        }

        public void RecreateDatabase(IEnumerable<Library> libraries)
        {
            libraries.ToList().ForEach(library =>
            {
                DirectoryInfo di = new DirectoryInfo(Path.Combine(DatabaseScriptsFolder, library.Name));
                var initializeScripts = LoadContentOfFiles(di, filePath => !filePath.Name.StartsWith("#unused#"));
                //SchemaService.Recreate(library.ToSchema, initializeScripts);
            });
        }

        static List<string> LoadContentOfFiles(DirectoryInfo targetFolder, Func<FileInfo, bool> isTargetFilePath)
        {
            List<FileInfo> Files = targetFolder.GetFiles().OfType<FileInfo>().ToList();
            List<string> Scripts = new List<string>();
            Files.Where(scriptFilePath => scriptFilePath.Exists).Where(isTargetFilePath).ToList().ForEach(scriptFile =>
            {
                Scripts.Add(File.ReadAllText(scriptFile.FullName).ToString());
            });
            return Scripts;
        }

        void createDB()
        {
            //            --Database: h1qkc

            //-- DROP DATABASE h1qkc;
            //        CREATE DATABASE h1qkc
            //WITH
            //OWNER = postgres
            //ENCODING = 'UTF8'
            //LC_COLLATE = 'Japanese_Japan.932'
            //LC_CTYPE = 'Japanese_Japan.932'
            //TABLESPACE = pg_default
            //CONNECTION LIMIT = -1;
        }

        //public virtual void TestForConversion(int caseNo)
        //{
        //    this.caseNo = caseNo;
        //    //beforeExecute(caseNo);
        //    var currentDcsTestCase = GetDcsTestCase(caseNo);
        //    TestForConversion(currentDcsTestCase,
        //       new Thread(() =>
        //       {
        //           Execute(caseNo);
        //       }),
        //       new Thread(() =>
        //       {
        //           Validation(caseNo);
        //       }));

        //    Assert.Equal(0, diffrenceRecordCount);

        //}

        //public void TestForConversion(DBAssertionTestCase aDCSTestCase, Action stu)
        //{
        //    SetupData(aDCSTestCase.dcsTestCaseDifinitionOfSetup);
        //    ExectueWithLoggingTime(aDCSTestCase.ExecuteLogFilePath(), stu);
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
        //    CountOfDifferenceSummaryView(aDCSTestCase.dcsTestCaseDifinitionOfExpected);
        //    Assert.Equal(0, diffrenceRecordCount);
        //}

        public void SetupData(DBAssertionTestCaseDifinition setup)
        {
            foreach (var setupSchema in setup.schemas)
            {
                var actualSchema = setupSchema.DbSchema;
                setupSchema.Tables.ForEach(table =>
                {
                    var source_table = table;
                    var actual_table = table.CreateWithChangingSchema(actualSchema);
                    ObjectOfSchemaRepository.ReplaceWith(source_table,actual_table);
                });
            }
        }

        public Stopwatch stopwatch = new Stopwatch();
        public void ExectueWithLoggingTime(string executeLogFilePath, Action stu)
        {
            TextWriter fileWriter = new StreamWriter(executeLogFilePath, true);

            stopwatch.Reset();
            stopwatch.Start();
            try
            {
                //stu.Start();
                //stu.Join();
                stu.Invoke();
                fileWriter.Write("end by return");
                //} catch (ReturnByLastRecord ex) {
                fileWriter.Write("end by ReturnByLastRecord");
            }
            catch (Exception)
            {
                fileWriter.Write("end by Exception");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                fileWriter.Write(string.Format(",start:%s,execution time(mms) :%d%s", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ZZZ"), stopwatch.Elapsed, "\r\n"));
            }
        }

        //public int CountOfDifferenceSummaryView(DBAssertionTestCaseDifinition expected)
        //{
        //    var count = 0;
        //    foreach (var expectedSchema in expected.schemas)
        //    {
        //        var expectedSchemaName = expectedSchema.Name;
        //        string view_name = expectedSchemaName + ".diff_counts";
        //        string query = "select sum(diff_count) as sum_of_diff_count from " + view_name;
        //        count += ObjectOfSchemaRepository.ExecuteNonQuery.ReturnIntQuery(query);
        //    }
        //    return count;
        //}

        //public void SetupInfra(DcsTestCaseList aDcsTestCaseList)
        //{
        //    aDcsTestCaseList.items.ForEach(dcsTestCase => SetupInfra(dcsTestCase));
        //}

        //public void SetupInfra(DBAssertionTestCase dcsTestCase)
        //{

        //    CreateTestSchemaInfra(dcsTestCase.dcsTestCaseDifinitionOfSetup);

        //    CreateExpectedSchemaInfra(dcsTestCase.dcsTestCaseDifinitionOfExpected);

        //}

        //void CreateExpectedSchemaInfra(DBAssertionTestCaseDifinition expected)
        //{

        //    CreateTestSchemaInfra(expected);

        //    CreateDifferenceView(expected);

        //}


        //void CreateTestSchemaInfra(DBAssertionTestCaseDifinition aDcsTestCaseDifinition)
        //{

        //    foreach (var schema in aDcsTestCaseDifinition.schemas)
        //    {
        //        SchemaService.Save(schema);

        //        CreateTableByLikeActualTable(schema);

        //        ReplaceByCSV(aDcsTestCaseDifinition.CsvFileContainedFolderPath(), schema);
        //    }

        //}

        //void CreateTableByLikeActualTable(DBAssertionTestCaseSchema schema)
        //{
        //    var sourceSchema = schema.DbSchema;
        //    var targetSchema =Schema.Of(schema.Name);
        //    List<string> tableNames = new List<string>();
        //    schema.tables.ForEach(table => { tableNames.Add(table.Name); });
        //    ObjectOfSchemaRepository.CreateTableByLike(sourceSchema, targetSchema, tableNames);
        //}

        //public void binToCsv()//TODO
        //{

        //}

        void ReplaceByCSV(string csvFileContainedFolderPath, DBAssertionTestCaseSchema aSchema)
        {

            var dbSchemaNameOfActual = aSchema.DbSchema.Name;
            aSchema.Tables.ForEach(table =>
            {
                ObjectOfSchemaRepository.Truncate(table);
                ((PostgresObjectOfSchemaService)ObjectOfSchemaRepository).FindColumns(table);
                var fileName = table.Name + ".csv";
                if (!File.Exists(csvFileContainedFolderPath + "\\" + fileName))
                {
                    fileName = dbSchemaNameOfActual + "_" + fileName;
                }
                string csv_file_path = csvFileContainedFolderPath + "\\" + fileName;
                ((PostgresObjectOfSchemaService)ObjectOfSchemaRepository).CopyByCsv(csv_file_path, table,table.JoinedColumnNames()
                    );
            });
        }

        void CreateDifferenceView(DBAssertionTestCaseDifinition expected)
        {

            if (File.Exists(expected.ColumnExcludingFilePath()))
            {
                List<string> excludColumns = new List<string>();

                excludColumns = File.ReadAllLines(expected.ColumnExcludingFilePath()).ToList();

                foreach (var exCol in excludColumns)
                {
                    var exColParts = exCol.Split(".");
                    var schemaName = exColParts[0];
                    var tableName = exColParts[1];
                    var columnName = exColParts[2];
                    var tab = expected.schemas.Where(s => s.DbSchema.Name.Equals(schemaName)).SelectMany(s => s.Tables).FirstOrDefault(t => t.Name.Equals(tableName));
                    if (tab != null)
                    {
                        var dcsTab = (DBAssertionTestCaseTable)tab;
                        dcsTab.AddExceptColumn(columnName);
                    }
                }
            }


            foreach (var expectedSchema in expected.schemas)
            {

                var actualSchema = expectedSchema.DbSchema;
                var expectedSchemaName = expectedSchema.Name;

                expectedSchema.Tables.ForEach(table =>
                {

                    Table actualTable = table.CreateWithChangingSchema(actualSchema);
                    Table expectedTable = table;

                    //DatabaseService.CreateViewForDifferenceByExceptTable(
                    //    View.ExcludeViewOf(expectedTable), actualTable, expectedTable);
                    //DatabaseService.CreateViewForDifferenceByExceptTable(
                    //    View.IncludeViewOf(expectedTable), expectedTable, actualTable);
                });

                //CreateDifferenceSummaryView(expectedSchema);

            }

        }

        //void CreateDifferenceSummaryView(Schema expectedSchema)
        //{

        //    var expectedSchemaName = expectedSchema.Name;

        //    string view_name = expectedSchemaName + ".diff_counts";
        //    string view_body = string.Empty;
        //    expectedSchema.tables.ForEach(table =>
        //    {
        //        var fullTableNameOfextra = expectedSchemaName + "." + table.Name + "_extra";
        //        var fullTableNameOfintra = expectedSchemaName + "." + table.Name + "_intra";
        //        view_body += "SELECT '" + fullTableNameOfextra + "' as view_name,COUNT(*) as diff_count FROM "
        //            + fullTableNameOfextra + " UNION ALL " + "SELECT '" + fullTableNameOfintra
        //            + "' as tablename,COUNT(*) as diff_count FROM " + fullTableNameOfintra;
        //        if (expectedSchema.tables.IndexOf(table) < expectedSchema.tables.Count - 1)
        //        {
        //            view_body += " UNION ALL ";
        //        }
        //    });

        //    ViewService.CreateView(view_name, view_body);

        //}

        //public void TestForConversionSqlServer(IDatabaseRepository databaseRepository, DcsTestCase aDCSTestCase, dynamic stu, dynamic assertHandle)
        //{
        //    SetupDataSqlServer(aDCSTestCase.dcsTestCaseDifinitionOfExpected, databaseRepository, aDCSTestCase);
        //    ExectueWithLoggingTime(aDCSTestCase.ExecuteLogFilePath(),
        //    stu);
        //    assertHandle.run();
        //}

        //public void SetupDataSqlServer(DcsTestCaseDifinition expected, IDatabaseRepository databaseRepository, DcsTestCase aDCSTestCase)
        //{
        //    //attach DB 
        //    //TODO:
        //    /*  var rb = ResourceBundle.getBundle(SqlServerByExceptAllTest.BUNDLE_BASE_NAME);
        //      var dbName = rb.getString(SqlServerByExceptAllTest.SQLSERVER_DBNAME_KEY);
        //      var mdfFileContainedFolderPath = aDCSTestCase.dcsTestCaseDifinitionOfSetup.MdfFileContainedFolderPath();
        //      var mdfFilePatj = mdfFileContainedFolderPath + "\\" + dbName + SqlServerByExceptAllTest.MDF_PREFIX;
        //      var ldfFilePath = mdfFileContainedFolderPath + "\\" + dbName + SqlServerByExceptAllTest.LDF_PREFIX;
        //      databaseRepository.AttachDb(dbName, mdfFilePatj, ldfFilePath);
        //      */
        //    //truncate tagret table
        //    foreach (var expectedSchemal in expected.schemas)
        //    {
        //        //TODO:
        //        /*
        //        var actualSchemaName = expectedSchemal.DbSchemaName();
        //        expectedSchemal.tables.ForEach(table =>
        //        {
        //              var actual_table_name = dbName + "." + actualSchemaName + "." + table.name;
        //              databaseRepository.TruncateTable(actual_table_name);
        //        });
        //        */
        //    }
        //}

        //public int CountOfDifferenceSummaryViewSqlServer(DcsTestCaseDifinition expected)
        //{
        //    var count = 0;
        //    //TODO:
        //    /* var rb = ResourceBundle.getBundle(SqlServerByExceptAllTest.BUNDLE_BASE_NAME);
        //     var dbName = rb.getString(SqlServerByExceptAllTest.SQLSERVER_DBNAME_KEY);
        //     foreach (var expectedSchema in expected.schemas)
        //     {
        //         var expectedSchemaName = expectedSchema.name;
        //         String view_name = expectedSchemaName + ".diff_counts";
        //         String query = "select sum(diff_count) as sum_of_diff_count from " + dbName + "." + view_name;
        //         viewRepository.ReturnIntQuery(query);

        //     }
        //     */
        //    return count;
        //}

        //public Action GetStu()
        //{
        //    return null;
        //}

        //public virtual void Execute(int caseNo)
        //{

        //    var stu = GetStu();
        //    //    if(stu instanceof ReportOutputter) {
        //    //      var filePath=getDcsTestCase(caseNo).actualFolderPath().resolve(programId() + "ReportData.xml").toString();
        //    //      ((ReportOutputter)stu).setReportDataRepository(new ReportDataRepository(filePath));
        //    //    }

        //    stu.Invoke();

        //}

        //private int diffrenceRecordCount;
        //public void Validation(int caseNo)
        //{

        //    DBAssertionTestCase dcsTestCase = GetDcsTestCase(caseNo);

        //    diffrenceRecordCount =
        //     CountOfDifferenceSummaryView(dcsTestCase.dcsTestCaseDifinitionOfExpected);
        //    //    var stu = getStu();
        //    //    if(stu instanceof ReportOutputter) {
        //    //      var reportFileName=programId() + "ReportData.xml";
        //    //      var actualReportFilePath=dcsTestCase.actualFolderPath().resolve(reportFileName);
        //    //      var expectedReportFilePath=dcsTestCase.expectedFolderPath().resolve(reportFileName);
        //    //      String actualReportFileContents=getContents(actualReportFilePath);
        //    //      String expectedReportFileContents=getContents(expectedReportFilePath);
        //    //      assertThat(actualReportFileContents, is(expectedReportFileContents));
        //    //    }
        //}

    }


}

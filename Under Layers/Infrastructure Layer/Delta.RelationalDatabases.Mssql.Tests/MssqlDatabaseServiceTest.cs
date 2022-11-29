using Delta.TrimSystem.MstControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace Delta.RelationalDatabases.Mssql.Tests
{
    public class MssqlDatabaseServiceTest
    {
        [Fact]
        public void ColumnNamesTest1()
        {
            string DatabaseName = "h1iid";
            DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("(local)", "SQLEXPRESs", DatabaseName);
            var connectionString= LocalTestMssqlConnectionStringBuilder.ConnectionString;

            var stu = Honsha01IidlibMssqlDbContext.Of(connectionString);
            var x = stu.MstSettypecodes.Find(1);
            //var targetTable = Table.Of(Schema.Of("IIDLIB"), "ORDDFL");

            //var findColumns = stu.FindColumns(targetTable).ColumnNames;

            ////HONSHA01.IIDLIB.ORDDFL.156
            Assert.Equal("", x.ToString());
        }

        [Fact]
        public void ColumnNamesTest2()
        {
            string DatabaseName = "TRIM_SYSTEM";
            DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("192.168.100.235", DatabaseName);
            var connectionString = LocalTestMssqlConnectionStringBuilder.ConnectionString;

            var stu = Honsha01IidlibMssqlDbContext.Of(connectionString);
            var x = stu.MstSettypecodes.Find(1);
            //var targetTable = Table.Of(Schema.Of("IIDLIB"), "ORDDFL");

            //var findColumns = stu.FindColumns(targetTable).ColumnNames;

            ////HONSHA01.IIDLIB.ORDDFL.156
            Assert.Equal(1, x.Id);
        }

        [Fact]
        public void CopyByCsv()
        {
            DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder
            = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("(local)", "SQLEXPRESs", "TRIM_SYSTEM");

            var database = MssqlOperatedBySQLWithSqlClient.Of(LocalTestMssqlConnectionStringBuilder);
            var stu = MssqlObjectOfSchemaService.Of(database);

            stu.CopyByCsv(@"C:\Delta\TestDatas\HONSHA01.IIDLIB.PIID074s.Piid074Service.1\Setup\HONSHA01.IIDLIB.CARSET.csv", 
                Table.Of(Schema.Of("PIID074S1"), "CARSET"),"");

        }

        //[Fact]
        //public void Truncate()
        //{

        // DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder
        //    = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("(local)", "SQLEXPRESs", "TRIM_SYSTEM");
        //Assert.Equal(1, x.Id);
        //}
    }
}

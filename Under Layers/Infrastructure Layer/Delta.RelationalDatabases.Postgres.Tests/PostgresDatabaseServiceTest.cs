using System;
using System.Data.Common;
using Xunit;
using System.Linq;

namespace Delta.RelationalDatabases.Postgres.Tests
{
    public class PostgresDatabaseServiceTest
    {
        //[Fact]
        //public void Test1()
        //{
        //    string DatabaseName = "k1sale";
        //    DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBilderOfLocalTest(DatabaseName);
        //    var p = PostgresDatabaseService.Of(LocalTestPostgresConnectionStringBuilder);
        //    var x = p.Count(Table.Of(Schema.Of("salelib"), "urihfl"));
        //}

        //[Fact]
        //public void ColumnNamesTest1()
        //{
        //    string DatabaseName = "h1iid";
        //    DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBilderOfLocalTest(DatabaseName);
        //    var stu = PostgresDatabaseService.Of(LocalTestPostgresConnectionStringBuilder);

        //    var targetTable = Table.Of(Schema.Of("IIDLIB"), "ASP16Z");

        //    var findColumns = stu.FindColumns(targetTable).ColumnNames;

        //    //HONSHA01.IIDLIB.ORDDFL.156
        //    Assert.Equal(165, findColumns.Count());
        //}

        [Fact]
        public void ColumnNamesTest1()
        {
            string DatabaseName = "h1iid";
            DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBuilderOfLocalTest(DatabaseName);
            var stu = PostgresDatabaseService.Of(LocalTestPostgresConnectionStringBuilder);

            var targetTable = Table.Of(Schema.Of("IIDLIB"), "ORDDFL");

            var findColumns = stu.FindColumns(targetTable).ColumnNames;

            //HONSHA01.IIDLIB.ORDDFL.156
            Assert.Equal(165, findColumns.Count());
        }
    }
}

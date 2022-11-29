using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.RelationalDatabases.Postgres
{
    public class PostgresObjectOfSchemaServiceTest
    {
        [Fact]
        public void ColumnNamesTest1()
        {
            string DatabaseName = "h1iid";
            DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBuilderOfLocalTest(DatabaseName);
            var stu = PostgresObjectOfSchemaService.Of(PostgresOperatedBySQLWithNpgsql.Of(LocalTestPostgresConnectionStringBuilder));

            var targetTable = Table.Of(Schema.Of("IIDLIB"), "CARSET");

            stu.ReplaceDataByCsv(@"C:\Delta\TestDatas\HONSHA01.IIDLIB.CIID015s.Ciid015Service.1\Setup\HONSHA01.IIDLIB.CARSET.csv", targetTable);

        }
    }
}

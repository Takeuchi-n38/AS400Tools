using Delta.RelationalDatabases;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCaseSchema : Schema
    {


        public DBAssertionTestCaseSchema(string aName) : base(aName)
        {

        }

        public static string CreateSchemaName(string schemaPrefix, string tableFullName)
        {
            var partsOfTableFullName = tableFullName.Split(".");
            var dbSchemaName = partsOfTableFullName[0];
            //var tableName=partsOfTableFullName[1];
            var schemaName = schemaPrefix + "_" + dbSchemaName;
            return schemaName;
        }

        public override Schema AddTable(string tableName)
        {
            return AddTable(new DBAssertionTestCaseTable(this, tableName));
        }

        public string DbSchemaName => Name.Substring(Name.LastIndexOf("_") + 1);

        public Schema DbSchema => Schema.Of(DbSchemaName);

    }

}

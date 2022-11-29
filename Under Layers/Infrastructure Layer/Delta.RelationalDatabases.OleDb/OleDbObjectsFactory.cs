using System.Data.Common;
using System.Data.OleDb;

namespace Delta.RelationalDatabases.Db2fori
{
    public class OleDbObjectsFactory : DbObjectsFactory
    {
        public override DbConnection CreateConnection()
        {
            return new OleDbConnection(ConnectionString);
        }

        public override DbCommand CreateCommand()
        {
            return new OleDbCommand();
        }

        public override DbDataAdapter CreateDataAdapter(string commandText)
        {
            return new OleDbDataAdapter(commandText, ConnectionString);
        }

        public OleDbObjectsFactory(string aConnectionString) : base(aConnectionString)
        {
        }
    }

}

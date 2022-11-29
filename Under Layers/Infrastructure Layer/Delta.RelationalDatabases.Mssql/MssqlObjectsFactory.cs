using System.Data.Common;
using Microsoft.Data.SqlClient;


namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlObjectsFactory : DbObjectsFactory
    {
        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbDataAdapter CreateDataAdapter(string commandText)
        {
            return new SqlDataAdapter(commandText, ConnectionString);
        }

        MssqlObjectsFactory(string aConnectionString) : base(aConnectionString)
        {
        }

        public static MssqlObjectsFactory Of(string aConnectionString)
        {
            return new MssqlObjectsFactory(aConnectionString);
        }
    }
}

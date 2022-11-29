using System.Data.Common;
using Npgsql;


namespace Delta.RelationalDatabases.Postgres
{
    public class PostgresObjectsFactory : DbObjectsFactory
    {
        public override DbConnection CreateConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public override DbCommand CreateCommand()
        {
            return new NpgsqlCommand();
        }

        public override DbDataAdapter CreateDataAdapter(string commandText)
        {
            return new NpgsqlDataAdapter(commandText, ConnectionString);
        }

        PostgresObjectsFactory(string aConnectionString) : base(aConnectionString)
        {
        }

        public static PostgresObjectsFactory Of(string aConnectionString)
        {
            return new PostgresObjectsFactory(aConnectionString);
        }
    }
}

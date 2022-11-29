using System.Data.Common;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlOperatedBySQLWithSqlClient : DatabaseOperatedBySQL
    {
        MssqlOperatedBySQLWithSqlClient(string aConnectionString) : base(MssqlObjectsFactory.Of(aConnectionString))
        {
        }

        public static MssqlOperatedBySQLWithSqlClient Of(DbConnectionStringBuilder aDbConnectionStringBuilder)
        {
            return new MssqlOperatedBySQLWithSqlClient(aDbConnectionStringBuilder.ConnectionString);
        }

        //int IDatabaseOperatedBySQL.ExecuteNonQuery(IEnumerable<string> commandTexts)
        //{
        //    int affected = 0;
        //    using (var cn = new NpgsqlConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new NpgsqlCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            commandTexts.ToList().ForEach(commandText =>
        //            {
        //                command.CommandText = commandText;
        //                affected = command.ExecuteNonQuery();
        //            });

        //        }
        //        cn.Close();
        //    }

        //    return affected;
        //}

        //object IDatabaseOperatedBySQL.ExecuteObject(string scalarQuery)
        //{
        //    object result = null;
        //    using (var cn = new NpgsqlConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new NpgsqlCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            command.CommandText = scalarQuery;
        //            result = command.ExecuteScalar();
        //        }
        //        cn.Close();
        //    }

        //    return result;
        //}

        //T IDatabaseOperatedBySQL.ExecuteReadFirst<T>(string sqlQuery, Func<DbDataReader, T> action) 
        //{
        //    T result;
        //    using (var cn = new NpgsqlConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new NpgsqlCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            command.CommandText = sqlQuery;
        //            using (var reader = command.ExecuteReader())
        //            {
        //                result = reader.HasRows ? action(reader) : null;
        //                reader.Close();
        //            }

        //        }
        //        cn.Close();
        //    }
        //    return result;
        //}

        //List<T> IDatabaseOperatedBySQL.ExecuteReader<T>(string sqlQuery, Func<DbDataReader, T> action)
        //{
        //    var returns = new List<T>();
        //    using (var cn = new NpgsqlConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new NpgsqlCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            command.CommandText = sqlQuery;

        //            using (var reader = command.ExecuteReader())
        //            { 
        //                while (reader.Read())
        //                {
        //                    returns.Add(action(reader));
        //                }
        //                reader.Close();
        //            }
        //        }
        //        cn.Close();
        //    }
        //    return returns;
        //}

        //List<T> IDatabaseOperatedBySQL.RawSqlQuerySingleColumn<T>(string sqlQuery)
        //{
        //    return ((IDatabaseOperatedBySQL)this).ExecuteReader<T>(sqlQuery, dr => dr.GetFieldValue<T>(0));
        //}

        //DataTable IDatabaseOperatedBySQL.Fill(string commandText)
        //{
        //    var dt = new DataTable();
        //    using (var adp = new NpgsqlDataAdapter(commandText, ConnectionString))
        //    {
        //        adp.Fill(dt);
        //    }
        //    return dt;
        //}

        //DataTable IDatabaseOperatedBySQL.FillSchema(string commandText)
        //{
        //    var dt = new DataTable();
        //    using (var adp = new NpgsqlDataAdapter(commandText, ConnectionString))
        //    {
        //        adp.FillSchema(dt, SchemaType.Source);
        //    }
        //    return dt;
        //}
    } 
    
}

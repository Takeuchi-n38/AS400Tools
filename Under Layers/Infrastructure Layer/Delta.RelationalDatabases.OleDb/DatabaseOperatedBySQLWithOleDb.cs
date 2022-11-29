using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;

namespace Delta.RelationalDatabases.OleDb
{
    public class DatabaseOperatedBySQLWithOleDb: DatabaseOperatedBySQL
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

        readonly string ConnectionString;
        public DatabaseOperatedBySQLWithOleDb(string aConnectionString):base()
        {
            this.ConnectionString = aConnectionString;
        }

        //int IDatabaseOperatedBySQL.ExecuteNonQuery(IEnumerable<string> commandTexts)
        //{
        //    int affected = 0;
        //    using (var cn = new OleDbConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new OleDbCommand())
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
        //    using (var cn = new OleDbConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new OleDbCommand())
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
        //    using (var cn = new OleDbConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new OleDbCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            command.CommandText = sqlQuery;
        //            using (var reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    reader.Read();
        //                    result = action(reader);
        //                }
        //                else
        //                {
        //                    result = null;
        //                }
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
        //    using (var cn = new OleDbConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new OleDbCommand())
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

        //protected void ExecuteReader(string commandText, Action<DbDataReader> action)
        //{
        //    using (var cn = new OleDbConnection(ConnectionString))
        //    {
        //        cn.Open();
        //        using (var command = new OleDbCommand())
        //        {

        //            command.Connection = cn;
        //            command.CommandType = CommandType.Text;

        //            command.CommandText = commandText;
        //            using (var reader = command.ExecuteReader())
        //            {
        //                //var x = reader.FieldCount;
        //                //var y = reader.GetColumnSchema();

        //                action(reader);

        //                reader.Close();
        //            }

        //        }
        //        cn.Close();
        //    }

        //}

        //List<T> IDatabaseOperatedBySQL.RawSqlQuerySingleColumn<T>(string sqlQuery)
        //{
        //    return ((IDatabaseOperatedBySQL)this).ExecuteReader<T>(sqlQuery, dr => dr.GetFieldValue<T>(0));
        //}

        //DataTable IDatabaseOperatedBySQL.Fill(string commandText)
        //{
        //    var dt = new DataTable();
        //    using (var adp = new OleDbDataAdapter(commandText, ConnectionString))
        //    {
        //        adp.Fill(dt);
        //    }
        //    return dt;
        //}

        //DataTable IDatabaseOperatedBySQL.FillSchema(string commandText)
        //{
        //    var dt = new DataTable();
        //    using (var adp = new OleDbDataAdapter(commandText, ConnectionString))
        //    {
        //        adp.FillSchema(dt, SchemaType.Source);
        //    }
        //    return dt;
        //}

        //protected DatabaseOperatedBySQL Database => this;

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases
{

    [Obsolete("廃止予定です。　DatabaseOperatedBySQL。")]

    public class IDatabaseOperatedBySQL : DatabaseOperatedBySQL
    {
        public IDatabaseOperatedBySQL(DbObjectsFactory dbFactory):base(dbFactory)
        {
        }

    }

    public abstract class DbObjectsFactory
    {
        protected readonly string ConnectionString;
        public DbObjectsFactory(string aConnectionString)
        {
            this.ConnectionString = aConnectionString;
        }

        public abstract DbConnection CreateConnection();

        public abstract DbCommand CreateCommand();

        public abstract DbDataAdapter CreateDataAdapter(string commandText);

    }

    public class DatabaseOperatedBySQL
    {
        readonly DbObjectsFactory dbFactory;
        public DatabaseOperatedBySQL(DbObjectsFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public int ExecuteNonQuery(IEnumerable<string> commandTexts)
        {
            int affected = 0;
            using (var cn = dbFactory.CreateConnection())
            {
                cn.Open();
                using (var command = cn.CreateCommand())
                {

                    command.Connection = cn;
                    command.CommandType = CommandType.Text;

                    commandTexts.ToList().ForEach(commandText =>
                    {
                        command.CommandText = commandText;
                        affected = command.ExecuteNonQuery();
                    });

                }
                cn.Close();
            }

            return affected;
        }

        public object ExecuteObject(string scalarQuery)
        {
            object result = null;
            using (var cn = dbFactory.CreateConnection())
            {
                cn.Open();
                using (var command = cn.CreateCommand())
                {

                    command.Connection = cn;
                    command.CommandType = CommandType.Text;

                    command.CommandText = scalarQuery;
                    result = command.ExecuteScalar();
                }
                cn.Close();
            }

            return result;
        }

        public List<T> ExecuteReader<T>(string sqlQuery, Func<DbDataReader, T> action)
        {
            var returns = new List<T>();
            using (var cn = dbFactory.CreateConnection())
            {
                cn.Open();
                using (var command = cn.CreateCommand())
                {

                    command.Connection = cn;
                    command.CommandType = CommandType.Text;

                    command.CommandText = sqlQuery;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returns.Add(action(reader));
                        }
                        reader.Close();
                    }
                }
                cn.Close();
            }
            return returns;
        }

        public T ExecuteReadFirst<T>(string sqlQuery, Func<DbDataReader, T> action) where T : class
        {
            T result;
            using (var cn = dbFactory.CreateConnection())
            {
                cn.Open();
                using (var command = cn.CreateCommand())
                {

                    command.Connection = cn;
                    command.CommandType = CommandType.Text;

                    command.CommandText = sqlQuery;
                    //using (var reader = command.ExecuteReader())
                    //{
                    //    result = reader.HasRows ? action(reader) : null;
                    //    reader.Close();
                    //}
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = action(reader);
                        }
                        else
                        {
                            result = null;
                        }
                        reader.Close();
                    }
                }
                cn.Close();
            }
            return result;
        }

        public List<T> RawSqlQuerySingleColumn<T>(string sqlQuery)
        {
            return ExecuteReader<T>(sqlQuery, dr => dr.GetFieldValue<T>(0));
        }

        public DataTable Fill(string commandText)
        {
            var dt = new DataTable();
            using (var adp = dbFactory.CreateDataAdapter(commandText))
            {
                adp.SelectCommand.CommandText = commandText;    
                adp.Fill(dt);
            }

            return dt;
        }

        public DataTable FillSchema(string commandText)
        {
            var dt = new DataTable();
            using (var adp = dbFactory.CreateDataAdapter(commandText))
            {
                adp.SelectCommand.CommandText = commandText;
                adp.FillSchema(dt, SchemaType.Source);
            }
            return dt;
        }

        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(new List<string>() { commandText });
        }

        public long ExecuteLong(string longQuery)
        {
            var scalar = ExecuteObject(longQuery);
            return scalar == null ? 0 : Convert.ToInt64(scalar);
        }

        public T ExecuteScalar<T>(string scalarQuery)
        {
            var scalar = ExecuteObject(scalarQuery);
            return scalar == null ? default(T) : (T)scalar;
        }

        public virtual DataTable Fill(ObjectOfSchema aObjectOfSchema)
        {
            var commandText = $"select * from {aObjectOfSchema.FullName}";
            return Fill(commandText);
        }

        public virtual DataTable FillSchema(ObjectOfSchema aObjectOfSchema)
        {
            var commandText = $"select * from {aObjectOfSchema.FullName}";
            return FillSchema(commandText);
        }
    }

}

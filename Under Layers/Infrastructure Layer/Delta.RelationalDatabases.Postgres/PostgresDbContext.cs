using Delta.RelationalDatabases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Delta.RelationalDatabases.Postgres
{
    
    public class PostgresDbContext : DbContext
    {
        //int IDatabaseOperatedBySQL.ExecuteNonQuery(string nonQuery)
        //{
        //    return Database.ExecuteSqlRaw(nonQuery);
        //}

        //T IDatabaseOperatedBySQL.ExecuteScalar<T>(string scalarQuery)
        //{
        //    throw new NotImplementedException();
        //}
        //T IDatabaseOperatedBySQL.ExecuteReadFirst<T>(string sqlQuery, Func<DbDataReader, T> action)
        //{
        //    throw new NotImplementedException();
        //}
        string ConnectionString =>
    $"Host={Host};Port={Port};Database={DatabaseOfConnectionString};Username={Username};Password={Password};";

        readonly string Host;
        readonly string Port;
        readonly string DatabaseOfConnectionString;
        readonly string Username;
        readonly string Password;
        protected PostgresDbContext(string aHost, string aPort, string aDatabaseOfConnectionString, string aUsername, string aPassword)
        {
            Host = aHost;
            Port = aPort;
            DatabaseOfConnectionString = aDatabaseOfConnectionString;
            Username = aUsername;
            Password = aPassword;
        }

        protected PostgresDbContext(string aDatabaseOfConnectionString) : this("localhost", "5432", aDatabaseOfConnectionString, "postgres", "postgres")
        {

        }

    //    public PostgresDbContext(DbContextOptions options)
    //: base(options)
    //    {
    //    }



        //int IDatabaseOperatedBySQL.ExecuteNonQuery(string nonQuery)
        //{
        //    return Database.ExecuteSqlRaw(nonQuery);
        //}

        //T IDatabaseOperatedBySQL.ExecuteScalar<T>(string scalarQuery)
        //{
        //    throw new NotImplementedException();
        //}

        //List<T> IDatabaseOperatedBySQL.ExecuteReader<T>(string query, Func<DbDataReader, T> map)
        //{

        //    using (var command = Database.GetDbConnection().CreateCommand())
        //    {
        //        command.CommandText = query;
        //        command.CommandType = CommandType.Text;

        //        Database.OpenConnection();

        //        using (var result = command.ExecuteReader())
        //        {
        //            var entities = new List<T>();

        //            while (result.Read())
        //            {
        //                entities.Add(map(result));
        //            }

        //            return entities;
        //        }
        //    }

        //}

        //T IDatabaseOperatedBySQL.ExecuteReadFirst<T>(string sqlQuery, Func<DbDataReader, T> action)
        //{
        //    throw new NotImplementedException();
        //}

        //List<T> IDatabaseOperatedBySQL.ExecuteReader<T>(string query, Func<DbDataReader, T> map)
        //{

        //    using (var command = Database.GetDbConnection().CreateCommand())
        //    {
        //        command.CommandText = query;
        //        command.CommandType = CommandType.Text;

        //        Database.OpenConnection();

        //        using (var result = command.ExecuteReader())
        //        {
        //            var entities = new List<T>();

        //            while (result.Read())
        //            {
        //                entities.Add(map(result));
        //            }

        //            return entities;
        //        }
        //    }

        //}


    }
}

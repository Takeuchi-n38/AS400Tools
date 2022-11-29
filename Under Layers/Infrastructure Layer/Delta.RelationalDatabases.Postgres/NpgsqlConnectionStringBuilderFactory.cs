using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Postgres
{
    public class NpgsqlConnectionStringBuilderFactory
    {
        public static DbConnectionStringBuilder ConnectionStringBuilderOfLocalTest(string aDatabaseName)
        {
            return ConnectionStringBuilderOfLocalTest(aDatabaseName,string.Empty);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilderOfLocalTest(string aDatabaseName, string aEncoding)
        {
            return ConnectionStringBuilder("localhost", aDatabaseName, aEncoding);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, string aDatabaseName)
        {
            return ConnectionStringBuilder(aHost,aDatabaseName,string.Empty);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, string aDatabaseName, string aEncoding)
        {
            return ConnectionStringBuilder(aHost, 5432, aDatabaseName,aEncoding);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, int aPort, string aDatabaseName)
        {
            return ConnectionStringBuilder(aHost, aPort, aDatabaseName, string.Empty);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, int aPort, string aDatabaseName, string aEncoding)
        {
            return ConnectionStringBuilder(aHost, aPort, aDatabaseName, "postgres", "postgres", aEncoding);
        }
        static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, int aPort, string aDatabaseName, string aUsername, string aPassword, string aEncoding)
        {
            return ConnectionStringBuilder(aHost, aPort, aDatabaseName, aUsername, aPassword, aEncoding,0);//0は無制限
        }

        static DbConnectionStringBuilder ConnectionStringBuilder(string aHost, int aPort, string aDatabaseName, string aUsername, string aPassword, string aEncoding,int aCommandTimeout)
        {
            
            NpgsqlConnectionStringBuilder param = new NpgsqlConnectionStringBuilder();
            param.Host = aHost;
            param.Port = aPort;
            param.Database = aDatabaseName;
            param.Username = aUsername;
            param.Password = aPassword;
            if(aEncoding!=string.Empty)
            {
                param.Encoding = aEncoding;//default:UTF8
                //param.ClientEncoding = "SJIS";//default:null
            }
            param.CommandTimeout= aCommandTimeout;
            return param;
        }

    }
}

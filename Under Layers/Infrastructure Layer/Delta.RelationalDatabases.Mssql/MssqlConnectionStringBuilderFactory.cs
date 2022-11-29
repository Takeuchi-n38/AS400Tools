using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlConnectionStringBuilderFactory
    {
        //public static DbConnectionStringBuilder ConnectionStringBilderOfLocalTest(string aDatabaseName)
        //{
        //    return ConnectionStringBilderOfLocalTest(aDatabaseName, string.Empty);
        //}

        public static DbConnectionStringBuilder ConnectionStringBuilderOfLocalTest(string aInitialCatalog//, string aEncoding
                                                                                                      )
        {
            return ConnectionStringBuilder("(local)", "MSSQLSERVER", aInitialCatalog);//, aEncoding);
        }

        //public static DbConnectionStringBuilder ConnectionStringBilder(string aHost, string aDatabaseName)
        //{
        //    return ConnectionStringBilder(aHost, aDatabaseName, string.Empty);
        //}

        //public static DbConnectionStringBuilder ConnectionStringBilder(string aHost, string aDatabaseName, string aEncoding)
        //{
        //    return ConnectionStringBilder(aHost, 5432, aDatabaseName, aEncoding);
        //}

        //public static DbConnectionStringBuilder ConnectionStringBilder(string aHost, int aPort, string aDatabaseName)
        //{
        //    return ConnectionStringBilder(aHost, aPort, aDatabaseName, string.Empty);
        //}
        public static DbConnectionStringBuilder ConnectionStringBuilder(string aServerName, string aInstanceName, string aInitialCatalog)
        {
            return ConnectionStringBuilder($"{aServerName}\\{aInstanceName}", aInitialCatalog);
        }

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aDataSource, //int aPort,
                                                                                     string aInitialCatalog
            //, string aEncoding
            )
        {
            return ConnectionStringBuilder(aDataSource, //aPort,
                                                 aInitialCatalog, "sa", "Delta110.Dcs0330");//, aEncoding);
        }
        /*
 ID:  sa
Pass:  Delta110.Dcs0330
DBName:  TRIM_SYSTEM
//MSSQLSERVER
        "Data Source=MySqlServer\MSSQL1;"  
 */
        //static DbConnectionStringBuilder ConnectionStringBilder(string aHost, int aPort, string aDatabaseName, string aUsername, string aPassword, string aEncoding)
        //{
        //    return ConnectionStringBilder(aHost, aPort, aDatabaseName, aUsername, aPassword, aEncoding, 0);//0は無制限
        //}

        public static DbConnectionStringBuilder ConnectionStringBuilder(string aDataSource, 
            //int aPort,
            string aInitialCatalog, string aUserID, string aPassword//, 
            //string aEncoding,
            //int aCommandTimeout
            )
        {
            SqlConnectionStringBuilder param = new SqlConnectionStringBuilder();
            param.DataSource = aDataSource;
            //param.Port = aPort;
            param.InitialCatalog = aInitialCatalog;
            param.UserID = aUserID;
            param.Password = aPassword;
            //if (aEncoding != string.Empty)
            //{
            //    param.Encoding = aEncoding;//default:UTF8
            //    //param.ClientEncoding = "SJIS";//default:null
            //}
            //param.CommandTimeout = aCommandTimeout;
            return param;
        }
    }
}

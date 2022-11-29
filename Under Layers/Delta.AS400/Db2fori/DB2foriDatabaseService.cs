using Delta.AS400.DataTypes.Characters;
using Delta.Utilities.RelationalDatabases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Db2fori
{
    public class DB2foriDatabaseService : IDatabaseOperatedBySQLWithOleDb
    {
        string IDatabaseOperatedBySQLWithOleDb.ConnectionString => $"Provider=IBMDA400;Data Source={IP};User ID={userID};Password={password};";//var cs = "Data Source=192.168.10.229;User ID=QUSER;Password=QUSER;";

        readonly string IP;
        readonly string userID = "QUSER";
        readonly string password = "QUSER";

        DB2foriDatabaseService(DB2foriOperatedBySQL aDB2foriDbContext) :base(aDB2foriDbContext, new Db2foriSchemaRepository(aDB2foriDbContext))
        {
            DB2foriDbContext = aDB2foriDbContext;
        }

        public static DB2foriDatabaseService Of(DB2foriOperatedBySQL aDB2foriDbContext)
        {
            return new DB2foriDatabaseService(aDB2foriDbContext);
        }

        public override void CreateTableLike(Table aSourceTable, Table aTargetTable)
        {
            ExecuteNonQuery($"create table {aTargetTable.FullName} like {aSourceTable.FullName}");
        }

    }
}

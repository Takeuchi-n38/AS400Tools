using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases
{
    public abstract class RelationalDatabaseService
    {
        readonly IDatabaseOperatedBySQL DatabaseOperatedBySQL;


        protected RelationalDatabaseService(IDatabaseOperatedBySQL aDatabaseOperatedBySQL)
        {
            DatabaseOperatedBySQL = aDatabaseOperatedBySQL;
        }

        //readonly ITableRepository tableRepository;
        //readonly IViewRepository viewRepository;

        protected int ExecuteNonQuery(string sqlCommand)
        {
            return DatabaseOperatedBySQL.ExecuteNonQuery(sqlCommand);
        }

        //public abstract void CreateTableLike(Table aSourceTable, Table aTargetTable);


        //public DataTable Fill(string commandText)
        //{
        //    return DatabaseOperatedBySQL.Fill(commandText);
        //}

        //public DataTable Fill(ObjectOfSchema aObjectOfSchema)
        //{
        //    var commandText = $"select * from {aObjectOfSchema.FullName}";
        //    return Fill(commandText);
        //}
        //public DataTable FillSchema(ObjectOfSchema aObjectOfSchema)
        //{
        //    var commandText = $"select * from {aObjectOfSchema.FullName}";
        //    return FillSchema(commandText);
        //}

        //public DataTable FillSchema(string commandText)
        //{
        //    return DatabaseOperatedBySQL.FillSchema(commandText);
        //}

        //public void Create(Schema aSchema)
        //{
        //    ExecuteNonQuery($"create schema {aSchema.Name}");
        //}

    }
}

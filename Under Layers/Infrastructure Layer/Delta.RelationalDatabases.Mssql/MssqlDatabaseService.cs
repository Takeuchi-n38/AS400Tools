using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlDatabaseService //: RelationalDatabaseService
    {
        readonly DatabaseOperatedBySQL DatabaseOperatedBySQL;

        readonly MssqlSchemaService SchemaService;
        readonly MssqlObjectOfSchemaService ObjectOfSchemaService;
        //            ObjectOfSchemaRepository = PostgresObjectOfSchemaService.Of(aTestDatabase);

        MssqlDatabaseService(DatabaseOperatedBySQL aDatabaseOperatedBySQL,
            MssqlSchemaService aPostgresSchemaService,
            MssqlObjectOfSchemaService aObjectOfSchemaService) 
            //: base(aDatabaseOperatedBySQL)
        {
            this.DatabaseOperatedBySQL = aDatabaseOperatedBySQL;
            this.SchemaService = aPostgresSchemaService;
            this.ObjectOfSchemaService = aObjectOfSchemaService;
        }

        public static MssqlDatabaseService OfLocalTest(string DatabaseName)
        {
            return Of(MssqlConnectionStringBuilderFactory.ConnectionStringBuilderOfLocalTest(DatabaseName));
        }

        public static MssqlDatabaseService Of(DbConnectionStringBuilder aDbConnectionStringBuilder)
        {
            var database = MssqlOperatedBySQLWithSqlClient.Of(aDbConnectionStringBuilder);
            return new MssqlDatabaseService(database, MssqlSchemaService.Of(database), MssqlObjectOfSchemaService.Of(database));
        }

        //public void Recreate(Schema aSourceSchema,Schema aTargetSchema)
        //{
        //    SchemaService.CreateIfNotExist(aTargetSchema);
        //    aTargetSchema.Tables.ForEach(targetTable =>
        //    ObjectOfSchemaService.ReCreateTableLike(targetTable.CreateWithChangingSchema(aSourceSchema), targetTable)
        //    );
        //}

        public void Create(Table table, string ddlScript,bool isRecreate)
        {
            //createTableScripts.ToList().ForEach(cur =>
            //{
                SchemaService.CreateIfNotExist(table.Schema);
                //if (isRecreate)
                //{
                //    SchemaService.ReCreate(cur.table.Schema);
                //}
                //else
                //{
                //    SchemaService.CreateIfNotExist(cur.table.Schema);
                //}
                ObjectOfSchemaService.Create(table, ddlScript, isRecreate);
            //});
        }
        //public void CreateObjectOfSchema(IEnumerable<(Table table, string ddlScript)> createTableScripts, bool isRecreate)
        //{
        //    CreateObjectOfSchema(createTableScripts,isRecreate);
        //}

        //void CreateObjectOfSchema(IEnumerable<(Table table, string ddlScript)> createTableScripts, bool isRecreate)
        //{
        //    createTableScripts.ToList().ForEach(cur =>
        //    {
        //        SchemaService.CreateIfNotExist(cur.table.Schema);

        //        ObjectOfSchemaService.Create(cur.table,cur.ddlScript, isRecreate);
        //    });
        //}

        public void CreateViewForDifferenceByExceptTable(View differenceView, Table targetTable, Table exceptTable)
        {
            string target_joined_column_names =
                targetTable.Columns.Count == 0 ? "*" : targetTable.JoinedColumnNames();
            string except_joined_column_names =
                exceptTable.Columns.Count == 0 ? "*" : exceptTable.JoinedColumnNames();

            CreateViewForDifferenceByExceptTable(differenceView, targetTable, target_joined_column_names, exceptTable, except_joined_column_names);
        }

        public void CreateViewForDifferenceByExceptTable(View differenceView, Table targetTable, string target_joined_column_names, Table exceptTable, string except_joined_column_names)
        {
            DatabaseOperatedBySQL.ExecuteNonQuery($"CREATE OR ALTER VIEW {differenceView.FullName} AS SELECT {target_joined_column_names} FROM {targetTable.FullName} EXCEPT SELECT {except_joined_column_names} FROM {exceptTable.FullName}");
        }

        public void ReplaceWith(Table aSourceTable, Table aTargetTable)
        {
            ObjectOfSchemaService.ReplaceWith(aSourceTable, aTargetTable);
        }

        public void Clear(Schema aSchema)
        {
            aSchema.Tables.ForEach(table =>Truncate(table));
        }

        public void Truncate(Table table)
        {
            ObjectOfSchemaService.Truncate(table);
        }

        public void ReplaceDataByCsv(string csv_file_path, Table table)
        {
            ObjectOfSchemaService.ReplaceDataByCsv(csv_file_path, table);
        }

        public long Count(ObjectOfSchema objectOfSchema)
        {
            return ObjectOfSchemaService.Count(objectOfSchema);
        }
        public Table FindColumns(Table aTable)
        {
            return ObjectOfSchemaService.FindColumns(aTable);
        }
        public int InsertValues(Table table, IEnumerable<IEnumerable<string>> valuesList)
        {
            if (table.Columns.Count() == 0)
            {
                FindColumns(table);
            }
            return ObjectOfSchemaService.InsertValues(table,valuesList);
        }

    }
}

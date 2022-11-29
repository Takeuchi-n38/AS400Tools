using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace Delta.RelationalDatabases
{
    public abstract class ObjectOfSchemaRepository
    {
        protected readonly DatabaseOperatedBySQL Database;
        protected ObjectOfSchemaRepository(DatabaseOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        protected virtual Table TargetTable { get; }
        public string ObjectName => TargetTable.FullName;

        //public int CountBy(string table_schema, string table_name);

        //public bool IsExist(string table_schema, string table_name);

        //public bool IsExist(Table table);

        public long Count()
        {
            return Count(TargetTable, null);
        }

        public long Count(ObjectOfSchema objectOfSchema)
        {
            return Count(objectOfSchema, null);
        }

        public long Count(string filterCondition)
        {
            return Count(TargetTable, filterCondition);
        }

        long Count(ObjectOfSchema objectOfSchema, string filterCondition)
        {
            var condition = string.IsNullOrEmpty(filterCondition) ? string.Empty : $" where {filterCondition}";
            var countQuery = $"select count(*) from {objectOfSchema.FullName}{condition}";
            return Database.ExecuteLong(countQuery);
            //if (result == null) return 0;
            //return int.Parse(result.ToString());
        }

        bool Exists(ObjectOfSchema objectOfSchema, string filterCondition)
        {
            return Count(objectOfSchema, filterCondition) > 0;
        }

        protected bool Exists(string filterCondition) => Exists(TargetTable, filterCondition);

        public int ExecuteNonQuery(string nonQuery) => Database.ExecuteNonQuery(nonQuery);

        public List<T> ExecuteReader<T>(string commandText, Func<DbDataReader, T> action)
            => Database.ExecuteReader(commandText, action);

        protected T Find<T>(string commandText, Func<DbDataReader, T> action) where T : class
            => Database.ExecuteReadFirst(commandText, action);

        //, string filterCondition

        //public void CreateSchema(Schema aSchema)
        //{
        //    //try
        //    //{
        //    ExecuteNonQuery($"create schema {aSchema.Name}");
        //    //}
        //    //catch (OleDbException ex)
        //    //{
        //    //    if (!ex.Message.StartsWith("SQL0601"))
        //    //    {
        //    //        throw;
        //    //    }
        //    //}
        //}

        public void CreateView(string view_name, string view_body)
        {
            ExecuteNonQuery($"CREATE OR REPLACE VIEW {view_name} AS {view_body}");
        }
        //public void CreateTableByScript(string create_table_script, string defaul_schema);

        //public void CreateTableByScriptFilePath(string createTableScrptFilePath, string default_schema);

        //    public void CreateTableByLike(Schema sourceSchema, Schema targetSchema,
        //List<string> tableNames)
        //    {
        //        tableNames.ForEach(tableName =>
        //        {
        //            var source_full_table = Table.Of(sourceSchema, tableName);
        //            var target_full_table = Table.Of(targetSchema, tableName);
        //            Database.CreateTableLike(source_full_table, target_full_table);
        //        });
        //    }

        //void CreateTableLike(string sourceTableName, string targetTableName)
        //void CreateTableLike(Schema setupSchema)
        //{
        //    Database.CreateTableLike(TargetTable,Table.Of(setupSchema,TargetTable.Name));
        //}

        //public abstract void CreateTableLike(Table aTargetTable,Table aSourceTable);

        //public void CreateTableByLike(string sourceSchemaName, string targetSchemaName, List<string> tableNames);

        //public List<string> SelectColumn_namesBy(string table_schema, string table_name);

        //public Table Find(Schema aSchema, string table_name);

        //public void FindColumns(Table aTable);

        public void Truncate(Table aTable)
        {
            Database.ExecuteNonQuery($"truncate table {aTable.FullName}");
        }

        public void Truncate()
        {
            Truncate(TargetTable);
        }

        public void ReplaceWith(Schema setupSchema)
        {
            Truncate();
            InsertIntoSelectFrom(setupSchema);
        }

        public void ReplaceWith(Table sourceTable, Table targetTable)
        {

            Truncate(targetTable);
            InsertIntoSelectFrom(sourceTable, targetTable);
            //CorrectValOfSequenceIfNecessary(targetTable);
        }

        //public void DropTable(string table_full_name);

        //public void DropTable(string table_schema, string table_name);

        //public void CopyByCSV(string csv_file_path, string table_full_name, string force_not_null_columns);

        //public void InsertInto(string source_table_name, string target_table_name);
        public void InsertIntoSelectFrom(Schema setupSchema)
        {
            InsertIntoSelectFrom(Table.Of(setupSchema, TargetTable.Name), TargetTable);
        }

        public void InsertIntoSelectFrom(Table aSourceTable, Table aTargetTable)
        {
            Database.ExecuteNonQuery($"insert into {aTargetTable.FullName} select * from {aSourceTable.FullName}");
        }

        View ExcludeView(Schema expectedSchema) => ExcludeViewOf(TargetTable.CreateWithChangingSchema(expectedSchema));

        View IncludeView(Schema expectedSchema) => IncludeViewOf(TargetTable.CreateWithChangingSchema(expectedSchema));

        static View ExcludeViewOf(Table TargetTable) => View.Of(TargetTable.Schema, $"{TargetTable.Name}EV");

        static View IncludeViewOf(Table TargetTable) => View.Of(TargetTable.Schema, $"{TargetTable.Name}IV");


        //public void CorrectValOfSequenceIfNecessary(string table_full_name);
        public long CountOfExtra(Schema expectedSchema)
        {
            return Count(ExcludeView(expectedSchema));
        }

        public long CountOfIntra(Schema expectedSchema)
        {
            return Count(IncludeView(expectedSchema));
        }

        public void CreateDifferenceView(Schema expectedSchema)
        {
            CreateDifferenceView(expectedSchema, expectedSchema);
        }

        public void CreateDifferenceView(Schema inSchema, Schema expectedSchema)
        {
            var expectedTable = TargetTable.CreateWithChangingSchema(expectedSchema);
            CreateViewForDifferenceByExceptTable(ExcludeView(inSchema), TargetTable, expectedTable);
            CreateViewForDifferenceByExceptTable(IncludeView(inSchema), expectedTable, TargetTable);
        }

        public void CreateViewForDifferenceByExceptTable(View differenceView, Table targetTable, Table exceptTable)
        {
            string target_joined_column_names =
                targetTable.Columns.Count == 0 ? "*" : targetTable.JoinedColumnNames();
            string except_joined_column_names =
                exceptTable.Columns.Count == 0 ? "*" : exceptTable.JoinedColumnNames();

            CreateViewForDifferenceByExceptTable(differenceView, targetTable, target_joined_column_names, exceptTable, except_joined_column_names);
        }


        public abstract void CreateViewForDifferenceByExceptTable(View differenceView, Table targetTable, string target_joined_column_names, Table exceptTable, string except_joined_column_names);



    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.RelationalDatabases.Postgres
{
    public class PostgresObjectOfSchemaService 
    {

        readonly DatabaseOperatedBySQL Database;
        PostgresObjectOfSchemaService(DatabaseOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        public static PostgresObjectOfSchemaService Of(DatabaseOperatedBySQL aDatabase)
        {
            return new PostgresObjectOfSchemaService(aDatabase);
        }

        void CorrectValOfSequenceIfNecessary(Table aTable)
        {
            var column_defaultOfId = SelectColumn_defaultOfIdBy(aTable);
            if (column_defaultOfId.Equals(string.Empty)) return;
            var splittedColumn_defaultOfId = column_defaultOfId.Split('\'');
            var sequence_full_name = splittedColumn_defaultOfId[1];//ex:nextval('seatlib.keic01_id_seq'::regclass)
            CorrectValOfSequence(sequence_full_name, aTable.FullName);
        }


        string SelectColumn_defaultOfIdBy(Table aTable)
        {
            var column_defaults = SelectColumn_defaultsOfIdBy(aTable);
            if (column_defaults.Count == 0) return "";
            return column_defaults[0];
        }

        List<string> SelectColumn_defaultsOfIdBy(Table aTable)
        {
            return GetColumn($"select column_default from information_schema.columns where table_schema = '{aTable.SchemaName}' and table_name = '{aTable.Name}' and column_name='id' and column_default like 'nextval%'");
        }

        void CorrectValOfSequence(string sequence_full_name, string table_full_name)
        {
            Database.ExecuteNonQuery($"SELECT setval('{sequence_full_name}', (SELECT MAX(id) FROM {table_full_name}))");
        }

        public Table FindColumns(Table aTable)
        {
            var rawSqlString = $"SELECT column_name FROM information_schema.columns WHERE table_schema = LOWER('{aTable.Schema.Name}') AND table_name = LOWER('{aTable.Name}') ORDER BY ordinal_position";
            var columnNames = Database.RawSqlQuerySingleColumn<string>(rawSqlString);
            columnNames.ForEach(columnName => aTable.AddColumn(new Column(columnName)));
            return aTable;
        }

        public void ReplaceDataByCsv(string csv_file_path, Table table)
        {
            Truncate(table);
            FindColumns(table);
            CopyByCsv(csv_file_path, table, table.JoinedColumnNames());
        }

        public void CopyByCsv(string csv_file_path, Table aTable, string force_not_null_columns)
        {
            //Database.ExecuteNonQuery($"COPY {aTable.FullName} FROM '{csv_file_path}' WITH (FORMAT csv,DELIMITER ',', HEADER, ENCODING 'UTF8',FORCE_NOT_NULL ({force_not_null_columns}))");
            Database.ExecuteNonQuery($"COPY {aTable.FullName} FROM program 'cmd /c type {csv_file_path}' WITH (FORMAT csv,DELIMITER ',', HEADER, ENCODING 'UTF8',FORCE_NOT_NULL ({force_not_null_columns}))");
        }
        //public void CopyByCsv(string csv_file_path, Table aTable, string force_not_null_columns)
        //{
        //    Database.ExecuteNonQuery($"COPY {aTable.FullName} FROM '{csv_file_path}' WITH (FORMAT text,DELIMITER \\t, ENCODING 'UTF8',FORCE_NOT_NULL ({force_not_null_columns}))");
        //}

        List<string> GetColumn(string rawSqlString)
        {
            return Database.RawSqlQuerySingleColumn<string>(rawSqlString);
        }

        public long Count(ObjectOfSchema objectOfSchema)
        {
            return Count(objectOfSchema, null);
        }

        long Count(ObjectOfSchema objectOfSchema, string filterCondition)
        {
            var condition = string.IsNullOrEmpty(filterCondition) ? string.Empty : $" where {filterCondition}";
            var countQuery = $"select count(*) from {objectOfSchema.FullName}{condition}";
            return Database.ExecuteLong(countQuery);
            //if (result == null) return 0;
            //return int.Parse(result.ToString());
        }

        //public int CountBy(string table_schema, string table_name)
        //{
        //    return Database.ExecuteSqlRaw($"SELECT count(*) as count FROM information_schema.tables WHERE table_schema = {table_schema} AND table_name = {table_name}");
        //}

        //public bool IsExist(string table_schema, string table_name)
        //{
        //    return 0 < CountBy(table_schema, table_name);
        //}

        //public bool IsExist(Table table)
        //{
        //    return IsExist(table.schema.name, table.name);
        //}

        public void Create(Table table, string ddlScript, bool isRecreate)
        {
            var executeCommandTexts = new List<string>();
            executeCommandTexts.Add($"SET search_path TO {table.SchemaName}");
            if (isRecreate) executeCommandTexts.Add($"DROP TABLE IF EXISTS {table.Name} CASCADE");
            executeCommandTexts.Add(ddlScript);
            executeCommandTexts.Add($"SET search_path TO {"\"$user\""}");

            try
            {
                Database.ExecuteNonQuery(executeCommandTexts);
            }
            catch (Npgsql.PostgresException ex)
            {
                if (ex.SqlState == "42P07")
                {
                    Console.WriteLine(ex.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        //List<string> SelectColumn_namesBy(string table_schema, string table_name)
        //{
        //    var rawSqlString = $"SELECT column_name FROM information_schema.columns WHERE table_schema = '{table_schema}' AND table_name = '{table_name}' ORDER BY ordinal_position");
        //    return Database.RawSqlQuerySingleStringColumn(rawSqlString);
        //}

        //public Table Find(Schema aSchema, string table_name)
        //{
        //    var instance = new Table(aSchema, table_name);
        //    var columnNames = SelectColumn_namesBy(aSchema.name, table_name);
        //    columnNames.ForEach(columnName => instance.AddColumn(new Column(columnName)));
        //    return instance;
        //}

        public void ReCreateTableLike(Table aSourceTable, Table aTargetTable)
        {
            Drop(aTargetTable);
            CreateTableLike(aSourceTable, aTargetTable);
        }

        public void Drop(Table aTable)
        {
            Database.ExecuteNonQuery(CommandTextOfDrop(aTable));
        }

        public string CommandTextOfDrop(Table aTable)
        {
            return $"DROP TABLE IF EXISTS {aTable.FullName} CASCADE";
        }
        void CreateTableLike(Table aSourceTable, Table aTargetTable)
        {
            Database.ExecuteNonQuery($"CREATE TABLE {aTargetTable.FullName} (LIKE {aSourceTable.FullName} INCLUDING ALL)");
        }
        //public void CreateTableByLike(string source_table_name, string target_table_name)
        //{
        //    Database.ExecuteSqlRaw($"CREATE TABLE {target_table_name} (LIKE {source_table_name} INCLUDING ALL");
        //}

        public void ReplaceWith(Table sourceTable, Table targetTable)
        {

            Truncate(targetTable);
            InsertIntoSelectFrom(sourceTable, targetTable);
            CorrectValOfSequenceIfNecessary(targetTable);
        }

        public void Truncate(Table aTable)
        {
            Database.ExecuteNonQuery($"TRUNCATE {aTable.FullName} RESTART IDENTITY CASCADE");
        }

        void InsertIntoSelectFrom(Table aSourceTable, Table aTargetTable)
        {
            Database.ExecuteNonQuery($"insert into {aTargetTable.FullName} select * from {aSourceTable.FullName}");
        }

        public int InsertValues(Table table, IEnumerable<IEnumerable<string>> valuesList)
        {
            var valuesStrings = valuesList.Select(values=> $"({string.Join(",", values)})").Aggregate((all,cur)=>$"{all},{cur}");
            var joinedColumnNames= table.JoinedColumnNames();
            //if (joinedColumnNames.StartsWith("id,"))
            //{
            //    joinedColumnNames= joinedColumnNames.Substring(3);
            //}
            return Database.ExecuteNonQuery($"INSERT INTO {table.FullName} ({joinedColumnNames}) VALUES {valuesStrings};");
        }
    }
}

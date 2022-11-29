using System.Collections.Generic;

namespace Delta.RelationalDatabases
{
    public class Schema
    {
        public readonly string Name;

        public Schema(string aName)
        {
            Name = aName;
            Tables = new List<Table>();
        }

        public Schema(string aName, List<string> tableNames)
        {
            Name = aName;
            Tables = new List<Table>();
            AddTables(tableNames);
        }

        public static Schema Of(string Name)
        {
            return new Schema(Name);
        }

        public readonly List<Table> Tables;

        protected Schema AddTable(Table table)
        {
            Tables.Add(table);
            return this;
        }

        public virtual Schema AddTable(string tableName)
        {
            return AddTable(new Table(this, tableName));
        }

        public Schema AddTables(List<string> tableNames)
        {
            tableNames.ForEach(tableName => AddTable(tableName));
            return this;
        }

        public Table CreateTableOf(string tableName) => Table.Of(this,tableName);

    }
}

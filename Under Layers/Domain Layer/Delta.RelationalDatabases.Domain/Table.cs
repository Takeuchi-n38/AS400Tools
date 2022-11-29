using System.Collections.Generic;
using System.Linq;

namespace Delta.RelationalDatabases
{
    public class Table : ObjectOfSchema
    {
        public readonly string Summary;

        public Table(Schema aSchema, string aName, string aSummary) : base(aSchema, aName)
        {
            Columns = new List<Column>();
            Summary = aSummary;
        }

        public Table(Schema aSchema, string aName) : this(aSchema, aName,string.Empty)
        {

        }

        public static Table Of(Schema newSchema, string TableName)
        {
            return new Table(newSchema, TableName);
        }
        public static Table Of(string SchemaName, string TableName)
        {
            return Of(Schema.Of(SchemaName), TableName);
        }

        public readonly List<Column> Columns;
        public IEnumerable<string> ColumnNames => Columns.Select(c => c.Name);

        public Table AddColumn(Column column)
        {
            Columns.Add(column);
            return this;
        }

        public virtual string JoinedColumnNames()
        {
            return string.Join(",", ColumnNames.ToArray());
        }

        public virtual Table CreateWithChangingSchema(Schema newSchema)
        {
            var rtn = Of(newSchema, Name);
            Columns.ForEach(col => rtn.AddColumn(col.clone()));
            return rtn;
        }

        public List<string> KeyNames = new List<string>();


    }
}

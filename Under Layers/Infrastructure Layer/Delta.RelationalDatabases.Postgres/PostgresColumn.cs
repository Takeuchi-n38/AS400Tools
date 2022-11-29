using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Postgres
{
    public class PostgresColumn:Column
    {
        string Summary;
        PostgresColumnDataType DataType;
        public PostgresColumn(string aName, PostgresColumnDataType aDataType, string aSummary) :base(aName)
        {
            this.DataType = aDataType;
            this.Summary = aSummary;
        }

        public static PostgresColumn Id => new PostgresColumn($"id", PostgresColumnDataType.Serial, "");

        public string Ddl()
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("{0,-20}", Name));
            sb.Append(DataType.Ddl());

            //if (!(validValues.Count == 0))
            //{
            //    var delimiterTmp = "";

            //    sb.Append(" CHECK (" + name + " IN (");

            //    // Join valid values by ", "
            //    foreach (var validValue in validValues)
            //    {
            //        sb.Append(delimiterTmp + validValue);
            //        delimiterTmp = ", ";
            //    }

            //    sb.Append("))");
            //}

            sb.Append(',');

            if (!(Summary.Length == 0))
            {
                sb.Append(" --");
                sb.Append(Summary);
            }

            return sb.ToString();
        }

        public PostgresColumn Of4s=>new PostgresColumn($"{Name}4s",DataType.Of4s,"");
    }
}

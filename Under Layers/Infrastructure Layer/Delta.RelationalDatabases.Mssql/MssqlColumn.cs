using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlColumn:Column
    {
        string Summary;
        MssqlColumnDataType DataType;
        public MssqlColumn(string aName, MssqlColumnDataType aDataType, string aSummary) :base(aName)
        {
            this.DataType = aDataType;
            this.Summary = aSummary;
        }

        public static MssqlColumn Id => new MssqlColumn($"id", MssqlColumnDataType.Serial, "");

        public string Ddl()
        {
            //, CONSTRAINT PK_TransactionHistoryArchive1_TransactionID PRIMARY KEY CLUSTERED (TransactionID)
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

        public MssqlColumn Of4s=>new MssqlColumn($"{Name}4s",DataType.Of4s,"");
    }
}

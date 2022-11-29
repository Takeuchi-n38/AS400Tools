using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Delta.RelationalDatabases.Db2fori
{
    [Obsolete("廃止予定です。　DB2foriOperatorを使ってください。")]

    public class DB2foriOperatedBySQL : DatabaseOperatedBySQL
    {
        static string ConnectionString(string aIP, string aUserID, string aPassword) => $"Provider=IBMDA400;Data Source={aIP};User ID={aUserID};Password={aPassword};";//var cs = "Data Source=192.168.10.229;User ID=QUSER;Password=QUSER;";

        public DB2foriOperatedBySQL(string aConnectionString) : base(new OleDbObjectsFactory(aConnectionString))
        {

        }

        static string IPofTest = "192.168.10.229";

        public static DB2foriOperatedBySQL TestOf()
        {
            return new DB2foriOperatedBySQL(ConnectionString(IPofTest, "QUSER", "QUSER"));
        }

        public int GetFileLength(Table aTable)
        {
            try
            {
                var hexDt = FillSchema(GetHexBytesCommandText(aTable));
                var fileLength = 0;
                for (int i = 0; i < hexDt.Columns.Count; i++)
                {
                    fileLength += hexDt.Columns[i].MaxLength;
                }
                return fileLength / 2;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetFileLength==0:{ex.Message}");
                return 0;
            }
        }

        public IEnumerable<string> GetHexStrings(Table aTable)
        {
            var hexDt = Fill(GetHexBytesCommandText(aTable));
            //return hexDt.AsEnumerable().Select(r => CCSID930.ToBytesFrom(r.Field<string>(0)));
            return hexDt.Rows.Cast<DataRow>().Select(r => r.ItemArray[0]==null?string.Empty:(string)r.ItemArray[0]);
        }

        string GetHexBytesCommandText(Table aTable)
        {
            var columnsCommandTxts = new List<string>();
            var dt = FillSchema(aTable);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var column = dt.Columns[i];
                columnsCommandTxts.Add($"HEX({column.ColumnName})");
            }
            var query= $"select {string.Join(" || ", columnsCommandTxts)} from {aTable.FullName}";
            for (int i = 0; i < dt.PrimaryKey.Length; i++)
            {
                var column = dt.PrimaryKey[i];
                query=$"{query} {(i == 0?"order by":",")} {column.ColumnName}";
            }
            return query;
        }

        public void Qcmdexc(string cmd)
        {
            ExecuteNonQuery(cmd);
        }
    }
}

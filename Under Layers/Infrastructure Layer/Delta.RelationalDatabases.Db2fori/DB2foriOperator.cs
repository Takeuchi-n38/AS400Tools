using Delta.IBMPowerSystems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Delta.RelationalDatabases.Db2fori
{

    public class DB2foriOperator : DatabaseOperatedBySQL, IIBMOperator
    {
        static string ConnectionString(string aIP, string aUserID, string aPassword) => $"Provider=IBMDA400;Data Source={aIP};User ID={aUserID};Password={aPassword};";//var cs = "Data Source=192.168.10.229;User ID=QUSER;Password=QUSER;";

        readonly string IP;
        readonly string UserID;
        readonly string Password;

        public DB2foriOperator(string aIP, string aUserID, string aPassword) : base(new OleDbObjectsFactory(ConnectionString(aIP, aUserID, aPassword)))
        {
            IP = aIP;
            UserID = aUserID;
            Password = aPassword;
        }

        public static DB2foriOperator Of(string aIP, string aUserID, string aPassword)
        {
            return new DB2foriOperator(aIP, aUserID, aPassword);
        }

        public static DB2foriOperator QuserOf(string aIP)
        {
            return Of(aIP, "QUSER", "QUSER");
        }

        static string IPofTest = "192.168.10.229";

        public static DB2foriOperator TestOf()
        {
            return QuserOf(IPofTest);
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
            return hexDt.Rows.Cast<DataRow>().Select(r => r.ItemArray[0] == null ? string.Empty : (string)r.ItemArray[0]);
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

        void IIBMOperator.ExecuteNonQuery(string nonQuery)
        {
            ExecuteNonQuery(nonQuery);
        }

        byte[] IIBMOperator.DownloadData(string aTargetLibraryName, string aTargetFileName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = new NetworkCredential(UserID, Password);
                return wc.DownloadData($"ftp://{IP}/{aTargetLibraryName}/{aTargetFileName}");
            }
        }

        List<byte[]> IIBMOperator.DownloadData(string aTargetLibraryName, string aTargetFileName, int fileLength)
        {
            var allBytes = ((IIBMOperator)this).DownloadData(aTargetLibraryName, aTargetFileName);
            return allBytes.Select((v, i) => new { v, i })
            .GroupBy(x => x.i / fileLength)
            .Select(g => g.Select(x => x.v)).Select(x => x.ToArray()).ToList();
        }

        public void DownloadFile(string aTargetLibraryName, string aTargetFileName, string aSaveFolderPath, string aSaveFileName, string aSaveFileExtension)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = new NetworkCredential(UserID, Password);

                Directory.CreateDirectory(aSaveFolderPath);

                try
                {
                    wc.DownloadFile($"ftp://{IP}/{aTargetLibraryName}/{aTargetFileName}", Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}"));
                }
                catch (Exception)
                {
                    wc.DownloadFile($"ftp://{IP}/{aTargetLibraryName}/{aTargetFileName}.TXT", Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}"));
                }
            }

        }

        public int UploadFile(string aTargetLibraryName, string aTargetFileName, string aSaveFolderPath, string aSaveFileName, string aSaveFileExtension)
        {
            //DeleteFile(aLibraryName,aFileName);

            var fileFullPath = Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}");

            if (!File.Exists(fileFullPath)) return 0;

            using (WebClient wc = new WebClient())
            {
                wc.Credentials = new NetworkCredential(UserID, Password);

                wc.UploadFile($"ftp://{IP}/{aTargetLibraryName}/{aTargetFileName}", fileFullPath);
            }

            return 1;

        }

    }

}

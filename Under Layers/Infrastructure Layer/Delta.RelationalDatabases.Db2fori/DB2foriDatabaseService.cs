using Delta.AS400.DataTypes.Characters;
using Delta.RelationalDatabases.OleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Db2fori
{
    public class DB2foriDatabaseService : RelationalDatabaseService
    {
        
        DB2foriDatabaseService(IDatabaseOperatedBySQL aDatabaseOperatedBySQL) :
            base(aDatabaseOperatedBySQL)
        {
        }

        //public static DB2foriDatabaseService Of(string aIP, string aUserID, string aPassword)
        //{
        //    var db2 = DB2foriOperatedBySQL(aIP, aUserID, aPassword);
        //    return Of(db2);
        //}

        public static DB2foriDatabaseService Of(IDatabaseOperatedBySQL aDatabaseOperatedBySQL)
        {
            return new DB2foriDatabaseService(aDatabaseOperatedBySQL);
        }

        //DB2foriDatabaseService(DB2foriOperatedBySQL aDB2foriDbContext) :base(aDB2foriDbContext, new Db2foriSchemaRepository(aDB2foriDbContext))
        //{
        //    DB2foriDbContext = aDB2foriDbContext;
        //}

        //public static DB2foriDatabaseService Of(DB2foriOperatedBySQL aDB2foriDbContext)
        //{
        //    return new DB2foriDatabaseService(aDB2foriDbContext);
        //}

        //public override void CreateTableLike(Table aSourceTable, Table aTargetTable)
        //{
        //    ExecuteNonQuery($"create table {aTargetTable.FullName} like {aSourceTable.FullName}");
        //}

        //List<T> IDatabaseOperatedBySQL.ExecuteReader<T>(string commandText, Func<DbDataReader, T> action)
        //{
        //    throw new NotImplementedException();
        //}

        //protected void ExecuteReader(string commandText, Action<DbDataReader> action)
        //        {
        //            using (var cn = new OleDbConnection(ConnectionString))
        //            {
        //                cn.Open();
        //                using (var command = new OleDbCommand())
        //                {

        //                    command.Connection = cn;
        //                    command.CommandType = CommandType.Text;

        //                    command.CommandText = commandText;
        //                    using (var reader = command.ExecuteReader())
        //                    {
        //                        //var x = reader.FieldCount;
        //                        //var y = reader.GetColumnSchema();

        //                        action(reader);

        //        reader.Close();
        //                    }

        //}
        //cn.Close();
        //            }

        //        }

        //public int GetFileLength(Table aTable)
        //{
        //    try
        //    {
        //        var hexDt = FillSchema(GetHexBytesCommandText(aTable));
        //        var fileLength = 0;
        //        for (int i = 0; i < hexDt.Columns.Count; i++)
        //        {
        //            fileLength += hexDt.Columns[i].MaxLength;
        //        }
        //        return fileLength / 2;

        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        //public IEnumerable<byte[]?> GetBytes(Table aTable)
        //{
        //    var dt = FillSchema(aTable);
        //    if (dt.Columns.Count == 1 && dt.Columns[0].DataType == typeof(byte[]))
        //    {
        //        return dt.AsEnumerable().Select(r => r.Field<byte[]>(0));
        //    }

        //    var hexDt = Fill(GetHexBytesCommandText(aTable));
        //    return hexDt.AsEnumerable().Select(r => CCSID930.ToBytesFrom(r.Field<string>(0)));
        //}

        //string GetHexBytesCommandText(Table aTable)
        //{
        //    var columnsCommandTxts = new List<string>();
        //    var dt = FillSchema(aTable);
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        var column = dt.Columns[i];
        //        columnsCommandTxts.Add($"HEX({column.ColumnName})");
        //    }
        //    return $"select {string.Join(" || ", columnsCommandTxts)} from {aTable.FullName}";
        //}


        //public void UploadFile(string aLibraryName, string aFileName, string aSaveFolderPath, string aSaveFileName, string aSaveFileExtension)
        //{

        //    //DeleteFile(aLibraryName,aFileName);

        //    //using (WebClient wc = new WebClient())
        //    //{
        //    //    wc.Credentials = new NetworkCredential(userID, password);

        //    //    //Directory.CreateDirectory(aSaveFolderPath);

        //    //    //try
        //    //    //{
        //    //        wc.UploadFile($"ftp://{IP}/{aLibraryName}/{aFileName}", Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}"));
        //    //    //}
        //    //    //catch (Exception)
        //    //    //{
        //    //    //    wc.UploadFile($"ftp://{IP}/{aLibraryName}/{aFileName}.TXT", Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}"));
        //    //    //}
        //    //}

        //    string upFile = Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}");
        //    Uri u = new Uri($"ftp://{IP}/{aLibraryName}/{aFileName}");

        //    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(u);
        //    ftpReq.Credentials = new NetworkCredential(userID, password);

        //    ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
        //    ftpReq.KeepAlive = false; //KeepAliveを無効
        //    ftpReq.UseBinary = true;   //バイナリモードで転送
        //    ftpReq.UsePassive = true;  //パッシブ接続にする
        //    ftpReq.Timeout = 10000;

        //    Stream reqStrm = ftpReq.GetRequestStream();
        //    var fs = new FileStream(upFile, FileMode.Open, FileAccess.Read);
        //    byte[] buffer = new byte[1024];
        //    while (true)
        //    {
        //        int readSize = fs.Read(buffer, 0, buffer.Length);
        //        if (readSize == 0) break;
        //        reqStrm.Write(buffer, 0, readSize);
        //    }
        //    fs.Close();
        //    reqStrm.Close();

        //    //FtpWebResponse ftpRes = (FtpWebResponse)ftpReq.GetResponse();
        //    //Console.WriteLine("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);
        //    //textBox1.Text += string.Format("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);
        //    //ftpRes.Close();

        //}

        //void DeleteFile(string aLibraryName, string aFileName)
        //{

        //    //削除するファイルのURI
        //    var u = new Uri($"ftp://{IP}/{aLibraryName}/{aFileName}");

        //    //FtpWebRequestの作成
        //    var ftpReq = (FtpWebRequest)WebRequest.Create(u);

        //    //ログインユーザー名とパスワードを設定
        //    ftpReq.Credentials = new NetworkCredential(userID, password);

        //    //MethodにWebRequestMethods.Ftp.DeleteFile(DELE)を設定
        //    ftpReq.Method = WebRequestMethods.Ftp.DeleteFile;

        //    //FtpWebResponseを取得
        //    var ftpRes = (FtpWebResponse)ftpReq.GetResponse();

        //    //FTPサーバーから送信されたステータスを表示
        //    Console.WriteLine("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);

        //    //閉じる
        //    ftpRes.Close();

        //}

        //public void DropSchema(string schemaName)
        //{
        //    ExecuteNonQuery($"drop schema {schemaName}");
        //}

        //public void DropTable(string tableName)
        //{
        //    ExecuteNonQuery($"drop table {tableName}");
        //}
        //void ICanBeOperatedBySQL.CreateSchema(Schema aSchema)
        //{
        //    //try
        //    //{
        //    ((ICanBeOperatedBySQL)this).ExecuteNonQuery($"create schema {aSchema.Name}");
        //    //}
        //    //catch (OleDbException ex)
        //    //{
        //    //    if (!ex.Message.StartsWith("SQL0601"))
        //    //    {
        //    //        throw;
        //    //    }
        //    //}
        //}

    }
}

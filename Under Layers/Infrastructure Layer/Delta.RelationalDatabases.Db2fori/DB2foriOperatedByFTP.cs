using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Db2fori
{
    [Obsolete("廃止予定です。　DB2foriOperatorを使ってください。")]
    public class DB2foriOperatedByFTP
    {
        readonly string IP;
        readonly string UserID;
        readonly string Password;

        DB2foriOperatedByFTP(string aIP, string aUserID, string aPassword)
        {
            IP = aIP;
            UserID = aUserID;
            Password = aPassword;
        }

        static string IPofTest = "192.168.10.229";

        public static DB2foriOperatedByFTP TestOf()
        {
            return new DB2foriOperatedByFTP(IPofTest, "QUSER", "QUSER");
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

            var fileFullPath= Path.Combine(aSaveFolderPath, $"{aSaveFileName}.{aSaveFileExtension}");

            if(!File.Exists(fileFullPath)) return 0;

            using (WebClient wc = new WebClient())
            {
                wc.Credentials = new NetworkCredential(UserID, Password);

                wc.UploadFile($"ftp://{IP}/{aTargetLibraryName}/{aTargetFileName}", fileFullPath);
            }

            return 1;

        }
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

        void DeleteFile(string aLibraryName, string aFileName)
        {

            //削除するファイルのURI
            var u = new Uri($"ftp://{IP}/{aLibraryName}/{aFileName}");

            //FtpWebRequestの作成
            var ftpReq = (FtpWebRequest)WebRequest.Create(u);

            //ログインユーザー名とパスワードを設定
            ftpReq.Credentials = new NetworkCredential(UserID, Password);

            //MethodにWebRequestMethods.Ftp.DeleteFile(DELE)を設定
            ftpReq.Method = WebRequestMethods.Ftp.DeleteFile;

            //FtpWebResponseを取得
            var ftpRes = (FtpWebResponse)ftpReq.GetResponse();

            //FTPサーバーから送信されたステータスを表示
            Console.WriteLine("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);

            //閉じる
            ftpRes.Close();

        }

    }
}

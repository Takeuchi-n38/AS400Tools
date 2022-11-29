using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace Util.Testing_Exceptall
{
    public class FileHelper
    {
        public static List<string> LoadInitSchemaScripts(DirectoryInfo targetInitFolder)
        {
            return LoadContentOfFiles(targetInitFolder, filePath => !filePath.Name.StartsWith("#unused#"));
        }
        public static List<string> LoadContentOfFiles(DirectoryInfo targetFolder, Func<FileInfo, bool> isTargetFilePath)
        {
            List<FileInfo> Files = targetFolder.GetFiles().OfType<FileInfo>().ToList();
            List<string> Scripts = new List<string>();
            Files.Where(scriptFilePath => scriptFilePath.Exists).Where(isTargetFilePath).ToList().ForEach(scriptFile =>
            {
                Scripts.Add(File.ReadAllText(scriptFile.FullName).ToString());
            });
            return Scripts;
        }

        public static List<string> LoadLines(string path)
        {
            List<string> rawLines;
            try
            {
                rawLines = File.ReadAllLines(path).ToList();
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
            return rawLines;
        }
    }
}

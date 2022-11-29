using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public static class FileHelper
    {
        public static void WriteAllText(string folderPath, string fileName, string extension, IEnumerable<string> contents)
        {
            WriteAllText(folderPath, $"{fileName}.{extension}", contents);
        }

        public static void WriteAllText(string folderPath, string fileName, string extension, string contents)
        {
            WriteAllText(folderPath, $"{fileName}.{extension}", contents);
        }

        public static void WriteAllText(string folderPath, string fileName, IEnumerable<string> contents)
        {
            var contentsAll = new StringBuilder();
            contents.ToList().ForEach(line => contentsAll.AppendLine(line));
            WriteAllText(folderPath, fileName, contentsAll.ToString());
        }

        public static void WriteAllText(string folderPath, string fileName, string contents)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var toPath = Path.Combine(folderPath, fileName);
            File.WriteAllText(toPath, contents);
        }

        public static void WriteAllText(string path, IEnumerable<string> contents)
        {
            var contentsAll = new StringBuilder();
            contents.ToList().ForEach(line => contentsAll.AppendLine(line));
            WriteAllText(path, contentsAll.ToString());
        }

        public static void WriteAllText(string path, string contents)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, contents);
        }

    }
}

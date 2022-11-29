using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO
{
    public static class DirectoryInfoExtension
    {

        public static DirectoryInfo CreateIfNotExists(this DirectoryInfo directoryInfo, string subDirectoryName)
        {
            return CreateIfNotExists(Path.Combine(directoryInfo.FullName, subDirectoryName));
        }

        public static DirectoryInfo CreateIfNotExists(string targetPath)
        {
            var di = new DirectoryInfo(targetPath);

            if (di.Exists == false) di.Create();

            return di;
        }

    }
}

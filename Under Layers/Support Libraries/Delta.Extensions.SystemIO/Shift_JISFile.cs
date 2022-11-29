using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public class Shift_JISFile
    {
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path, EncodingExtension.Shift_JIS);
        }

        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path, EncodingExtension.Shift_JIS);
        }

    }
}

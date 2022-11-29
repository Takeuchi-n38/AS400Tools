using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Koubai01;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Tools.Modernization.Test
{
    public class TestFilesHelperTest
    {
        static Library MainLibrary = Honsha01LibraryList.Iidlib;
        static PathResolver pathResolver = PathResolver.Of($"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta", MainLibrary.Partition.Name, MainLibrary.Name);

        [Fact]
        public void Test1()
        {
            var caseName = "1";
            var testFilesHelper = TestFilesHelper.ForServiceOf(pathResolver, TestTarget.Of(Honsha01LibraryList.Iielib.ObjectIDOf("PIIE015")));

            var originals = testFilesHelper.ReadBytesFromSetupFolder(caseName, Honsha01LibraryList.Iielib.ObjectIDOf("RLSCTL")).ToList();
            var updates = System.BytesExtensions.ReplacePart(originals,16,new byte[] { 0xF2, 0xF0, 0xF0, 0xF3, 0xF0, 0xF1, 0xF0, 0xF2, });
            testFilesHelper.WriteAllBytesToActualFolder(caseName,updates, Honsha01LibraryList.Iielib.ObjectIDOf("RLSCTL"));
        }

        //List<byte[]> ReplacePart(List<byte[]> originals, int replaceStartIndex,byte[] replaceBytes)
        //{
        //    var updates = new List<byte[]>();

        //    originals.ToList().ForEach(original =>
        //    {
        //        var update = new byte[original.Length];
        //        Array.Copy(original, update, update.Length);
        //        Array.Copy(replaceBytes, 0, update, replaceStartIndex, replaceBytes.Length);
        //        updates.Add(update);
        //    });
        //    return updates;
        //}
    }
}

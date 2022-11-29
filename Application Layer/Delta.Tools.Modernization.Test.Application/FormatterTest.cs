using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Test
{
    public abstract class FormatterTest : CompornentTest
    {
        //protected abstract EntityTestHelper SetupEntityTestHelperOf(string caseName);
        //protected abstract EntityTestHelper ExpectedEntityTestHelperOf(string caseName);

        protected virtual EntityTestHelper SetupEntityTestHelperOf(string caseName) => throw new NotImplementedException();//Honsha01IidlibOrddtaTestHelper.Of;

        protected virtual EntityTestHelper ExpectedEntityTestHelperOf(string caseName) => throw new NotImplementedException();//Honsha01IidlibOrddtaTestHelper.Of;

        protected override void SetupEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.Add(SetupEntityTestHelperOf(caseName));
        }

        protected override void ExpectedEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers)
        {
            entityTestHelpers.Add(SetupEntityTestHelperOf(caseName));
        }

        protected IEnumerable<byte[]> ReadBytesFromSetupFolder(string caseName) => TargetTestHelper.ReadBytesFromSetupFolder(caseName, SetupEntities(caseName).FirstOrDefault());
        protected void WriteAllBytesToActualFolder(string caseName, IEnumerable<byte[]> aBytes) => TargetTestHelper.WriteAllBytesToActualFolder(caseName, aBytes, SetupEntities(caseName).FirstOrDefault());

        protected IEnumerable<byte[]> ReadBytesFromExpectedFolder(string caseName) => TargetTestHelper.ReadBytesFromExpectedFolder(caseName, ExpectedEntities(caseName).FirstOrDefault());

    }

}

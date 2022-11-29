using System.Collections.Generic;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCaseList
    {

        public ConversionProgram conversionProgram;
        private DBAssertionTestCaseList(ConversionProgram aConversionProgram)
        {
            conversionProgram = aConversionProgram;
            items = new List<DBAssertionTestCase>();
        }

        public static DBAssertionTestCaseList CreateBy(ConversionProgram aConversionProgram, int countOfTestCase)
        {
            var instance = new DBAssertionTestCaseList(aConversionProgram);
            for (int i = 0; i < countOfTestCase; i++)
            {
                instance.AddItem();
            }
            return instance;
        }

        public List<DBAssertionTestCase> items ;
        DBAssertionTestCaseList AddItem()
        {
            var dcsTestCase = new DBAssertionTestCase(this);
            items.Add(dcsTestCase);
            return this;
        }

        public DBAssertionTestCase GetDcsTestCase(int caseNo)
        {
            return items[caseNo - 1];
        }

    }
}

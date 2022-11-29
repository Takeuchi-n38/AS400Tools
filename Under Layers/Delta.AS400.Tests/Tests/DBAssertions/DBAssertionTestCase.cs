using System.IO;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCase
    {

        DBAssertionTestCaseList binder;

        int caseNo;

        public DBAssertionTestCaseDifinition dcsTestCaseDifinitionOfSetup;
        public DBAssertionTestCaseDifinition dcsTestCaseDifinitionOfExpected;
        public DBAssertionTestCase(DBAssertionTestCaseList aBinder)
        {
            binder = aBinder;
            caseNo = aBinder.items.Count + 1;
            dcsTestCaseDifinitionOfSetup = new DBAssertionTestCaseDifinition(this, DBAssertionTestCaseType.setup);
            dcsTestCaseDifinitionOfExpected = new DBAssertionTestCaseDifinition(this, DBAssertionTestCaseType.expected);
        }

        public string TestCaseId()
        {
            return binder.conversionProgram.programSchema.ToLower() + "_" + binder.conversionProgram.programId + "_test" + caseNo;
        }

        public string TestFolderPath()
        {
            var currentModulePath = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp3.1", string.Empty);
            var programNameSpace = binder.conversionProgram.programPackage.Split(".");
            currentModulePath += "\\Resouces\\" + programNameSpace[^3] + "\\" + programNameSpace[^2] + "\\" + programNameSpace[^1].ToLower();
            currentModulePath += "\\testForConversion\\";
            return currentModulePath;
        }

        public string TestCaseFolderPath()
        {
            return TestFolderPath() + "case" + caseNo;
        }

        public string ExecuteLogFilePath()
        {
            return TestCaseFolderPath() + "\\executeLog.txt";
        }

        public string ActualFolderPath()
        {
            return TestCaseFolderPath() + "\\actual";
        }

        public string ExpectedFolderPath()
        {
            return TestCaseFolderPath() + "\\expected";

        }

    }

}

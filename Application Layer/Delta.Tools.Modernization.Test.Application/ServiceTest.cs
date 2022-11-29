using Delta.RelationalDatabases;
using System.Collections.Generic;

namespace Delta.Tools.Modernization.Test
{

    public abstract class ServiceTest : CompornentTest
    {

        protected virtual void InsertTestDataCsvToSetupAndExpectedSchemaOf(string caseName) => TargetTestHelper.InsertTestDataCsvToSetupAndExpectedSchema(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));
        protected virtual void InsertTestDataCsvToSetupAndExpectedPostgresSchemaOf(string caseName) => TargetTestHelper.InsertTestDataCsvToSetupAndExpectedPostgresSchema(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));

        protected virtual void InsertTestDataBinarySetupAndExpectedSchemaOf(string caseName) => TargetTestHelper.InsertTestDataBinarySetupAndExpectedSchema(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));
        protected virtual void InsertTestDataBinarySetupAndExpectedPostgresSchemaOf(string caseName) => TargetTestHelper.InsertTestDataBinarySetupAndExpectedPostgresSchema(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));


        protected virtual void InsertSetupTablesToActualTablesOf(string caseName,IEnumerable<Table> actualTables) => TargetTestHelper.InsertSetupTablesToActualTables(caseName, actualTables);
        protected virtual void InsertSetupTablesToActualPostgresTablesOf(string caseName, IEnumerable<Table> actualTables) => TargetTestHelper.InsertSetupPostgresTablesToActualTables(caseName, actualTables);

        protected virtual void InsertSetupTablesToActualTablesOf(string caseName) => TargetTestHelper.InsertSetupTablesToActualTables(caseName, SetupEntityTestHelpersOf(caseName));
        protected virtual void InsertSetupTablesToActualPostgresTablesOf(string caseName) => TargetTestHelper.InsertSetupPostgresTablesToActualTables(caseName, SetupEntityTestHelpersOf(caseName));

        protected virtual long DifferenceCountOfExceptView(IEnumerable<Table> expectedTables) => TargetTestHelper.DifferenceCountOfExceptView(expectedTables);
        protected virtual long DifferenceCountOfExceptPostgresView(IEnumerable<Table> expectedTables) => TargetTestHelper.DifferenceCountOfExceptPostgresView(expectedTables);

        protected virtual long DifferenceCountOfExceptView(string caseName) => TargetTestHelper.DifferenceCountOfExceptView(caseName, ExpectedEntityTestHelpersOf(caseName));
        protected virtual long DifferenceCountOfExceptPostgresView(string caseName) => TargetTestHelper.DifferenceCountOfExceptPostgresView(caseName, ExpectedEntityTestHelpersOf(caseName));

        //protected virtual void AssertEqualByExceptView(string caseName) => Assert.Equal(0, DifferenceCountOfExceptView(caseName));

        //public static void AssertEqual(IEnumerable<string> expected, IEnumerable<string> actual)
        //{

        //    for (int i = 0; i < Math.Min(expected.Count(), actual.Count()); i++)
        //    {
        //        Assert.Equal(expected.ElementAt(i), actual.ElementAt(i));
        //    }

        //    Assert.Equal(expected.Count(), actual.Count());

        //}

        //public static void AssertEqual(IEnumerable<byte[]> expected, IEnumerable<byte[]> actual)
        //{
        //    //Assert.Equal(expected, actual);

        //    for (int i = 0; i < Math.Min(expected.Count(), actual.Count()); i++)
        //    {
        //        var curExpected = expected.ElementAt(i);
        //        var curActual = actual.ElementAt(i);
        //        //Assert.Equal(curExpected, curActual);

        //        for (int j = 0; j < Math.Min(curExpected.Count(), curActual.Count()); j++)
        //        {
        //            Assert.Equal(curExpected.ElementAt(j), curActual.ElementAt(j));
        //        }
        //        Assert.Equal(curExpected.Count(), curActual.Count());
        //    }

        //    Assert.Equal(expected.Count(), actual.Count());

        //}
    }
}

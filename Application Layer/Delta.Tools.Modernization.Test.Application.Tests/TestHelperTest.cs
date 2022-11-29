using Delta.Honsha01;
using Delta.Honsha01.Iidlib.Carsets;
using Delta.Koubai01;
//using Delta.RelationalDatabases.Mssql;
//using Delta.Koubai01.Salelib.Denpfls;
using Delta.RelationalDatabases.Postgres;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using Xunit;

namespace Delta.Tools.Modernization.Test
{
    public class TestHelperTest
    {

        //[Fact]
        //public void InsertTestDataCsvToSetupAndExpectedSchemaTest()
        //{
        //    var caseName="1";
        //    TestHelper TestHelperInstance = Koubai01SalelibTestHelper.Of(TestTarget.Of(Koubai01LibraryList.Salelib.ObjectIDOf("PQEA051")));
        //    TestHelperInstance.InsertTestDataCsvToSetupAndExpectedSchema(caseName, new List<EntityTestHelper>(){DenpflTestHelper.Of }, new List<EntityTestHelper>() { DenpflTestHelper.Of });
        //}

        //[Fact]
        //public void Test1()
        //{
        //    var caseName = "1";
        //    TestHelper TestHelperInstance = Koubai01SalelibTestHelper.Of(TestTarget.Of(Koubai01LibraryList.Salelib.ObjectIDOf("PQEA051")));
        //    TestHelperInstance.InsertSetupTablesToActualTables(caseName, new List<EntityTestHelper>() { DenpflTestHelper.Of });
        //}

        //[Fact]
        //public void ConvertTestDataBinaryToCsvTest1()
        //{
        //    var caseName = "1";
        //    TestHelper TestHelperInstance = TestHelper.ForServiceOf(TestTarget.Of(Honsha01LibraryList.Iidlib.ObjectIDOf("PAIC004")), 
        //        NpgsqlConnectionStringBuilderFactory.ConnectionStringBilderOfLocalTest("h1iid"), 
        //        PathResolver.Of($"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta", Honsha01LibraryList.Iidlib));

        //    TestHelperInstance.ConvertTestDataBinaryToCsv(caseName,new List<EntityTestHelper>(){ Asp16zTestHelper.Of }, new List<EntityTestHelper>() { Asp16zTestHelper.Of });

        //}

        //[Fact]
        //public void InsertTestDataCsvToSetupAndExpectedSchemaTest1()
        //{
        //    var caseName = "1";
        //    TestHelper TestHelperInstance = TestHelper.ForServiceOf(TestTarget.Of(Honsha01LibraryList.Iidlib.ObjectIDOf("PAIC004")),
        //        NpgsqlConnectionStringBuilderFactory.ConnectionStringBilderOfLocalTest("h1iid"),
        //        PathResolver.Of($"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta", Honsha01LibraryList.Iidlib));

        //    TestHelperInstance.InsertTestDataCsvToSetupAndExpectedSchema(caseName, new List<EntityTestHelper>() { Asp16zTestHelper.Of }, new List<EntityTestHelper>() { Asp16zTestHelper.Of });

        //}

        static readonly TestTarget TestTarget = TestTarget.Of(Honsha01LibraryList.Iidlib.ObjectIDOf("PIID074"));
        public static readonly TestHelper TestHelperInstance = Honsha01IidlibTestHelper.Of(TestTarget);

        [Fact]
        public void InsertTestDataCsvToSetupAndExpectedSchemaTest1()
        {
            var caseName = "1";

            TestHelperInstance.InsertTestDataBinarySetupAndExpectedSchema(caseName, new List<EntityTestHelper>() { CarsetTestHelper.Of }, new List<EntityTestHelper>() { CarsetTestHelper.Of });

        }
    }
}

using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Delta.AS400;
using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.RelationalDatabases.Postgres;
using System.Data.Common;
using System.IO;
using Delta.Tools.Modernization;
using Delta.Tools.Modernization.Test;
using Delta.Tools.Modernization.Test.Adapters;
using Delta.RelationalDatabases.Mssql;

namespace Delta
{
    public partial class Honsha01IidlibTestHelper
    {
        static Library MainLibrary = Honsha01LibraryList.Iidlib;
        static PathResolver pathResolver = PathResolver.Of($"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta", MainLibrary.Partition.Name,MainLibrary.Name);

        static string DatabaseName = "h1iid";
        public static DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBuilderOfLocalTest(DatabaseName);
        public static DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder 
            = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("(local)", "SQLEXPRESs", "TRIM_SYSTEM");
        //public static IHonsha01IidlibDependencyInjector DependencyInjector = Honsha01IidlibDependencyInjectorForTest.Of();


        public static TestHelper Of(TestTarget aTestTarget)
        {
            return TestHelper.ForServiceOf(aTestTarget, LocalTestPostgresConnectionStringBuilder, LocalTestMssqlConnectionStringBuilder,pathResolver); 
        }

        public static TestHelper ForFormatterOf(TestTarget aTestTarget)
        {
            return TestHelper.ForFormatterOf(aTestTarget, LocalTestPostgresConnectionStringBuilder, LocalTestMssqlConnectionStringBuilder,pathResolver);
        }

    }
}

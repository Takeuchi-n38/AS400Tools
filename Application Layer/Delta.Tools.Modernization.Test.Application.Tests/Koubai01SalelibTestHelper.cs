using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.AS400.Libraries;
using Delta.Koubai01;
using System.Data.Common;
using Delta.RelationalDatabases.Postgres;
using Delta.AS400;
using Delta.Tools.Modernization.Test;
using System.IO;
using Delta.RelationalDatabases.Mssql;

namespace Delta.Tools.Modernization.Test
{

    public partial class Koubai01SalelibTestHelper
    {
        static Library MainLibrary = Koubai01LibraryList.Salelib;
        static PathResolver pathResolver = PathResolver.Of($"{Path.GetPathRoot(Environment.CurrentDirectory)}Delta",MainLibrary.Partition.Name,MainLibrary.Name);

        static string DatabaseName = "k1sale";
        public static DbConnectionStringBuilder LocalTestPostgresConnectionStringBuilder = NpgsqlConnectionStringBuilderFactory.ConnectionStringBuilderOfLocalTest(DatabaseName);
        //public static IKoubai01SalelibDependencyInjector DependencyInjector = Koubai01SalelibDependencyInjector.Of(LocalTestPostgresConnectionStringBuilder);
        
        public static DbConnectionStringBuilder LocalTestMssqlConnectionStringBuilder
    = MssqlConnectionStringBuilderFactory.ConnectionStringBuilder("(local)", "SQLEXPRESs", "TRIM_SYSTEM");

        public static TestHelper Of(TestTarget aTestTarget)
        {
            return TestHelper.ForServiceOf(aTestTarget, LocalTestPostgresConnectionStringBuilder, LocalTestMssqlConnectionStringBuilder, pathResolver);
        }
        public static TestHelper ForFormatterOf(TestTarget aTestTarget)
        {
            return TestHelper.ForFormatterOf(aTestTarget, LocalTestPostgresConnectionStringBuilder, LocalTestMssqlConnectionStringBuilder, pathResolver);
        }

    }
}

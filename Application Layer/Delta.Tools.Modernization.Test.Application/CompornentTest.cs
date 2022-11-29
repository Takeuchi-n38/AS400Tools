using Delta.AS400.Objects;
using Delta.RelationalDatabases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Test
{
    public abstract class CompornentTest
    {
        Stopwatch sw = new Stopwatch();
        protected void RestartStopwatch() => sw.Restart();
        protected void StopStopwatch() => sw.Stop();

        protected long ElapsedMilliseconds => sw.ElapsedMilliseconds;

        protected void WriteElapsedTime() => WriteOnTestExploler($"{ElapsedMilliseconds / 1000 / 60} minutes ({ElapsedMilliseconds} mm sec {DateTime.Now:yyyy/MM/dd HH:mm:ss})");

        protected virtual void WriteOnTestExploler(string value)
        {
            throw new NotImplementedException();
        }

        protected abstract TestHelper TargetTestHelper { get; }
        protected virtual List<EntityTestHelper> SetupEntityTestHelpersOf(string caseName)
        {
            var entityTestHelpers = new List<EntityTestHelper>();
            SetupEntityTestHelpersOf(caseName, entityTestHelpers);
            return entityTestHelpers;
        }

        protected virtual List<EntityTestHelper> ExpectedEntityTestHelpersOf(string caseName)
        {
            var entityTestHelpers = new List<EntityTestHelper>();
            ExpectedEntityTestHelpersOf(caseName, entityTestHelpers);
            return entityTestHelpers;
        }

        protected virtual void SetupEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers) => new List<EntityTestHelper>();
        protected virtual void ExpectedEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers) => new List<EntityTestHelper>();

        protected virtual IEnumerable<ObjectID> SetupEntities(string caseName) => SetupEntityTestHelpersOf(caseName).Select(t => t.ObjectID);
        protected virtual IEnumerable<ObjectID> ExpectedEntities(string caseName) => ExpectedEntityTestHelpersOf(caseName).Select(t => t.ObjectID);
        protected virtual IEnumerable<Table> ExpectedTables(string caseName) => ExpectedEntities(caseName).Select(o => TargetTestHelper.ExpectedSchema(caseName).CreateTableOf(o.Name));

        protected virtual void DownloadTestDataBinaryOf(string caseName) => TargetTestHelper.DownloadTestDataBinary(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));

        protected virtual void ConvertTestDataBinaryToCsvOf(string caseName) => TargetTestHelper.ConvertTestDataBinaryToCsv(caseName, SetupEntityTestHelpersOf(caseName), ExpectedEntityTestHelpersOf(caseName));

    }

}

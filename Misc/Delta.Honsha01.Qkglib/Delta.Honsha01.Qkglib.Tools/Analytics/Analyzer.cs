using Delta.Honsha01.Qkglib.Tools.Configs;
using Delta.Tools.Modernization.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Honsha01.Qkglib.Tools.Analytics
{
    public class Analyzer
    {
        [Fact]
        public void Analyze()
        {
            AnalyzerService.Main(Honsha01QkglibConfig.Of());
        }
    }
}

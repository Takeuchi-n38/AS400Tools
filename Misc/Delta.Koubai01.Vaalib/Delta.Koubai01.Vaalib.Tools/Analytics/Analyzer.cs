﻿using Delta.Koubai01.Vaalib.Tools.Configs;
using Delta.Tools.Modernization.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Koubai01.Vaalib.Tools.Analytics
{
    public class Analyzer
    {
        [Fact]
        public void Analyze()
        {
            AnalyzerService.Main(Koubai01VaalibConfig.Of());
        }
    }
}
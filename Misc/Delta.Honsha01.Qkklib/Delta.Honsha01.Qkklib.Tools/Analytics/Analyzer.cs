﻿using Delta.Honsha01.Qkklib.Tools.Configs;
using Delta.Tools.Modernization.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Honsha01.Qkklib.Tools.Analytics
{
    public class Analyzer
    {
        [Fact]
        public void Analyze()
        {
            AnalyzerService.Main(Honsha01QkklibConfig.Of());
        }
    }
}

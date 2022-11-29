﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public interface IRecordFormatOutputField
    {
        int Line { get; }

        int Position { get; }

        string Color { get; set; }

        string IndicatorValueString { get; }

        bool HasIndicator { get; }

    }
}

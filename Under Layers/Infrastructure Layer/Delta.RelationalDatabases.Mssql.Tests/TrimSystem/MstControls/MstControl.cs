using System;
using System.Collections.Generic;

namespace Delta.TrimSystem.MstControls
{
    public partial class MstControl
    {
        public string ControlId { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public string? Value4 { get; set; }
        public string? Value5 { get; set; }
        public string? Memo { get; set; }
    }
}

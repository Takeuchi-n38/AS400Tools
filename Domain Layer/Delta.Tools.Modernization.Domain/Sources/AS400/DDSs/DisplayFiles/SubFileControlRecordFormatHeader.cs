using Delta.Tools.AS400.DDSs.RecordFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public class SubFileControlRecordFormatHeader : RecordFormatHeader
    {
        public readonly string SubFileRecordName;
        public SubFileControlRecordFormatHeader(string PublicModernName, string SubFileRecordName) : base(PublicModernName)
        {
            this.SubFileRecordName = SubFileRecordName;
        }

        public override int LineSpan => base.LineSpan + 17;

    }
}

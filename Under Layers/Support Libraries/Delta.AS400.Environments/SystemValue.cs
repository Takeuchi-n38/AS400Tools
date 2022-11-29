using Delta.Types.Dates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.Environments
{
    public class SystemValue
    {
        readonly DateTime? qdatetime;

        public DateTime Qdatetime => qdatetime.HasValue ? qdatetime.Value : DateTime.Now ;

        public SystemValue(DateTime? aQdatetime)
        {
            this.qdatetime = aQdatetime;
        }

        public static SystemValue Of(DateTime? aQdatetime) => new SystemValue(aQdatetime);

        public static SystemValue Of() => new SystemValue(null);

        public int yyyyMMddOfNow => int.Parse(Qdatetime.ToString("yyyyMMdd"));

        public string Qdate => Qdatetime.ToString("yyMMdd");

        public Date QdateInDateFormat => Date.Of(Qdatetime);

        public string Qtime => Qdatetime.ToString("HHmmss");

        public long Time => long.Parse(Qdatetime.ToString("HHmmssyyyyMMdd"));
        public short HHmmOfTimeInShort => short.Parse(Qdatetime.ToString("HHmm"));

    }
}

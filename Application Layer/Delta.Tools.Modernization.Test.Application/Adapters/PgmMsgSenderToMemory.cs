using Delta.AS400.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Test.Adapters
{
    public class PgmMsgSenderToMemory : IPgmMsgSender
    {
        public List<string> Msgs = new List<string>();
        public void Send(string msgdta)
        {
            Msgs.Add(msgdta);
        }

        public static PgmMsgSenderToMemory Of => new PgmMsgSenderToMemory();

    }
}

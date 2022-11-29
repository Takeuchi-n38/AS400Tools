using Delta.AS400.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Test.Adapters
{
    public class MsgSenderToMemory : IMsgSender
    {
        public List<(string msg, string tomsgq)> Msgs = new List<(string msg, string tomsgq)>();

        public void Send(string msg, string tomsgq)
        {
            Msgs.Add((msg, tomsgq));
        }

        public static MsgSenderToMemory Of => new MsgSenderToMemory();
    }
}

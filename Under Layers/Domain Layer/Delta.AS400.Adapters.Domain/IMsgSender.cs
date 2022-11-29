using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Adapters
{
    public interface IMsgSender
    {
        void Send(string msg,string tomsgq);
    }
}

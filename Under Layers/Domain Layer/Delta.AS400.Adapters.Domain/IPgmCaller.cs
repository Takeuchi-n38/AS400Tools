using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.Adapters
{
    public interface IPgmCaller
    {
        void Call(string pgm);

        object[] Call(string pgm, object[] parm);

        void SndUsrInfMsg(string msg);

    }

}

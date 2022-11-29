using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.Workstations.Sndusrmsgs
{
    class SndUsrInqMsgPlist
    {
        internal static SndUsrInqMsgPlist Of(string msg, string dft)
        {
            return new SndUsrInqMsgPlist()
            {
                Msg = msg,
                Dft = dft,
            };
        }
        public string Msg { get; private set; }

        public string Dft { get; private set; }

    }
}

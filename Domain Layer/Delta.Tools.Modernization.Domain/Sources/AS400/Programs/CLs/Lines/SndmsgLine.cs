using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SndmsgLine : CLLine
    {
        public readonly string Msg;
        public readonly string Tomsgq;
        public SndmsgLine(string aMsg, string aTomsgq, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Msg = aMsg;
            this.Tomsgq= aTomsgq;
        }
        //0294 SNDMSG     MSG('トリム品番配列に構成モレ有。エラーリストがSEATOUTQにあるので確認して下さい。(JLFB10D0/PLFB260)') TOMSGQ(OPOP)
    }
}

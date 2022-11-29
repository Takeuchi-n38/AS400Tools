using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Lines
{
    //Lineは意味は問わず、複数Lineから行結合記号や改行を除外し１行につなげた値
    public interface ILine 
    {
        string Value { get; }//複数Lineから行結合記号や改行を除外し１行につなげた値

        int StartLineIndex { get; }

        int EndLineIndex { get; }

        bool IsJoined => StartLineIndex != EndLineIndex;
    }
}

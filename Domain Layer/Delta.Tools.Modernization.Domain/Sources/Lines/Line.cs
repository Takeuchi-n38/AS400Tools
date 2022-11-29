using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Lines
{
    public class Line : ILine
    {

        public string Value { get; }//複数Lineから行結合記号や改行を除外し１行につなげた値

        public int StartLineIndex { get; }
        public string StartLineIndexD4 => StartLineIndex.ToString("D4");

        public int EndLineIndex { get; }

        protected Line(string value, int startLineIndex, int endLineIndex)
        {
            Value = value;
            StartLineIndex = startLineIndex;
            EndLineIndex = endLineIndex;
        }

        protected Line(string value, int startLineIndex) : this(value, startLineIndex, startLineIndex)
        {

        }

        protected Line(int startLineIndex) : this(string.Empty, startLineIndex)
        {

        }

    }
}

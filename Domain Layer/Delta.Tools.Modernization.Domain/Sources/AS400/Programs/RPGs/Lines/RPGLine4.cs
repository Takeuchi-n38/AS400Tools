using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Lines
{
    public class RPGLine4 : Line, IRPGLine4
    {
        readonly FormType FormType;
        FormType IRPGLine.FormType => FormType;

        public static int MaxLengthOfLine = 80;

        protected RPGLine4(FormType FormType, string value, int originalLineStartIndex, int originalLineEndIndex) : base(value.Length < MaxLengthOfLine ? value.PadRight(MaxLengthOfLine) : value, originalLineStartIndex, originalLineEndIndex)
        {
            this.FormType = FormType;
        }

        public RPGLine4(FormType FormType, string value, int originalLineStartIndex) : this(FormType, value, originalLineStartIndex, originalLineStartIndex)
        {

        }
    }
}
/*
     OFILEB   D        01 10
     O                         DATA     128
     O                                  108 '    '
     OFILEC   D        01 20
     O                         DATA     128
     O                                  108 '    '
 */
/*
     OQPRINT    E            #HED             03
     O                                           10 'PQEA050'
     O                                           74 'オーダー売上リスト'

 * */
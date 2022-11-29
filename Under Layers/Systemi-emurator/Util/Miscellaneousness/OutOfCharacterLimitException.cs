using System;

namespace Util.Miscellaneousness
{
    public class OutOfCharacterLimitException : SystemException
    {
        private static readonly string MESSAGE = "文字数が制限を超えています。";
        private static readonly string CAUSE = "最大文字数：%d セットされた文字数：%d";

        public OutOfCharacterLimitException(int limitLength, int actuallyLength) : base(MESSAGE, new Exception(String.Format(CAUSE, limitLength, actuallyLength)))
        {
        }
    }
}

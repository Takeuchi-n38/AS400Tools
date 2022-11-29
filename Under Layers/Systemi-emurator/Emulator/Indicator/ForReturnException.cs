using System;

namespace Systemi_emurator.Emulator.Indicator
{
    // @SuppressWarnings("serial")
    public class ForReturnException : SystemException
    {
        public ForReturnException(string message) : base(message)
        {
        }
        public ForReturnException() : base()
        {
        }
    }
}

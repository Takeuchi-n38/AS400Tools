using System;
namespace Systemi_emurator.Emulator.Indicator
{
    //@SuppressWarnings("serial")
    public class ReturnByHalt : SystemException
    {
        public ReturnByHalt(string message) : base(message)
        {
        }
        public ReturnByHalt() : base()
        {
        }
    }
}

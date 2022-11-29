using System;

namespace Util.Miscellaneousness
{
    public class OurSystemException : SystemException
    {
        public OurSystemException(Exception e) : base()
        {
        }

        public OurSystemException(String message) : base(message)
        {
        }
    }
}

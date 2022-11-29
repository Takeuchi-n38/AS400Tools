using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items
{
    public interface AccessModifier
    {
        //protected, 
        //protected internal,
        //private protected

    }

    public class PublicAccessModifier : AccessModifier
    {
        public static AccessModifier instance = new PublicAccessModifier();
        public override string ToString()
        {
            return "public";
        }
    }
    public class PrivateAccessModifier : AccessModifier
    {
        public static AccessModifier instance = new PrivateAccessModifier();

        public override string ToString()
        {
            return "private";
        }
    }
    public class InternalAccessModifier : AccessModifier
    {
        public static AccessModifier instance = new InternalAccessModifier();

        public override string ToString()
        {
            return "internal";
        }
    }
    public class DefaultAccessModifier : AccessModifier
    {
        public static AccessModifier instance = new DefaultAccessModifier();

        public override string ToString()
        {
            return string.Empty;
        }
    }
}

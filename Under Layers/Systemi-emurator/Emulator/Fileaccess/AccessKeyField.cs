using System;

namespace Systemi_emurator.Fileaccess
{
    public class AccessKeyField : AccessKeyDefinition
    {
        public Object value;

        public AccessKeyField(AccessKeyDefinition keyDefinition, Object keyValue) : base(keyDefinition)
        {
            value = keyValue;
        }

        public static AccessKeyField CreateBy(AccessKeyDefinition keyDefinition, Object keyValue)
        {
            return new AccessKeyField(keyDefinition, keyValue);
        }
    }
}

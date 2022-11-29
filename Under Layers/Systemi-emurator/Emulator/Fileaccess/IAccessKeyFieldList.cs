using System;
using System.Collections.Generic;

namespace Systemi_emurator.Fileaccess
{
    public interface IAccessKeyFieldList
    {
        internal List<object> GetAccessKeyValues()
        {
            List<Object> accessKeyValues = new List<Object>();
            PassAccessKeyValueTo(accessKeyValues);
            return accessKeyValues;
        }
        public void PassAccessKeyValueTo(List<object> accessKeyValueCollector);
    }

}

using System;
using System.Collections.Generic;
using Systemi_emurator.Fileaccess;

namespace Systemi_emurator.fileaccess
{
    public class AccessKeyDefinitions
    {
        public List<AccessKeyDefinition> accessKeyDefinitions;
        public readonly string orderByClause;
        public String GetOrderByClause()
        {
            return orderByClause;
        }

        public AccessKeyDefinitions(List<AccessKeyDefinition> accessKeyDefinitions)
        {

            this.accessKeyDefinitions = accessKeyDefinitions;
            List<string> valueOrderby = new List<string>();
            accessKeyDefinitions.ForEach(a =>
            {
                valueOrderby.Add(a.Orderbyclause());
            });

            this.orderByClause = string.Join(",", valueOrderby);


        }

        internal List<AccessKeyField> CreateAccessKeyFieldsBy(List<object> accessKeyValues)
        {
            List<AccessKeyField> accessKeys = new List<AccessKeyField>();
            for (int i = 0; i < accessKeyValues.Count; i++)
            {
                accessKeys.Add(AccessKeyField.CreateBy(accessKeyDefinitions[i], accessKeyValues[i]));
            }
            return accessKeys;
        }


    }
}
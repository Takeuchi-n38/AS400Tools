using System;

namespace Systemi_emurator.Fileaccess
{

    public class AccessKeyDefinition
    {
        public string name = String.Empty;
        public AccessOrderType order;
        private AccessKeyDefinition(string name, AccessOrderType order)
        {
            this.name = name;
            this.order = order;
        }

        protected AccessKeyDefinition(AccessKeyDefinition accessKeyDefinition)
        {
            this.name = accessKeyDefinition.name;
            this.order = accessKeyDefinition.order;
        }

        public static AccessKeyDefinition CreateAscKeyBy(string name)
        {
            return new AccessKeyDefinition(name, AccessOrderType.ASC);
        }

        public static AccessKeyDefinition CreateDescKeyBy(string name)
        {
            return new AccessKeyDefinition(name, AccessOrderType.DESC);
        }


        public enum AccessOrderType
        {
            ASC, DESC
        }
        public static AccessOrderType CreateBy(string order)
        {
            return order.ToLower() == "desc" ? AccessOrderType.DESC : AccessOrderType.ASC;
        }

        public string GreaterThanOperator()
        {
            return this.order.Equals(AccessOrderType.ASC) ? " >" : " <";
        }

        public string LessThanOperator()
        {
            return this.order.Equals(AccessOrderType.ASC) ? " <" : " >";
        }

        public string Orderbyclause()
        {
            return name + " " + order.ToString().ToLower();
        }
        public string GreaterThanCondition()
        {
            return name.Replace("_ebcdic", "") + GreaterThanOperator();
        }

        private string equalToOperator = " =";
        public string equalToCondition()
        {
            return name.Replace("_ebcdic", "") + equalToOperator;
        }

        public string LessThanCondition()
        {
            return name.Replace("_ebcdic", "") + LessThanOperator();
        }

    }
}


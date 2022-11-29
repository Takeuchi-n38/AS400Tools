using System;
using System.Collections.Generic;

namespace Systemi_emurator.Fileaccess
{
    public abstract class CriteriaForAccessPath
    {
        public abstract void AddCriterion(String condition, Object value, String property);

        public void AddEqualToCriterionBy(AccessKeyField keyField)
        {
            AddCriterion(keyField.equalToCondition(), keyField.value, keyField.name);
        }
        public void AddEqualToCriterionBy(List<AccessKeyField> keyFields)
        {
            keyFields.ForEach(AddEqualToCriterionBy);
        }

        public void AddGreaterThanCriterionBy(AccessKeyField keyField)
        {
            AddCriterion(keyField.GreaterThanCondition(), keyField.value, keyField.name);
        }

        public void AddLessThanCriterionBy(AccessKeyField keyField)
        {
            AddCriterion(keyField.LessThanCondition(), keyField.value, keyField.name);
        }

    }
}

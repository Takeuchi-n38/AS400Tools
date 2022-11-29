using System;
using System.Collections.Generic;
using System.Linq;
using Systemi_emurator.fileaccess;

namespace Systemi_emurator.Fileaccess
{
    public abstract class TargetConditionsForAccessPath
    {
        public abstract CriteriaForAccessPath Or();

        public virtual List<CriteriaForAccessPath> GetOredCriteriaForAccessPath()
        {
            var rtn = new List<CriteriaForAccessPath>();
            rtn.Add(Or());
            return rtn;
        }
        public abstract void SetOrderByClause(String orderByClause);

        public abstract void Clear();

        public AccessKeyDefinitions accessKeyDefinitions;

        protected TargetConditionsForAccessPath()
        {
            List<AccessKeyDefinition> accessKeyDefinitions = new List<AccessKeyDefinition>();
            PassAccessKeyDefinitionsTo(accessKeyDefinitions);
            this.accessKeyDefinitions = new AccessKeyDefinitions(accessKeyDefinitions);
        }

        protected abstract void PassAccessKeyDefinitionsTo(List<AccessKeyDefinition> collector);

        public void SetOrderByClause()
        {
            this.SetOrderByClause(this.accessKeyDefinitions.GetOrderByClause());
        }

        public List<AccessKeyField> CreateAccessKeyFieldsBy(IAccessKeyFieldList accessKeyFieldList)
        {
            return accessKeyDefinitions.CreateAccessKeyFieldsBy(accessKeyFieldList.GetAccessKeyValues());
        }

        //public List<AccessKeyField> CreateAccessKeyFieldsBy(List<Object> accessKeyValues)
        //{
        //    return accessKeyDefinitions.CreateAccessKeyFieldsBy(accessKeyValues);
        //}
        public Action<List<AccessKeyField>, bool> setLimitConditionWith;
        public void ReplaceLowerLimitCondition(IAccessKeyFieldList limitKeyFieldList)
        {
            this.setLimitConditionWith = CreateSetLimitConditionWith(CreateAccessKeyFieldsBy(limitKeyFieldList), false);
        }

        public void ReplaceGreaterThanCondition(IAccessKeyFieldList limitKeyFieldList)
        {
            this.setLimitConditionWith =
                CreateSetLimitConditionWith(CreateAccessKeyFieldsBy(limitKeyFieldList), true);
        }

        public Action<List<AccessKeyField>, bool> CreateSetLimitConditionWith(
            List<AccessKeyField> limitKeyFields, bool isGt)
        {
            if (isGt)
            {
                return (equalKeyFields, isPrior) =>
                {
                    //Optional<List<AccessKeyField>> curLimitKeyFields = Optional.of(limitKeyFields);
                    if (isPrior)
                    {
                        OrLessThan(limitKeyFields, equalKeyFields);
                        OrEquqlTo(limitKeyFields, equalKeyFields);
                    }
                    else
                    {
                        OrGreaterThan(limitKeyFields, equalKeyFields);
                    }
                };
            }
            else
            {
                return (equalKeyFields, isPrior) =>
                {
                    //Optional<List<AccessKeyField>> curLimitKeyFields = Optional.of(limitKeyFields);
                    if (isPrior)
                    {
                        OrLessThan(limitKeyFields, equalKeyFields);
                    }
                    else
                    {
                        OrEquqlTo(limitKeyFields, equalKeyFields);
                        OrGreaterThan(limitKeyFields, equalKeyFields);
                    }
                };
            }
        }

        public void SetConditionsForReadBy(bool prior)
        {
            if (prior)
            {
                SetConditionsForReadPrior();
            }
            else
            {
                SetConditionsForRead();
            }
        }

        public void SetConditionsForRead()
        {
            SetOrderByClause();
            if (setLimitConditionWith != null)
            {
                SetLimitConditionsForReadWith(null);
            }
            else
            {
                GetOredCriteriaForAccessPath();//key指定やset指定が無い場合は、ここでLFの諸条件を取り込む
            }
        }

        public void SetConditionsForReadPrior()
        {
            SetOrderByClause();
            if (setLimitConditionWith != null)
            {
                SetLimitConditionsForReadPriorWith(null);
            }
            else
            {
                GetOredCriteriaForAccessPath();//key指定やset指定が無い場合は、ここでLFの諸条件を取り込む
            }
        }

        public void SetConditionsForReadEqual(IAccessKeyFieldList equalKeyFieldList)
        {

            SetOrderByClause();

            List<AccessKeyField> equalKeyFields = CreateAccessKeyFieldsBy(equalKeyFieldList);
            if (setLimitConditionWith == null)
            {
                OrEquqlTo(null, equalKeyFields);
                return;
            }
            SetLimitConditionsForReadWith(equalKeyFields);
        }

        public void SetConditionsForReadEqualPrior(IAccessKeyFieldList equalKeyFieldList)
        {

            SetOrderByClause();

            if (CreateAccessKeyFieldsBy(equalKeyFieldList) != null)
            {
                List<AccessKeyField> equalKeyFields = CreateAccessKeyFieldsBy(equalKeyFieldList);
                if (setLimitConditionWith == null)
                {
                    OrEquqlTo(null, equalKeyFields);
                    return;
                }
                SetLimitConditionsForReadPriorWith(equalKeyFields);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void SetLimitConditionsForReadWith(List<AccessKeyField> equalKeyFields)
        {

            // Action<List<AccessKeyField>, Boolean> biConsumer = this.setLimitConditionWith;
            this.setLimitConditionWith.Invoke(equalKeyFields, false);
            setLimitConditionWith = null;
        }

        public void SetLimitConditionsForReadPriorWith(List<AccessKeyField> equalKeyFields)
        {

            this.setLimitConditionWith.Invoke(equalKeyFields, true);
            setLimitConditionWith = null;
        }

        //TODO:
        public void OrEquqlTo(List<AccessKeyField> limitKeyFields,
            List<AccessKeyField> equalKeyFields)
        {
            foreach (CriteriaForAccessPath criteria in GetOredCriteriaForAccessPath())
            {
                if (limitKeyFields != null)
                {
                    limitKeyFields.ForEach(criteria.AddEqualToCriterionBy);
                }
                if (equalKeyFields != null)
                {
                    criteria.AddEqualToCriterionBy(equalKeyFields);
                }
            }
        }

        public void OrGreaterThan(List<AccessKeyField> limitKeyFields,
            List<AccessKeyField> equalKeyFields)
        {

            OrThan(limitKeyFields, equalKeyFields,
                (criteria, accessKeyField) => criteria.AddGreaterThanCriterionBy(accessKeyField));

        }

        public void OrLessThan(List<AccessKeyField> limitKeyFields,
            List<AccessKeyField> equalKeyFields)
        {

            OrThan(limitKeyFields, equalKeyFields,
                (criteria, accessKeyField) => criteria.AddLessThanCriterionBy(accessKeyField));

        }

        public void OrThan(List<AccessKeyField> limitKeyFields,
            List<AccessKeyField> equalKeyFields,
            Action<CriteriaForAccessPath, AccessKeyField> addThanCriterionBy)
        {
            List<AccessKeyField> curlimitKeyFields = limitKeyFields;
            for (int criteriaIndex = 0; criteriaIndex < curlimitKeyFields.Count(); criteriaIndex++)
            {
                foreach (CriteriaForAccessPath criteria in GetOredCriteriaForAccessPath())
                {
                    //curlimitKeyFields.stream().limit(criteriaIndex).forEach(criteria::addEqualToCriterionBy);
                    curlimitKeyFields.GetRange(0, criteriaIndex).ForEach(criteria.AddEqualToCriterionBy);
                    //addThanCriterionBy.accept(criteria, curlimitKeyFields.get(criteriaIndex));
                    addThanCriterionBy.Invoke(criteria, curlimitKeyFields[criteriaIndex]);
                    //equalKeyFields.ifPresent(criteria::addEqualToCriterionBy);
                    if (equalKeyFields != null)
                        criteria.AddEqualToCriterionBy(equalKeyFields);
                }
            }

        }
    }
}

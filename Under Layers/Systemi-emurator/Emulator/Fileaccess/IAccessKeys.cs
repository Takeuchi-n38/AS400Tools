using System;

namespace Systemi_emurator.Fileaccess
{
    public interface IAccessKeys<TypeOfTargetConditions, TypeOfCriteria>
    {
        public abstract TypeOfTargetConditions NewTargetConditions();
        public virtual TypeOfTargetConditions CreateOrderdAllTargetConditions()
        {
            TypeOfTargetConditions targetConditions = NewTargetConditions();
            SetOrderByClause(targetConditions);
            return targetConditions;
        }


        public TypeOfTargetConditions CreateTargetConditions(Action<TypeOfTargetConditions> setCriteria)
        {
            TypeOfTargetConditions targetConditions = NewTargetConditions();
            SetOrderByClause(targetConditions);
            setCriteria.Invoke(targetConditions);
            return targetConditions;
        }

        public TypeOfTargetConditions CreateEquqlToTargetConditions()
        {
            return CreateTargetConditions((targetConditions) => SetEqualToCriteria(targetConditions));
        }

        public TypeOfTargetConditions CreateGreaterThanTargetConditions()
        {
            return CreateTargetConditions((targetConditions) => SetGreaterThaCriteria(targetConditions));
        }

        public TypeOfTargetConditions CreateGreaterThanOrEqualToTargetConditions()
        {
            return CreateTargetConditions((targetConditions) => SetGreaterThanOrEqualToCriteria(targetConditions));
        }

        public void SetEqualToCriteria(TypeOfTargetConditions targetConditions)
        {
            SetCriteria(targetConditions, criteria => AddEqualToCriterion(criteria));
        }

        public void SetGreaterThaCriteria(TypeOfTargetConditions targetConditions)
        {
            SetCriteria(targetConditions, criteria => AddEqualToCriterion(criteria));
        }

        public void SetGreaterThanOrEqualToCriteria(TypeOfTargetConditions targetConditions)
        {
            SetCriteria(targetConditions, criteria => AddEqualToCriterion(criteria));
        }

        public void SetCriteria(TypeOfTargetConditions targetConditions, Action<TypeOfCriteria> addCriterion)
        {
            TypeOfCriteria criteria = CreateCriteria(targetConditions);
            addCriterion.Invoke(criteria);
        }
        public abstract TypeOfCriteria CreateCriteria(TypeOfTargetConditions targetConditions);
        public abstract void AddEqualToCriterion(TypeOfCriteria criteria);
        public abstract void AddGreaterThanCriterion(TypeOfCriteria criteria);
        public abstract void AddGreaterThanOrEqualToCriterion(TypeOfCriteria criteria);
        public abstract void SetOrderByClause(TypeOfTargetConditions targetConditions);
    }
}

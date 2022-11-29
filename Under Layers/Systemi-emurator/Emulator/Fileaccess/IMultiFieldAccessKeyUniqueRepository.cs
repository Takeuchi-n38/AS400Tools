using System.Collections.Generic;
using System.Linq;

namespace Systemi_emurator.Fileaccess
{
    public interface IMultiFieldAccessKeyUniqueRepository<TypeOfEntity>
    {
        public List<TypeOfEntity> SelectByScrollandPageableTargetConditionsWithClearCondition(
            TargetConditionsForAccessPath targetConditions, bool prior,
            Pageable pageable)
        {
            List<TypeOfEntity> results = SelectByScrollandPageableTargetConditions(targetConditions, prior, pageable);
            targetConditions.Clear();
            if (results == null)
                results = new List<TypeOfEntity>();
            return results;
        }

        List<TypeOfEntity> SelectByScrollandPageableTargetConditions(TargetConditionsForAccessPath targetConditions, bool prior, Pageable pageable);

        public List<TypeOfEntity> Read(TargetConditionsForAccessPath targetConditions)
        {
            targetConditions.SetConditionsForRead();
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, false, null);
        }

        public List<TypeOfEntity> ReadPrior(TargetConditionsForAccessPath targetConditions)
        {
            targetConditions.SetConditionsForReadPrior();
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, true, null);
        }

        public List<TypeOfEntity> ReadPrior(TargetConditionsForAccessPath targetConditions, int limit)
        {
            targetConditions.SetConditionsForReadPrior();
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, true, new Pageable(0, limit));
        }

        public List<TypeOfEntity> ReadEqual(TargetConditionsForAccessPath targetConditions,
            IAccessKeyFieldList accessKeyFieldList)
        {
            targetConditions.SetConditionsForReadEqual(accessKeyFieldList);
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, false, null);
        }

        public List<TypeOfEntity> ReadEqual(TargetConditionsForAccessPath targetConditions,
            IAccessKeyFieldList accessKeyFieldList, int limit)
        {
            targetConditions.SetConditionsForReadEqual(accessKeyFieldList);
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, false, new Pageable(0, limit));
        }

        public List<TypeOfEntity> ReadEqualPrior(TargetConditionsForAccessPath targetConditions,
            IAccessKeyFieldList accessKeyFieldList)
        {
            targetConditions.SetConditionsForReadEqualPrior(accessKeyFieldList);
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, true, null);
        }

        public List<TypeOfEntity> ReadEqualPrior(TargetConditionsForAccessPath targetConditions,
            IAccessKeyFieldList accessKeyFieldList, int limit)
        {
            targetConditions.SetConditionsForReadEqualPrior(accessKeyFieldList);
            return SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, true, new Pageable(0, limit));
        }

        public TypeOfEntity Chain(TargetConditionsForAccessPath targetConditions,
            IAccessKeyFieldList accessKeyFieldList)
        {
            targetConditions.SetConditionsForReadEqual(accessKeyFieldList);
            List<TypeOfEntity> list =
                SelectByScrollandPageableTargetConditions(targetConditions, false, new Pageable(0, 1));
            targetConditions.Clear();
            return (list == null || list.Count() == 0) ? default(TypeOfEntity) : list[0];
        }

        public TypeOfEntity Chain(TargetConditionsForAccessPath targetConditions, int rrn)
        {
            return Chain(targetConditions, false, rrn);
        }

        public TypeOfEntity Chain(TargetConditionsForAccessPath targetConditions, bool prior, int rrn)
        {
            targetConditions.SetConditionsForReadBy(prior);
            List<TypeOfEntity> list =
                SelectByScrollandPageableTargetConditionsWithClearCondition(targetConditions, prior, new Pageable(rrn, 1));
            return (list == null || list.Count() == 0) ? default(TypeOfEntity) : list[0];
        }

        public TypeOfEntity ReadFirst(TargetConditionsForAccessPath targetConditions)
        {
            return Chain(targetConditions, 0);
        }

        public TypeOfEntity ReadPriorFirst(TargetConditionsForAccessPath targetConditions)
        {
            return Chain(targetConditions, true, 0);
        }

    }
}

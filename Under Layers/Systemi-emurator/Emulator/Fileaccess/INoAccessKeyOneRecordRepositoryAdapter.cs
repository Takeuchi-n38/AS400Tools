using System;
using System.Collections.Generic;

namespace Systemi_emurator.Fileaccess
{
    public interface INoAccessKeyOneRecordRepositoryAdapterBase<TypeOfEntity, TypeOfTargetConditions>
    {
        List<TypeOfEntity> SelectByTargetConditions(TypeOfTargetConditions targeConditions);
        TypeOfEntity Read();
    }

    public abstract class INoAccessKeyOneRecordRepositoryAdapter<TypeOfEntity, TypeOfTargetConditions> : INoAccessKeyOneRecordRepositoryAdapterBase<TypeOfEntity, TypeOfTargetConditions>
    {
        public virtual TypeOfEntity Read()
        {
            //TODO: Generic type null
            //List<TypeOfEntity> list = selectByTargetConditions(null);
            //return (list.size() == 0) ? null : list.get(0);
            throw new NotImplementedException();
        }

        public abstract List<TypeOfEntity> SelectByTargetConditions(TypeOfTargetConditions targeConditions);
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Delta.Entities
{
    public interface IEntityRepository<EntityType> where EntityType : class
    {
        int Insert(EntityType entity);

        int Update(EntityType item);

        int Delete(EntityType item);

        int DeleteWhere(Expression<Func<EntityType, bool>> predicate);

        IQueryable<EntityType> FindAll();

        int Count();

        int Count(Expression<Func<EntityType, bool>> predicate);

    }
}

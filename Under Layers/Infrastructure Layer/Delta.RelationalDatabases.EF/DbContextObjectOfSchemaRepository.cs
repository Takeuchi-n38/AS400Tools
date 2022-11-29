using Delta.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.EF
{
    public class DbContextObjectOfSchemaRepository<EntityType>
        : IEntityRepository<EntityType>
        where EntityType : class
    {
        protected int ExecuteSqlRaw(string rawSqlString,params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRaw(rawSqlString, parameters);
        }

        protected readonly DbContext DbContext;

        protected DbContextObjectOfSchemaRepository(DbContext context)
        {
            DbContext = context;
        }

        int IEntityRepository<EntityType>.Insert(EntityType entity)
        {
            if (entity is ISortaleBy4s sortaleBy4S) sortaleBy4S.Update4sValues();
            DbContext.Add(entity);
            return DbContext.SaveChanges();
        }

        int IEntityRepository<EntityType>.Update(EntityType entity)
        {
            if (entity is ISortaleBy4s sortaleBy4S) sortaleBy4S.Update4sValues();
            //_context.Entry(entity).State = EntityState.Modified;
            DbContext.Update(entity);
            return DbContext.SaveChanges();
        }

        int IEntityRepository<EntityType>.Delete(EntityType entity)
        {
            DbContext.Remove(entity);
            return DbContext.SaveChanges();
        }

        int IEntityRepository<EntityType>.DeleteWhere(Expression<Func<EntityType, bool>> predicate)
        {
            var dbSet = DbContext.Set<EntityType>();
            DbContext.Set<EntityType>().RemoveRange(dbSet.Where(predicate));
            return DbContext.SaveChanges();
        }

        IQueryable<EntityType> IEntityRepository<EntityType>.FindAll()
        {
            return DbContext.Set<EntityType>().AsNoTracking();
        }

        protected void Truncate(string objectName)
        {
            ExecuteSqlRaw($"TRUNCATE TABLE {objectName}");
        }

        int IEntityRepository<EntityType>.Count()
        {
            return ((IEntityRepository<EntityType>)this).FindAll().Count();
        }

        int IEntityRepository<EntityType>.Count(Expression<Func<EntityType, bool>> predicate)
        {
            return ((IEntityRepository<EntityType>)this).FindAll().Where(predicate).Count();
        }
    }
}

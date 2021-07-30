using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ApplicatonProcess.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int skip = 0, int take = 0);
        TEntity GetByID(object id);
        
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}

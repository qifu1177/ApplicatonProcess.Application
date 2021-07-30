using ApplicatonProcess.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DataContext _context;
        protected DbSet<TEntity> _dbSet;
        public BaseRepository(DataContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<TEntity>();
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if(_context.Entry(entityToDelete).State==EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "",int skip=0,int take=0)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);                
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy!=null)
            {
                query = orderBy(query);
            }
            if(skip>0)
            {
                query.Skip(skip);
            }
            if(take>0)
            {
                query.Take(take);
            }
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }        

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

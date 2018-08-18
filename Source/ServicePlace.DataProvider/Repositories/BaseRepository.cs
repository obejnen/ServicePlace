using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;

        protected abstract IEnumerable<Expression<Func<TEntity, object>>> Includes { get; }

        protected IQueryable<TEntity> Query
        {
            get
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (Includes != null) query = Includes.Aggregate(query, (current, include) => current.Include(include));

                return query;
            }
        }

        protected BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(entity);
        }

        public virtual IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Query.Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Query;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using rpavelko.Data.Core;
using rpavelko.Data.Extensions;

namespace rpavelko.Data.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        private IContext _context;

        protected RepositoryBase(IContextFactory factory)
        {
            ContextFactory = factory;
        }

        protected IContextFactory ContextFactory
        {
            get; private set;
        }

        protected IContext Context
        {
            get { return _context ?? (_context = ContextFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            Context.Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Delete(entity);
        }

        public virtual T GetById(int id)
        {
            return Context.GetById<T>(id);
        }

        public virtual IEnumerable<T> GetAllInList(string includeProperties = "")
        {
            return Context.GetAllInList<T>(includeProperties);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.GetAll<T>();
        }

        public virtual IEnumerable<T> GetPage(int pageIndex, int pageSize, string sortField, string sortDirection)
        {
            return Context.GetAll<T>().OrderBy(sortField, sortDirection).Skip(pageIndex * pageSize).Take(pageSize).ToList();    
        }

        public virtual IEnumerable<dynamic> GetPage(int pageIndex, int pageSize, string sortField, string sortDirection, Expression<Func<T, dynamic>> selector)
        {
            return Context.GetAll<T>().OrderBy(sortField, sortDirection).Skip(pageIndex * pageSize).Take(pageSize).Select(selector);
        }

        public virtual IEnumerable<T> Get(out int total, int page = 0, int rows = 0, string orderBy = null, string orderDir = null, string includeProperties = "")
        {
            return Context.Get<T>(out total, page, rows, orderBy, orderDir, includeProperties);
        }
    }
}

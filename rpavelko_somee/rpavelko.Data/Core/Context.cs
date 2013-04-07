using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using rpavelko.Data.Entities;
using rpavelko.Data.Mapping;

namespace rpavelko.Data.Core
{
    public class Context: DbContext, IContext
    {
        public Context(string cnStringName)
            : base(cnStringName)
        {
            DtBase = Database;
        }
        public Database DtBase { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();

            modelBuilder.Configurations.Add(new AccountMap());
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public T GetById<T>(int id) where T : class
        {
            return Set<T>().Find(id);
        }

        public IEnumerable<T> GetAllInList<T>(string includeProperties = "") where T : class
        {
            IQueryable<T> query = Set<T>();
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Set<T>();
        }


        public virtual IEnumerable<T> Get<T>(out int total, int page = 0, int rows = 0, string orderBy = null, string orderDir = null, string includeProperties = "") where T : class
        {
            IQueryable<T> query = Set<T>();
            total = query.Count();

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (rows > 0)
                query = query.OrderBy(String.Format("{0} {1}", orderBy, orderDir)).Skip(page * rows - rows).Take(rows);
            
            return query.ToList();
        }
    }
}

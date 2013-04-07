using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using rpavelko.Data.Entities;

namespace rpavelko.Data.Core
{
    public interface IContext
    {
        Database DtBase { get; set; }
        DbSet<Account> Accounts { get; set; }
        
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        T GetById<T>(int id) where T : class;
        IEnumerable<T> GetAllInList<T>(string includeProperties = "") where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        IEnumerable<T> Get<T>(out int total, int page = 0, int rows = 0, string orderBy = null, string orderDir = null, string includeProperties = "") where T : class;

        void Commit();
    }
}

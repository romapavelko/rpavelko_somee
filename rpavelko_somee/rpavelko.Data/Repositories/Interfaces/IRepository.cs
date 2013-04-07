using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace rpavelko.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAllInList(string includeProperties = "");
        IQueryable<T> GetAll();

        IEnumerable<T> GetPage(int pageIndex, int pageSize, string sortField, string sortDirection);
        IEnumerable<dynamic> GetPage(int pageIndex, int pageSize, string sortField, string sortDirection, Expression<Func<T, dynamic>> selector);


        IEnumerable<T> Get(
            out int total,
            int start = 0,
            int limit = 0,
            string orderBy = null,
            string orderDir = null,
            string includeProperties = "");
    }
}

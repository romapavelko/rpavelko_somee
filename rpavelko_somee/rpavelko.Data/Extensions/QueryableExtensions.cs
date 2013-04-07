using System;
using System.Linq;
using System.Linq.Expressions;

namespace rpavelko.Data.Extensions
{
    static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> items, string propertyName, string sortDirection = "asc")
        {
            var typeOfT = typeof(T);
            var parameter = Expression.Parameter(typeOfT, "parameter");
            var propertyType = typeOfT.GetProperty(propertyName).PropertyType;
            var propertyAccess = Expression.PropertyOrField(parameter, propertyName);
            var orderExpression = Expression.Lambda(propertyAccess, parameter);
            string method;
            switch (sortDirection.ToLower())
            {
                case "asc":
                    method = "OrderBy";
                    break;
                case "desc":
                    method = "OrderByDescending";
                    break;
                default:
                    throw new ArgumentException("Incorrect value of sortDirection parameter");
            }
            var expression = Expression.Call(typeof(Queryable), method, new Type[] { typeOfT, propertyType }, items.Expression, Expression.Quote(orderExpression));
            return items.Provider.CreateQuery<T>(expression);
        }
    }
}

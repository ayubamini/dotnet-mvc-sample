using System.Linq.Dynamic.Core;

namespace CustomerManagementSystem.Extensions
{


    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByProperty)
        {
            if (string.IsNullOrWhiteSpace(orderByProperty))
                return query;

            return query.OrderBy(orderByProperty);
        }

        public static IQueryable<T> OrderByDynamicDescending<T>(this IQueryable<T> query, string orderByProperty)
        {
            if (string.IsNullOrWhiteSpace(orderByProperty))
                return query;

            return query.OrderBy(orderByProperty + " descending");
        }
    }

}

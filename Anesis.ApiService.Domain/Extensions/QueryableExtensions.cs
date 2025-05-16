using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Anesis.ApiService.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, bool condition)
        {
            if (!condition)
                return query;

            return query.Where(predicate);
        }

        public static IPagedList<T> ToPageList<T>(this IQueryable<T> query, int startIndex, int countNumber)
        {
            var totalCount = query.Count();
            return new PagedList<T>()
            {
                Data = query.ApplyPaging(startIndex, countNumber).ToList(),
                TotalCount = totalCount,
                TotalPage = totalCount / countNumber + (totalCount % countNumber > 0 ? 1 : 0),
                PageIndex = startIndex / countNumber,
                PageSize = countNumber
            };
        }

        public async static Task<IPagedList<T>> ToPageListAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            return await query.ToPageListAsync(0, int.MaxValue, cancellationToken);
        }

        public async static Task<IPagedList<T>> ToPageListAsync<T>(this IQueryable<T> query, 
            int startIndex, int countNumber, CancellationToken cancellationToken = default)
        {
            var totalCount = query.Count();
            var data = await query.ApplyPaging(startIndex, countNumber).ToListAsync();
            return new PagedList<T>()
            {
                Data = data,
                TotalCount = totalCount,
                TotalPage = totalCount / countNumber + (totalCount % countNumber > 0 ? 1 : 0),
                PageIndex = startIndex / countNumber,
                PageSize = countNumber
            };
        }


        public static IQueryable<T> SortData<T>(this IQueryable<T> query, string sidx, string sord, string defaultSidx = null, string defaultSord = SortOrder.Ascending)
        {
            if (sidx.IsNullOrWhiteSpace() && defaultSidx.IsNullOrWhiteSpace())
                return query;

            sidx = sidx.IfNullOrWhiteSpace(defaultSidx);
            sord = sord.IfNullOrWhiteSpace(defaultSord);

            var property = typeof(T).GetProperty(sidx, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (property != null)
            {
                var methodName = $"OrderBy{(sord == SortOrder.Ascending ? "" : SortOrder.Descending)}";
                var param = Expression.Parameter(typeof(T), "x");

                var propertyAccess = Expression.Property(param, property);
                var orderByExpression = Expression.Lambda(propertyAccess, param);

                var resultExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    [ typeof(T), property.PropertyType ],
                    query.Expression,
                    Expression.Quote(orderByExpression));

                return query.Provider.CreateQuery<T>(resultExpression);
            }

            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int startIndex, int countNumber)
        {
            if (startIndex == 0 && countNumber == int.MaxValue)
                return query;

            return query
                .Skip(startIndex)
                .Take(countNumber);
        }
    }
}

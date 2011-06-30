using System;
using System.Collections.Generic;
using System.Linq;
using Plumbing.Utility;

namespace Plumbing.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IQueryable<TTo> Map<TFrom, TTo>(this IQueryable<TFrom> queryable,
                                                      IMapper<TFrom, TTo> mapper)
        {
            return queryable.Select(mapper.MappingExpression);
        }
    }
}
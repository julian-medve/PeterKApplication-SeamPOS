using System;
using System.Linq;
using System.Linq.Expressions;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Since<T>(this IQueryable<T> set, DateTime time, bool has = true, Expression<Func<T, bool>> ex = null)
            where T : BaseEntity
        {
            var q = set.Where(i => !has || i.CreatedOn >= time || i.ModifiedOn >= time);

            if (ex != null) q = q.Where(ex);

            return q;
        }
        
        public static IQueryable<AppUser> Since(this IQueryable<AppUser> set, DateTime time, bool has = true, Expression<Func<AppUser, bool>> ex = null)
        {
            var q = set.Where(i => !has || i.CreatedOn >= time || i.ModifiedOn >= time);

            if (ex != null) q = q.Where(ex);

            return q;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Interfaces;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> Since<T>(this DbSet<T> set, DateTime time, bool has,
            Expression<Func<T, bool>> ex = null)
            where T : BaseEntity
        {
            var q = set.Where(i => !has || i.CreatedOn >= time || i.ModifiedOn >= time);

            if (ex != null) q = q.Where(ex);

            return q;
        }
        
        public static IQueryable<AppUser> Since(this DbSet<AppUser> set, DateTime time, bool has,
            Expression<Func<AppUser, bool>> ex = null)
        {
            var q = set.Where(i => !has || i.CreatedOn >= time || i.ModifiedOn >= time);

            if (ex != null) q = q.Where(ex);

            return q;
        }

        public static void FindOrNew<T>(this DbSet<T> set, Guid id, out T ob, out bool isNew)
            where T : BaseEntity, new()
        {
            var ex = set.Find(id);
            isNew = ex == null;
            ob = ex ?? new T();
        }

        public static void FindOrNew(this DbSet<AppUser> set, string id, out AppUser ob, out bool isNew)
        {
            var ex = set.Find(id);
            isNew = ex == null;
            ob = ex ?? new AppUser();
        }

        public static void Apply<T, TG>(this DbSet<T> set, List<TG> objects, Func<T, bool, T> modify = null)
            where T : BaseEntity, new() where TG : IHasId
        {
            foreach (var o in objects)
            {
                set.FindOrNew(o.Id, out var ob, out var isNew);
                ob.ApplyProperties(o);
                if (modify != null) ob = modify(ob, isNew);
                if (isNew)
                {
                    set.Add(ob);
                }
            }
        }

        public static void Apply<T, TG>(this DbSet<T> set, TG o, Func<T, bool, T> modify = null)
            where T : BaseEntity, new() where TG : IHasId
        {
            if (o == null) return;
            
            set.FindOrNew(o.Id, out var ob, out var isNew);
            ob.ApplyProperties(o);
            if (modify != null) ob = modify(ob, isNew);
            if (isNew)
            {
                set.Add(ob);
            }
        }

        public static void ApplyUsers<T>(this DbSet<AppUser> set, List<T> objects, Func<AppUser, bool, AppUser> modify = null)
            where T : IHasStringId
        {
            foreach (var o in objects)
            {
                set.FindOrNew(o.Id, out var ob, out var isNew);
                ob.ApplyProperties(o);
                if (modify != null) ob = modify(ob, isNew);
                if (isNew)
                {
                    set.Add(ob);
                }
            }
        }
    }
}
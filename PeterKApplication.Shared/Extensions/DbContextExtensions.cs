using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task Upsert<T>(this DbContext context, List<T> updates) where T : BaseEntity
        {
            if (!updates.Any()) return;
            
            foreach (var baseEntity in updates)
            {
                if (context.Find<T>(baseEntity.Id) is T t)
                {
                    context.Entry(t).State = EntityState.Detached;
                    context.Update(baseEntity);
                }
                else
                {
                    context.Add(baseEntity);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task Upsert(this DbContext context, List<AppUser> updates)
        {
            if (!updates.Any()) return;

            foreach (var baseEntity in updates)
            {
                if (context.Find<AppUser>(baseEntity.Id) is AppUser u)
                {
                    context.Entry(u).State = EntityState.Detached;
                    context.Update(baseEntity);
                }
                else
                {
                    context.Add(baseEntity);
                }
            }
            
            await context.SaveChangesAsync();
        }

        public static async Task Upsert<T>(this DbContext context, T update) where T : BaseEntity
        {
            if (context.Find<AppUser>(update.Id) is AppUser u)
            {
                context.Entry(u).State = EntityState.Detached;
                context.Update(update);
            }
            else
            {
                context.Add(update);
            }

            await context.SaveChangesAsync();
        }
    }
}
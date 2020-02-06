using System.Collections.Generic;
using System.Linq;
using PeterKApplication.Shared.Interfaces;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Extensions
{
    public static class BaseEntityExtensions
    {
        public static List<T> SetIsSynced<T>(this List<T> list) where T : BaseEntity
        {
            return list.Select(s =>
            {
                s.IsSynced = true;
                return s;
            }).ToList();
        }

        public static List<AppUser> SetIsSynced(this List<AppUser> list)
        {
            return list.Select(s =>
            {
                s.IsSynced = true;
                return s;
            }).ToList();
        }

        public static T SetIsSynced<T>(this T s) where T : BaseEntity
        {
            s.IsSynced = true;
            return s;
        }

        public static T ApplyProperties<T, TG>(this T dest, TG source)
        {
            var parentProperties = source.GetType().GetProperties();
            var childProperties = dest.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(dest, parentProperty.GetValue(source));
                        break;
                    }
                }
            }

            return dest;
        }
    }
}
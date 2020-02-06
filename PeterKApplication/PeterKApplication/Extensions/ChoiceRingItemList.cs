using System;
using System.Collections.Generic;
using System.Linq;
using PeterKApplication.Models;
using Xamarin.Forms.Internals;

namespace PeterKApplication.Extensions
{
    public static class ChoiceRingItemList
    {
        public static List<ChoiceRingItem> Selected(this List<ChoiceRingItemGroup> items)
        {
            return items?.Aggregate(new List<ChoiceRingItem>(), (list, group) =>
            {
                new[] {group.Item1, group.Item2, group.Item3}.ForEach(i =>
                {
                    if (i?.Selected == true)
                    {
                        list.Add(i);
                    }
                });

                return list;
            });
        }
        
        public static List<Guid> SelectedIds(this List<ChoiceRingItemGroup> items)
        {
            return items?.Selected()?.Select(s => s.Id)?.ToList();
        }

        public static List<string> SelectedNames(this List<ChoiceRingItemGroup> items)
        {
            return items?.Selected()?.Select(s => s.Text)?.ToList();
        }

        public static ChoiceRingItem FirstSelected(this List<ChoiceRingItemGroup> items)
        {
            foreach (var i in items)
            {
                if (i.Item1?.Selected == true)
                {
                    return i.Item1;
                }

                if (i.Item2?.Selected == true)
                {
                    return i.Item2;
                }

                if (i.Item3?.Selected == true)
                {
                    return i.Item3;
                }
            }
            return null;
        }

        public static Guid? FirstSelectedId(this List<ChoiceRingItemGroup> items)
        {
            return items?.FirstSelected()?.Id;
        }

        public static string FirstSelectedName(this List<ChoiceRingItemGroup> items)
        {
            return items?.FirstSelected()?.Text;
        }
    }
}
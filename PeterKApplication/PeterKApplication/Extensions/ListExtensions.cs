using System.Collections.Generic;
using PeterKApplication.Models;
using Xamarin.Forms.Internals;

namespace PeterKApplication.Extensions
{
    public static class ListExtensions
    {
        public static List<ChoiceRingItemGroup> ToChoiceRingItemGroup(this IEnumerable<ChoiceRingItemTemplate> list)
        {
            var newList = new List<ChoiceRingItemGroup>();
            
            list.ForEach(i =>
            {
                newList.Add(new ChoiceRingItem
                {
                    Selected = false,
                    Text = i.Name,
                    Id = i.Id
                });
            });

            return newList;
        }
    }
}
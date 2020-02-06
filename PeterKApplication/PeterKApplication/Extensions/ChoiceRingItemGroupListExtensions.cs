using System.Collections.Generic;
using PeterKApplication.Models;

namespace PeterKApplication.Extensions
{
    public static class ChoiceRingItemGroupListExtensions
    {
        public static List<ChoiceRingItemGroup> Add(this List<ChoiceRingItemGroup> list, ChoiceRingItem item)
        {
            if (list.Count > 0)
            {

                if (list[list.Count - 1].Item1 == null)
                {
                    list[list.Count - 1].Item1 = item;
                }
                else if (list[list.Count - 1].Item2 == null)
                {
                    list[list.Count - 1].Item2 = item;
                }
                else if (list[list.Count - 1].Item3 == null)
                {
                    list[list.Count - 1].Item3 = item;
                }
                else
                {
                    list.Add(new ChoiceRingItemGroup
                    {
                        Item1 = item
                    });
                }
            }
            else
            {
                list.Add(new ChoiceRingItemGroup
                {
                    Item1 = item
                });
            }

            return list;
        }
    }
}
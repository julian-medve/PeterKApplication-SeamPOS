using System.Collections.Generic;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Extensions
{
    public static class PairedListPairListExtensions
    {
        public static PairedListItem Selected(this List<PairedListPair> list)
        {
            foreach (var pairedListPair in list)
            {
                if (pairedListPair?.Item1?.Selected == true)
                {
                    return pairedListPair.Item1;
                }
                
                if (pairedListPair?.Item2?.Selected == true)
                {
                    return pairedListPair.Item2;
                }
            }

            return null;
        }
    }
}
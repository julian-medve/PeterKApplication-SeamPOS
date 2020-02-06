using PeterKApplication.Annotations;

namespace PeterKApplication.Models
{
    public class PairedListPair
    {
        [CanBeNull] public PairedListItem Item1 { get; set; }
        [CanBeNull] public PairedListItem Item2 { get; set; }
    }
}
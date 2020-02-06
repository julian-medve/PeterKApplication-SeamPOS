namespace PeterKApplication.Models
{
    public class ChangeOwnerTab
    {
        public OwnerTabPage Page { get; set; }
        public OwnerTabPageSubCategory SubCategory { get; set; }
    }

    public enum OwnerTabPage
    {
        Products = 1,
        Business = 3
    }

    public enum OwnerTabPageSubCategory
    {
        Staff,
        Business,
        Payments,
        Support,
        Faq,
        Offers,
        Analytics, 
        Locations
    }
}
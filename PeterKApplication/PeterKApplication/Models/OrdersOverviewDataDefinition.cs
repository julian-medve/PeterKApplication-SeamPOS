namespace PeterKApplication.Models
{
    public class OrdersOverviewDataDefinition
    {
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal Highest { get; set; }
        public decimal Lowest { get; set; }
    }
}
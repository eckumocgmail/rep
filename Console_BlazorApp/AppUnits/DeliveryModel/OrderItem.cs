namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class OrderItem : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }


        public Product Product { get; set; }
        public int ProductCount { get; set; }
        public string ProductSize { get; internal set; }
        public int ProductWeight { get; internal set; }
    }

}

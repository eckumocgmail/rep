namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class OrderItem : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }


        public Product Product { get; set; }
        public int ProductCount { get; set; }

        [NotNullNotEmpty]
        public string ProductSize { get; set; } = "100,100,100";

        [NotNullNotEmpty]
        public int ProductWeight { get; set; } = 1000;
    }

}

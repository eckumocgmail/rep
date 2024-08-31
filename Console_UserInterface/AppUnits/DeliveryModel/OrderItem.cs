﻿namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    [Label("Позиция в заказе")]
    public class OrderItem : BaseEntity
    {
        [NotInput]
        public int OrderId { get; set; }

        [InputDictionary($"{nameof(Product)},ProductName")]
        public int ProductId { get; set; }


        public Product Product { get; set; }
        public int ProductCount { get; set; }

        [NotNullNotEmpty]
        public string ProductSize { get; set; } = "100,100,100";

        [NotNullNotEmpty]
        public int ProductWeight { get; set; } = 1000;
    }

}

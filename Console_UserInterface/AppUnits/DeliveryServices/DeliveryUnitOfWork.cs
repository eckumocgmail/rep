using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_AuthModel
{
    public class DeliveryUnitOfWork
    {
        public readonly IEntityFasade<Product> Products;
        public readonly IEntityFasade<Holder> Holders;
        public readonly IEntityFasade<Order> Orders;
        public readonly IEntityFasade<OrderItem> OrderItems;
        public readonly IEntityFasade<ProductImage> ProductImages;

        public DeliveryUnitOfWork(IEntityFasade<Product> products, IEntityFasade<Holder> holders, IEntityFasade<Order> orders, IEntityFasade<OrderItem> orderItems, IEntityFasade<ProductImage> productImages)
        {
            Products = products;
            Holders = holders;
            Orders = orders;
            OrderItems = orderItems;
            ProductImages = productImages;
        }



    }

}

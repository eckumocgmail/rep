using Console_BlazorApp.AppUnits.DeliveryModel;

namespace pickpoint_delivery_service
{
    internal class SparePart
    {
        public object Product_id { get; internal set; }
        public string Product_name { get; internal set; }
        public string Description { get; internal set; }
        public List<ProductImage> Images { get; internal set; }
    }
}
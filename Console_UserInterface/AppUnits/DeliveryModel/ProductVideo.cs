using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_UserInterface.AppUnits.DeliveryModel
{
    public class ProductVideo: BaseEntity
    {
        public string ContentType { get; set; }
        public byte[] VideoData { get; set; }
        public string VideoName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

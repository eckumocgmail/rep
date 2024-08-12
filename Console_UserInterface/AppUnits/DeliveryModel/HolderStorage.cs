using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Blazor_UserInterface.AppUnits.DeliveryModel
{
    /// <summary>
    /// Полка на складе
    /// </summary>
    public class HolderStorage: BaseEntity
    {
        public int HolderId { get; set; }
        public Holder Holder { get; set; }
        public string StorageCode { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}

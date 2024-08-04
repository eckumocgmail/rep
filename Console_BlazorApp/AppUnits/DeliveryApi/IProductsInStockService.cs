using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IProductsInStockService
    {
        public IEnumerable<int> GetReservationStocksIds(int orderId);
        public IEnumerable<Holder> GetReservationStocks(int orderId);
    }
}

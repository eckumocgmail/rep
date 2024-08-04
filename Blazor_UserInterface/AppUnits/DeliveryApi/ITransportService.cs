using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface ITransportService
    {
        
        void PutOrder(int orderId, int transportId);
        void CancelOrder(int orderId, int transportId);
        void TakeOrder(int orderId, int transportId);
        IEnumerable<Order> GetActiveList(int transportId);
        IEnumerable<Order> GetCompletedList(int transportId);
        IEnumerable<Order> GetAvailableList(int transportId);
        IEnumerable<Order> GetCancelledList(int transportId);
        IEnumerable<Order> GetAvailableOrders(int transportId);        
        IEnumerable<Order> GetCompletedOrders(int transportId);
    }
}
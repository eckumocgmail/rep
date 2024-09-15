using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{

    /// <summary>
    /// Функции польщователей в роли курьера
    /// </summary>
    public interface ITransportService
    {
        /// <summary>
        /// Передать заказ на отгрузку
        /// </summary>      
        void PutOrder(int orderId, int transportId);

        /// <summary>
        /// Отменить заказ
        /// </summary>   
        void CancelOrder(int orderId, int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        void TakeOrder(int orderId, int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetActiveList(int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetCompletedList(int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetAvailableList(int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetCancelledList(int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetAvailableOrders(int transportId);

        /// <summary>
        /// Взять заказ в работу 
        /// </summary>
        IEnumerable<Order> GetCompletedOrders(int transportId);
    }
}
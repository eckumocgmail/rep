using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;
using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    public class TransportService : ITransportService
    {
        private readonly DeliveryDbContext _deliveryDbContext;
        //private readonly SigninUser _signin;

        public TransportService(DeliveryDbContext _deliveryDbContext/*, SigninUser signin*/)
        {
            this._deliveryDbContext = _deliveryDbContext;
            //this._signin = signin;
        }

        public IEnumerable<Order> GetAvailableOrders(int transportId)
        {
            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.OrderStatus == 2 && order.TransportId == null).ToList();
        }

        public IEnumerable<Order> GetOrders(int transportId)
        {
            /*if (this._signin.IsSignin() == false)
                throw new UnauthorizedAccessException("Метод должен использоваться только авторизованными пользователяими");
            var userId = this._signin.Verify().Id;
            var transport = _deliveryDbContext.Transports.FirstOrDefault(t => t.UserId == userId);
            if (transport is null)
            {
                throw new NullReferenceException($"Нет ссылки на объект Transport для пользователя с ид {userId}");
            }*/
            return _deliveryDbContext.Orders.Where(order => order.TransportId == transportId).ToList();
        }

        /// <summary>
        /// Взять заявку на доставку в работу
        /// </summary>
        /// <param name="orderId">ид заказа</param>
        /// <param name="transportId">ид курьера</param>
        public void TakeOrder(int orderId, int transportId)
        {
            _deliveryDbContext.Orders.First(order => order.Id == orderId).OnOrderIsDelivering();
            _deliveryDbContext.Orders.First(order => order.Id == orderId).TransportId = transportId;
            _deliveryDbContext.SaveChanges();
        }

        /// <summary>
        /// Завершить работу над доставкой заказа
        /// </summary>
        /// <param name="orderId">ид заказа</param>
        /// <param name="transportId">ид курьера</param>
        public void PutOrder(int orderId, int transportId)
        {
            _deliveryDbContext.Orders.First(order => order.Id == orderId).OnOrderDelivered();
            _deliveryDbContext.Orders.First(order => order.Id == orderId).TransportId = transportId;
            _deliveryDbContext.SaveChanges();
        }

        /// <summary>
        /// Завершить работу над доставкой заказа
        /// </summary>
        /// <param name="orderId">ид заказа</param>
        /// <param name="transportId">ид курьера</param>
        public void CancelOrder(int orderId, int transportId)
        {
            _deliveryDbContext.Orders.First(order => order.Id == orderId).OnOrderCanceled();
            _deliveryDbContext.Orders.First(order => order.Id == orderId).TransportId = transportId;
            _deliveryDbContext.SaveChanges();
        }

        /// <summary>
        /// Список выполненных заказов
        /// </summary>       
        public IEnumerable<Order> GetCompletedOrders(int transportId)
        {
            /*if (this._signin.IsSignin() == false)
                throw new UnauthorizedAccessException("Метод должен использоваться только авторизованными пользователяими");
            var userId = this._signin.Verify().Id;
            var transport = _deliveryDbContext.Transports.FirstOrDefault(t => t.UserId == userId);
            if (transport is null)
            {
                throw new NullReferenceException($"Нет ссылки на объект Transport для пользователя с ид {userId}");
            }*/

            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.TransportId == transportId && new int[] { 4, 5, 7 }.Contains(order.OrderStatus)).ToList();
        }

        public IEnumerable<Order> GetActiveList(int transportId)
        {
            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.OrderStatus == 3 && order.TransportId == transportId).ToList();
        }

        public IEnumerable<Order> GetCompletedList(int transportId)
        {
            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.OrderStatus>=4 && order.TransportId == transportId).ToList();
        }

        public IEnumerable<Order> GetAvailableList(int transportId)
        {
            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.OrderStatus == 2 && order.TransportId == null).ToList();
        }

        public IEnumerable<Order> GetCancelledList(int transportId)
        {
            return _deliveryDbContext.Orders.Include(Order => Order.Holder).Where(order => order.OrderStatus == 6 && order.TransportId == null).ToList();
        }
    }
}

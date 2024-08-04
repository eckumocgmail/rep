using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    public class TransportUser : ITransportUser
    {
        private readonly SigninUser signin;
        private readonly DeliveryDbContext deliveryDbContext;
        private readonly ITransportService service;

        public TransportUser(DeliveryDbContext deliveryDbContext, ITransportService service, SigninUser signin)
        {
            this.signin = signin;
            this.deliveryDbContext = deliveryDbContext;
            this.service = service;
        }

        public int GetTransportId()
        {
            var transport = deliveryDbContext.Transports.FirstOrDefault(t => t.UserId == this.signin.Verify().Id);
            if(transport is null)
            {
                deliveryDbContext.Transports.Add(transport=new Transport()
                {
                    UserId = this.signin.Verify().Id
                });
                deliveryDbContext.SaveChanges();
                //throw new Exception("Не зарегистрирован объект Transport для пользователя");
            }
            return transport.Id;
        }

        public void PutOrder(int orderId)
        {
            service.PutOrder(orderId, GetTransportId());
        }
        public void CancelOrder(int orderId)
        {
            service.CancelOrder(orderId, GetTransportId());
        }

        public void TakeOrder(int orderId)
        {
            service.TakeOrder(orderId, GetTransportId());
        }

        public IEnumerable<Order> GetActiveList()
        {
            return service.GetActiveList(GetTransportId());
        }

        public IEnumerable<Order> GetCompletedList()
        {
            return service.GetCompletedList(GetTransportId());
        }

        public IEnumerable<Order> GetAvailableList()
        {
            return service.GetAvailableList(GetTransportId());
        }

        public IEnumerable<Order> GetCancelledList()
        {
            return service.GetCancelledList(GetTransportId());
        }

        public IEnumerable<Order> GetAvailableOrders()
        {
            return service.GetAvailableOrders(GetTransportId());
        }

        public IEnumerable<Order> GetCompletedOrders()
        {
            return service.GetCompletedOrders(GetTransportId());
        }
    }

    public interface ITransportUser
    {
        void CancelOrder(int orderId);
        void PutOrder(int orderId);
        void TakeOrder(int orderId);
        IEnumerable<Order> GetActiveList();
        IEnumerable<Order> GetCompletedList();
        IEnumerable<Order> GetAvailableList();
        IEnumerable<Order> GetCancelledList();
        IEnumerable<Order> GetAvailableOrders();
        IEnumerable<Order> GetCompletedOrders();
    }
}

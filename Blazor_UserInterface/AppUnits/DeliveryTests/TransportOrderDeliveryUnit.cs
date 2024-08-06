using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Console_BlazorApp.AppUnits.DeliveryServices;
using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryTests
{
    [Label("Модуль управления службой доставки")]
    [Description("Служба доставки просматривает созданные заказы, " +
        "назначает на конкретного исполнителя(курьера), исполнитель доставляет и заказ готов к выдачи.")]
    public class TransportOrderDeliveryUnit : TestingElement
    {
        private readonly ICustomerService _customerService;
        private readonly ITransportService _transportService;
        private readonly IHolderService _holderService;
        private readonly DeliveryDbContext _deliveryDbContext;
        private readonly DbContextUser _dbContextUser;

        public TransportOrderDeliveryUnit(IServiceProvider provider): base(provider)
        {
       
            _dbContextUser = provider.Get<DbContextUser>();
            _customerService = provider.Get<ICustomerService>();
            _transportService = provider.Get<ITransportService>();
            _holderService = provider.Get<IHolderService>();
            _deliveryDbContext = provider.Get<DeliveryDbContext>();
        }

        public override void OnTest()
        {            
            Assert((test) => 
            {
                if (_deliveryDbContext.Customers.Count() == 0)
                {
                    _deliveryDbContext.Add(CustomerGenerator.CreateCustomer());
                    _deliveryDbContext.SaveChanges();
                }

                if (_deliveryDbContext.Transports.Count() == 0)
                {
                    _deliveryDbContext.Transports.Add(new Transport()
                    {
                        Latitude = 60.000000f,
                        Longitude = 30.000000f,
                        UserId = _dbContextUser.UserContexts_.First().Id
                    });
                }
                if (_deliveryDbContext.Products.Count() == 0)
                {
                    var init = new DeliveryDbContextInitiallizer();
                    init.InitProducts(_deliveryDbContext);
                }
                    
                if (_deliveryDbContext.Holders.Count() == 0)
                {
                    var init = new DeliveryDbContextInitiallizer();
                    init.InitHolders(_deliveryDbContext);
                }

                var order = _customerService.CreateOrder(
                     _deliveryDbContext.Customers.First().Id,
                     new Dictionary<Product, int>() {
                        { _deliveryDbContext.Products.First(), 1 }
                     },
                     _deliveryDbContext.Holders.First().Id);

                _holderService.SetOrderStored(_deliveryDbContext.Holders.First().Id, order);
                order.OnOrderStored();

                
                var orders = _transportService.GetAvailableOrders(_deliveryDbContext.Transports.First().Id);
                var result = orders.Any(o => o.Id == order.Id);
                return result;
            },
            "Функция получение заказов для службы доставки работает корректно",
            "Функция получение заказов для службы доставки работает не корректно");


            Assert((test) =>
            {
                var order = _customerService.CreateOrder(
                    _deliveryDbContext.Customers.First().Id,
                    new Dictionary<Product, int>() {
                        { _deliveryDbContext.Products.First(), 1 }
                    },
                    _deliveryDbContext.Holders.First().Id);

                _holderService.SetOrderStored(_deliveryDbContext.Holders.First().Id, order);
                order.OnOrderStored();

                _deliveryDbContext.SaveChanges();

                if (_deliveryDbContext.Transports.Count() == 0)
                {
                    _deliveryDbContext.Transports.Add(new Transport()
                    {
                        Latitude = 60.000000f,
                        Longitude = 30.000000f,
                        UserId = _dbContextUser.UserContexts_.First().Id
                    });
                }

                var orders = _transportService.GetAvailableOrders(_deliveryDbContext.Transports.First().Id);
                 
                return orders.Any(o => o.Id == order.Id);
            },
            "Функция назначения заказов для службы доставки работает корректно",
            "Функция получение заказов для службы доставки работает не корректно");
 
        }

        /*public IEnumerable<Order> GetAvailableOrders(   )
        {
            return _deliveryDbContext.Orders.Where(order => order.OrderStatus == 2);
        }

        public void DoOrder(int orderId, int transportId )
        {
            _deliveryDbContext.Orders.First( order => order.Id == orderId ).OnOrderIsDelivering();
            _deliveryDbContext.Orders.First( order => order.Id == orderId ).TransportID = transportId;
            _deliveryDbContext.SaveChanges();
            Messages.Add("Транспортная компания взяла в работу заказ на доставку");
        }

        public void DoneOrder(int orderId, int transportId)
        {
            _deliveryDbContext.Orders.First(order => order.Id == orderId).OnOrderDelivered();
            _deliveryDbContext.Orders.First(order => order.Id == orderId).TransportID = transportId;
            _deliveryDbContext.SaveChanges();
            Messages.Add("Транспортная компания выполнила заказ на доставку");
        }*/
    }
}
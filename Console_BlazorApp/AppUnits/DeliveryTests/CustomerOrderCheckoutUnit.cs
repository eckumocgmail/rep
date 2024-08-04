using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Console_BlazorApp.AppUnits.DeliveryServices;
using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Pqc.Crypto.Lms;

using pickpoint_delivery_service;

using System.Collections.Generic;
using System.Linq;

namespace Console_BlazorApp.AppUnits.DeliveryTests
{

    [Label("Тестирование процесса оформления заказа со стороны клиента компании")]
    public class CustomerOrderCheckoutUnit : TestingElement
    {
        public CustomerOrderCheckoutUnit(IServiceProvider provider): base(provider)
        {
        }
        public void OnTestData()
        {



            using (var db = new DeliveryDbContext())
            {
                /*db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                new DeliveryDbContextInitiallizer().Init(db, "D:\\System-Config\\ProjectsConsole\\Console_AuthModel");
                Messages.Add("Инициаллизация первичных данных выполнена успешно");

                var customer = new CustomerContext()
                {

                    FirstName = "FirstName",
                    LastName = "LastName",
                    Tel = "PhoneNumber",
                };

                db.Add(customer);
                db.SaveChanges();

                var order = new Order()
                {
                    CustomerID = customer.Id
                };
                order.HolderID = db.Holders.First().Id;
                db.Add(order);
                db.SaveChanges();

                var orders = new OrdersService(db);
                Order value = orders.CreateOrder(customer.Id);
                orders.GetOrders().ToJsonOnScreen().WriteToConsole();
                orders.GetOrderItems(orderId: value.Id).ToJsonOnScreen().WriteToConsole();
                Messages.Add("Добавлен новый заказ");*/

            }

        }
        public override void OnTest()
        {
            AssertService<DeliveryDbContext>(
                db => {
                    var productsInStockService = provider.Get<IProductsInStockService>();
                    var holdersService = provider.Get<IHolderService>();
                    var service = provider.Get<ICustomerService>();
                    var products = service.SearchProducts("");

                    Messages.Add($"Ищю товары по запросу: dell");
                    products = service.SearchProducts("dell");
                    //this.Messages.Add(products.ToJsonOnScreen());

                    Messages.Add($"Ищю товары по запросу: venue");
                    products = service.SearchProducts("venue");
                    //this.Messages.Add(products.ToJsonOnScreen());

                    var customer = new CustomerContext()
                    {
                        FirstName = "Batov",
                        LastName = "Konstantin",
                        PhoneNumber = "79210903572",
                    };
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    if(db.Holders.Count()==0)
                    {
                        var initiallizer = new DeliveryDbContextInitiallizer();
                        initiallizer.InitHolders(db);
                    }

                    var order = service.CreateOrder(db.Customers.First().Id, new Dictionary<Product, int>(
                        products.Select(item => new KeyValuePair<Product, int>(item, 1))
                    ), db.Holders.First().Id);
                    service.OrderCheckout(order);
                    Messages.Add("Заказ оформлен");




                    /*if (orders.Count() == 0)
                    {
                        var products = _customerService.SearchProducts("dell");
                        if (products.Count() == 0)
                            throw new Exception("Необходимо зарегистрировать товары");
                        var customer = _customerService.GetCustomerByPhone("79210903572");
                        _customerService.CreateCustomer("Константин", "Батов", "79210903572");
                        var holderId = _customerService.GetHolders().First().Id;
                        var porder = _customerService.CreateOrder(customer.Id, new Dictionary<Product, int>()
                            {
                                { products.First(), 1}
                            }, holderId);

                    }*/
                    return true;
                },
                "",
                ""
            );
            
        }
    }
}
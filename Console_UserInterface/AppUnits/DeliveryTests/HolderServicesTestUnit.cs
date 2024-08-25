using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryServices;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryTests
{
    [Label("Модуль тестирования функции работы со складом")]
    public class HolderServicesTestUnit : TestingElement
    {

        public HolderServicesTestUnit(IServiceProvider provider):base(provider)  
        {
        }

        public override void OnTest()
        {
            /**
                int PaymentDelivery(int orderId);
                int GetProductCount(int holderId, int productId);
                IEnumerable<ProductsInStock> GetProductOffer(int holderId);
                List<Product> GetProducts(int holderId);
                Dictionary<int, int> GetProductsCountsInStock(int holderId);
                Dictionary<int, int> GetProductsCounts(int holderId);
                Dictionary<int, int> CreateOrder(int holderId, Dictionary<int, int> targetProductsCount);
                void SetOrderStored(int holderId, Order order);             
             */
            AssertService<IHolderService>(holderService =>
            {
                var holder = provider.Get<DeliveryDbContext>().Holders.ToList().First();
                var products = holderService.GetProductsCounts(holder.Id);
                new
                {
                    holder = holder,
                    products = products
                }.ToJsonOnScreen().WriteToConsole();

                foreach(var key in products.Keys)
                {
                    products[key] = products[key]+1;
                }

                var items = holderService.CreateOrder(holder.Id, products);
                Assert(el => items.All(p => p.Value == 1), 
                    $"Формирование автозаказа на оптовый склад по адресу {holder.HolderLocation} работает корректно",
                    $"Формирование автозаказа на оптовый склад по адресу {holder.HolderLocation} работает не корректно");

                return true;                
            },  "", 
                "");
            try
            {
                /*using (var db = new DeliveryDbContext())
                {
                    var customer = provider.Get<CustomerService>();
                    var holder = provider.Get<HolderService>();
                    var pholder = db.Holders.First();
                    var pcustomer = db.Customers.First();
                    customer.CreateOrder(pcustomer.Id);
                    var offer1 = holder.GetProductOffer(pholder.Id);
                    customer.AddItemToOrder(offer1.First().Product, (int)pcustomer.CurrentOrderId);
                    var order = db.Orders.Find(pcustomer.CurrentOrderId);
                    customer.OrderCheckout(order);

                    var offer2 = holder.GetProductOffer(pholder.Id);
                    foreach (var line in offer2)
                    {
                        var p = offer1.FirstOrDefault(p => p.ProductID == line.ProductID);
                        var p2 = order.OrderItems.FirstOrDefault(p => p.ProductID == line.ProductID);
                        if ((p is not null ? p.ProductCount : 0) !=
                            (line.ProductCount + (p2 is not null ? p2.ProductCount : 0)))
                        {
                            throw new Exception("Кол-во товара учитывается невернно");
                        }
                    }
                    var shouldBe = holder.GetProductsCountsInStock(pholder.Id);
                    var orderItems = holder.CreateOrder(pholder.Id, shouldBe);
                }*/
            }
            catch (Exception ex)
            {
                Messages.Add("Функции работы сос кладом не работают");
            }



        }
    }
}
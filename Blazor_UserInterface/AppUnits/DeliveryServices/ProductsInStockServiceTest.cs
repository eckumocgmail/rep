using Console_BlazorApp.AppUnits.DeliveryApi;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    public class ProductsInStockServiceTest : TestingElement
    {
        public ProductsInStockServiceTest(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest()
        {
            AssertService<IProductsInStockService>(service => {
                foreach(var item in provider.Get<DeliveryDbContext>().Orders.ToList())
                {
                    service.GetReservationStocksIds(item.Id);
                }                
                return true;
            },
            "Получение вариантов резервирования работает некорректно",
            "Получение вариантов резервирования работает успешно");
        }
    }
}

using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryServices;

using pickpoint_delivery_service;

using System.Diagnostics.Metrics;

namespace Console_BlazorApp.AppUnits.DeliveryTests
{
    /// <summary>
    /// Учет товаров
    /// </summary>
    [Label("Проверка функции бронирования товарных позиций")]
    public class ReservationOrderCheckoutUnit : TestingElement
    {

        public ReservationOrderCheckoutUnit(IServiceProvider provider):base(provider)
        {
        }

        public override void OnTest()
        {
            this.Info($"начинаю тестирование ... ");
            using(var db = new DeliveryDbContext())
            {
                var customerService = new CustomerService(db);
                var products = customerService.SearchProducts("");
                if (products is null || products.Count() == 0)
                {
                    return;
                }
                var product = products.First( p => p.ProductName.IndexOf("streak")!=-1);
                Dictionary<int, int> instocks = customerService.SearchProductHolders(product.Id, 1);
                this.Info(instocks.ToJsonOnScreen());
                if (instocks.Count() == 0)
                    return;
                var holderId = instocks.Keys.First();
                int count = instocks.Values.First();
                customerService.SetProductReserved(product.Id, 1, holderId);
                var instocks2 = customerService.SearchProductHolders(product.Id, 1);

                this.Info(instocks[holderId]);
                this.Info(instocks2[holderId]);
                if ((instocks[holderId] - instocks2[holderId]) != 1)
                {

                    Messages.Add($"При получении сведений о наличии товаров на складах не учитывает резерв товара.");
                }
                else
                {
                    Messages.Add($"Функция учёта товара в резерве работает корректно");
                }
            }
            
            
        }
    }
}


using Console_AuthModel.AuthorizationServices;

using Console_BlazorApp.AppUnits;
using Console_BlazorApp.AppUnits.AuthorizationTests;
using Console_BlazorApp.AppUnits.DeliveryTests;

namespace Console_BlazorApp
{
    [Label("Тестирование приложения для продажи и доставки авто-товаров.")]
    public class DeliveryServicesUnit : TestingUnit
    {
        public DeliveryServicesUnit( IServiceProvider provider = null ): base(provider)
        {
            if( provider is null )
            {
                provider = AppProviderService.GetSingletonInstance();
            }
            this.Append(new HolderServicesTestUnit(provider));
            
            
            //this.Append(new CustomerOrderCheckoutUnit(provider));
            //this.Append(new TransportOrderDeliveryUnit(provider));
            //this.Append(new ReservationOrderCheckoutUnit(provider));
            //this.Append(new AuthorizationUnit(provider));
        }

        
    }
}

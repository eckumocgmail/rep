using Console_UserInterface.App;
using Console_UserInterface.AppUnits;

namespace Console_UserInterface
{

    [Label("Модуль тестирования пользовательского интерфейса")]
    public class UserInterfaceUnit : TestingUnit
    {      
        public UserInterfaceUnit(IServiceProvider parent) : base(parent)
        {
            
            this.Append(new LocationUnit(provider));
            this.Append(new AuthorizationUnit(provider));
            
            this.Append(new DeliveryServicesUnit());
            this.Append(new AppServicesUnit());
            
        }
    }

}

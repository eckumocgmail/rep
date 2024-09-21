using Console_UserInterface.AppUnits;

namespace Console_UserInterface
{
    [Label("Модуль тестирования пользовательского интерфейса")]
    public class UserInterfaceUnit : TestingUnit
    {      
        public UserInterfaceUnit(IServiceProvider parent) : base(parent)
        {
            Append(new DeliveryServicesUnit());
        }
    }
}

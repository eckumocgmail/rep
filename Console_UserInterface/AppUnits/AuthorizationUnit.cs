using Console_BlazorApp.AppUnits.AuthorizationTests;
using Console_BlazorApp.AppUnits;
using Console_BlazorApp.AppUnits.AuthorizationServices.ServiceApi;

namespace Console_UserInterface.AppUnits
{
    [Label("Проверка авторизации")]
    [Description("Проверяем модули аутентификации должны быть доступны функции: вход, выход, регистрация")]
    public class AuthorizationUnit : TestingUnit
    {
        public AuthorizationUnit(IServiceProvider provider)
        {
            Append(new AuthorizationCollectionTest(provider));
            Append(new AuthorizationUsersTest(provider));
            Append(new AuthorizationServicesTest(provider));
            Append(new AuthorizationServiceTest(provider));
            Append(new DbContextServiceTest(provider));
            Append(new UserRolesTest(provider));
            Append(new UserMessagesTest(provider));
        }
    }
}
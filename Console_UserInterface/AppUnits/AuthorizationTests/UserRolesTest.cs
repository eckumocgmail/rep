namespace Console_BlazorApp.AppUnits.AuthorizationTests
{
    [Label("Проверка таблицы UserRoles")]
    public class UserRolesTest : TestingElement
    {
        public UserRolesTest(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest()
        {
            this.provider.Get<SignupUser>().SignupRoles(new UserAccount("test@gmail.com", "test@gmail.com") , 
                new UserPerson("Константин", "Батов", "Александрович", DateTime.Parse("26.08.1989"), "7-921-090-3572") {
                
            },new string[] { });
            this.provider.Get<SigninUser>().Signin("test@gmail.com", "test@gmail.com");
        }
    }
}
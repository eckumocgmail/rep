using Console_UserInterface.Pages.App;

namespace Console_UserInterface.Services.App
{
    public class AppBuilderTest : TestingElement
    {
        public override void OnTest()
        {

            this.Info(AppBuilder.CreateNavMenu(typeof(FormsContrustorView).Namespace, new Dictionary<string, string>()
            {

            }).ToJsonOnScreen());
        }
    }
}

using Console_UserInterface.Pages.App;

namespace Console_UserInterface.Services
{
    public class AppBuilderTest : TestingElement
    {
        public override void OnTest()
        {

            this.Info(AppBuilder.CreateNavMenu(typeof(FormsContrustor).Namespace, new Dictionary<string, string>()
            {

            }).ToJsonOnScreen());
        }
    }
}

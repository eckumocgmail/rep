namespace Console_UserInterface.AppUnits
{
    public class TypeCustomizationTest : TestingElement
    {
        public override void OnTest()
        {
            Assert(p =>
            {
                var ptype = typeof(ViewBuilder);
                var attrs = ptype.GetTypeAttributes();
                return true;
            }, "", "");
        }
    }
}
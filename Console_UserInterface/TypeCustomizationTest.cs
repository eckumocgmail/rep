namespace Console_UserInterface
{
    public class TypeCustomizationTest: TestingElement
    {
        public override void OnTest()
        {
            Assert(p =>
            {
                var ptype = typeof(ViewBuilder);
                var attrs = TypeAttributesExtensions.GetTypeAttributes(ptype);
                return true;



            }, "","");
        }
    }
}
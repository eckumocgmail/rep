public class InputStructureCollectionAttributeTest : TestingElement<InputStructureCollectionAttribute>
{
    public override void OnTest()
    {
        var attr = new InputStructureCollectionAttribute();
        this.Assert(test => false == attr.IsValidValue(new List<string>()),
            "Удалось определить стукртурированную коллекцию",
            "Не удалось определить стукртурированную коллекцию");
        this.Assert(test => false == attr.IsValidValue(new List<int>()),
            "Удалось определить стукртурированную коллекцию",
            "Не удалось определить стукртурированную коллекцию");
        this.Assert(test => true == attr.IsValidValue(new List<object>()),
            "Удалось определить стукртурированную коллекцию",
            "Не удалось определить стукртурированную коллекцию");
        this.Assert(test => true == attr.IsValidValue(new List<Attribute>()),
            "Удалось определить стукртурированную коллекцию",
             "Не удалось определить стукртурированную коллекцию");
    }

    public static void Main(string[] args)
    {
        new InputPrimitiveCollectionAttributeTest().DoTest(false).ToDocument().WriteToConsole();
    }

}
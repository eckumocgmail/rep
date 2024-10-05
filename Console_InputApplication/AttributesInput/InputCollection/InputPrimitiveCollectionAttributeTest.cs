public class InputPrimitiveCollectionAttributeTest: TestingElement<InputPrimitiveCollectionAttribute>
{
    public override void OnTest()
    {
        var attr = new InputPrimitiveCollectionAttribute();
        this.Assert(test => true == attr.IsValidValue(new List<string>()), 
            "Удалось определить примитивную коллекцию", 
            "Не удалось определить примитивную коллекцию");
        this.Assert(test => true == attr.IsValidValue(new List<int>()), 
            "Удалось определить примитивную коллекцию",
            "Не удалось определить примитивную коллекцию");
        this.Assert(test => false == attr.IsValidValue(new List<object>()), 
            "Удалось определить примитивную коллекцию",
            "Не удалось определить примитивную коллекцию");
        this.Assert(test => false == attr.IsValidValue(new List<Attribute>()), 
            "Удалось определить примитивную коллекцию",
             "Не удалось определить примитивную коллекцию");
    }

    public static void Main(string[] args)
    {
        new InputPrimitiveCollectionAttributeTest().DoTest(false).ToDocument().WriteToConsole();
    }
}
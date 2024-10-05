
public class InputCollectionUnit : TestingUnit
{
    public InputCollectionUnit()
    {
        Append(new InputPrimitiveCollectionAttributeTest());
        Append(new InputStructureCollectionAttributeTest());
    }
}


public class InputInputTypeAttributeTest: TestingElement<InputInputTypeAttribute>
{
    public override void OnTest()
    {
        this.Info(InputInputTypeAttribute.GetInputTypes().ToJsonOnScreen());
    }
}
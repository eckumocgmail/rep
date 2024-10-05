
public class InputControlTypeAttributeTest : TestingElement<InputInputTypeAttribute>
{
    public override void OnTest()
    {
        this.Info(InputControlTypeAttribute.GetInputTypes().ToJsonOnScreen());
    }
}
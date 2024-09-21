
using Console_InputApplication.InputApplicationModule.Files;

public class HelpService
{
    public TypeNode<String> GetContents()
    {
        var ctrl = new FileController<TypeNode<String>>
        (
            Path.Combine
            (
                Directory.GetCurrentDirectory(),
                "Help",
                "Contents"
            )
        );
        return ctrl.Get();
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomAttributesUnit
{
    public static void Test()
    {
        CustomDbContext.Build();
        var provider = new CustomDataProvider(new CustomDbContext());
        provider.ToType(typeof(ViewBuilder));
        var service = new CustomService(new CustomDbContext());
        var attrs = service.GetAttributes(nameof(ViewBuilder));
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(attrs));
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(service.GetMembersAttributes(nameof(ViewBuilder))));
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(provider.GetTypes()));
    }
}
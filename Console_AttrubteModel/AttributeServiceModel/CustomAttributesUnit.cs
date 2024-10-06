using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Модуль тестирования
/// </summary>
public class CustomAttributesUnit
{
    public static void Test()
    {
        CustomDbContext.Build();

        var ptype = typeof(ViewBuilder);
        var a1 = ptype.GetAttributes();
        ptype.AddAttribute("Label", "Тест");
        var a2 = ptype.GetAttributes();
        if (a2.ContainsKey("Label")==false)
            throw new Exception("Добавление аттрибута не работает");
        ptype.RemoveAttribute("Label");
        var a3 = ptype.GetAttributes();
        if (a3.ContainsKey("Label"))
            throw new Exception("Удаление аттрибута не работает");
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(a1));
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(a2));
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(a3));

    }
}
using System.ComponentModel.DataAnnotations.Schema;

[Label("Настройки сервиса")]
public class ServiceSettings: BaseEntity
{

    [Label("URL адрес OpenApi-схемы")]
    [NotNullNotEmpty("Необходимо указать текст OpenApi документ")]
    public string OpenApi { get; set; } = "https://sps.euroauto.ru/api/detaliusbot/swagger/index.html";

    [Label("Тип авторизации")]
    [InputSelect("Basic,JWT")]
    [NotNullNotEmpty]
    public string AuthScheme { get; set; } = "Basic";

    [NotInput]
    [Label("Json")]
    public string AuthOptionsJson { get; set; } = "{}";
    [NotMapped]
    public Dictionary<string, string> AuthOptions
    {
        get => Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(AuthOptionsJson);
        set => AuthOptionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
    }
     

}
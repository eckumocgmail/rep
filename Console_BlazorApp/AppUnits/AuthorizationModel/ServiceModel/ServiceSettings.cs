public class ServiceSettings: BaseEntity
{
    [NotNullNotEmpty("Необходимо указать текст OpenApi документ")]
    public string OpenApi { get; set; } = "https://sps.euroauto.ru/api/detaliusbot/swagger/index.html";
}
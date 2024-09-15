using System.ComponentModel;

/// <summary>
/// Активные объекты.
/// Проходят процедуру авторизации в приложении.
/// Система будет отслеживать их состояние.
/// </summary>
public class ActiveObject : NamedObject, IActiveObject
{

    [DisplayName("Последнее посещение")]
    public long LastActive { get; set; }
    public DateTime LastActiveTime { get; set; }
    public string UserAgent { get; set; }

    [DisplayName("Онлайн")]
    public bool IsActive { get; set; } = false;

    [DisplayName("Секретный ключ")]
    public string SecretKey { get; set; } = "";

    [InputUrl("Значение не похоже на URL")]
    public string URL { get; set; } = "";

    public async Task DoCheck(object context, string key)
    {
        this.Info($"DoCheck({context},{key})");
        await Task.CompletedTask;
    }

    [InputTcpIp4Address]
    public string Ip4 { get; set; }


    public DateTime GeoUpdated { get; set; } = DateTime.Now;
    public string Latitude { get; set; } = "60.000000";
    public string Longitude { get; set; } = "30.000000";
} 
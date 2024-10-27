[Label("Активный объект сеанса")]
public interface IActiveObject
{
    [Label("Признак активности")]
    [InputBool]
    bool IsActive { get; set; }

    [Label("Время последнего обращения")]
    [InputNumber]
    long LastActive { get; set; }

    [Label("Время последнего обращения")]
    [InputDateTime]
    DateTime LastActiveTime { get; set; }

    [Label("Клоюч доступа")]
    [InputText]
    string SecretKey { get; set; }

    [Label("Адрес текущей странице")]
    [InputUrl]
    string URL { get; set; }

    //[Label("ИП адрес клиента")]
    //[InputTcpIp4Address]
    //string Ip4 { get; set; }

    //[Label("Браузер пользователя")]
    //[InputEngWord]
    //string UserAgent { get; set; }

    [Label("Актуализация состояния")]
    Task DoCheck(object context, string key);
}

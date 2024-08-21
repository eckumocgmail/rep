[Label("Сертификат доступа")]
[Description("Устанавливается на клиента для получения доступа к сервису")]
public class ServiceSertificate: BaseEntity
{
    [Label("Открытый ключ")]
    [NotNullNotEmpty("Необходимо указать открытый ключ")]
    [InputFileData()]
    public byte[] PublicKey { get; set; } = new byte[0];

    [Label("Закрытый ключ")]
    [NotNullNotEmpty("Необходимо указать закрытый ключ")]
    [InputFileData()]
    public byte[] PrivateKey { get; set; } = new byte[0];
}
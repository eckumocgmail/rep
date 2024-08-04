public class ServiceSertificate: BaseEntity
{

    [NotNullNotEmpty("Необходимо указать открытый ключ")]
    public byte[] PublicKey { get; set; } = new byte[0];

    [NotNullNotEmpty("Необходимо указать закрытый ключ")]
    public byte[] PrivateKey { get; set; } = new byte[0];
}
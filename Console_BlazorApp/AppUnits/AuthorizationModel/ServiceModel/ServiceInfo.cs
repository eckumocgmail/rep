// == UserPerson
public class ServiceInfo:  BaseEntity
{
    [NotNullNotEmpty]
    public string Url { get; set; } = "https://localhost";
    [NotNullNotEmpty]
    public string Name { get; set; } = "Api";
    [NotNullNotEmpty]
    public string Version { get; set; } = "1.0.0";
}
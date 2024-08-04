[Label("Настройки")]
public partial class UserSettings : BaseEntity
{
    public bool EnableNotifications { get; set; }
    public bool SendNews { get; set; }
    public string ColorTheme { get; set; } = "light";

}
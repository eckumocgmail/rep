[Label("Настройки")]
public partial class UserSettings : BaseEntity
{
    [Label("Включить уведомления")]
    public bool EnableNotifications { get; set; }

    [Label("Отправлять новости на электронную почту")]
    public bool SendNews { get; set; }

    [Label("Цветовая схема")]
    public string ColorTheme { get; set; } = "light";


}
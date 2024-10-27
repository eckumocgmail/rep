[Label("Настройки")]
public partial class UserSettings : BaseEntity
{

    [Label("Включить уведомления")]
    public bool EnableNotifications { get; set; }


    [Label("Отправлять новости на электронную почту")]
    public bool SendNews { get; set; }


    [Label("Цветовая схема")]
    public string ColorTheme { get; set; } = "light";


    [Label("Размер текста")]
    [InputSelect("малеьнкий,средний,большой")]
    public string TextSize { get; set; } = "большой";


    [Label("Меню навигации")]
    [InputSelect("сверху,Снизу")]
    public string NavPosition { get; set; } = "сверху";


    [Label("Интерактивная помощь")]
    public bool InteractiveSupport { get; set; } = true;

}
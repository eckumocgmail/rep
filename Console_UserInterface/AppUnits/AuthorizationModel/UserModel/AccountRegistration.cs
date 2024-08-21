
using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AccountRegistration : BaseEntity
{


    [InputOrder(1)]
    [InputEmail("Электронный адрес задан некорректно")]
    [Label("Электронный адрес")]
    [NotNullNotEmpty("Не указан электронный адрес")]
    [Icon("email")]
    [UniqValue("Этот адрес электронной почты уже зарегистрирован")]
    [JsonProperty("Email")]
    public string Email { get; set; }


    [InputOrder(2)]
    [Label("Пароль")]
    [NotNullNotEmpty]
    [InputPassword()]
    [NotMapped]

    [JsonProperty("Password")]
    public string Password { get; set; }

    [InputOrder(3)]
    [Label("Подтверждение")]
    [NotNullNotEmpty]
    [InputPassword()]
    [NotMapped]
    [Compare("Password")]

    [JsonProperty("Confirmation")]
    public string Confirmation { get; set; }
}


using Newtonsoft.Json;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Ученая запись пользователя
/// </summary>
[Label("Учетная запись")]
[Icon("account_box")]
public class UserAccount: BaseEntity
{



    [InputEmail("Электронный адрес задан некорректно")]
    [Label("Электронный адрес")]
    [NotNullNotEmpty("Не указан электронный адрес")]
    [Icon("email")]
    [UniqValue("Этот адрес электронной почты уже зарегистрирован")]
    [JsonProperty("Email")]
    public string Email { get; set; }


    [Label("Пароль")]
    [NotNullNotEmpty]
    [InputPassword()]
        

    [JsonProperty("Password")]
    public string Password { get; set; }


    /// <summary>
    /// Время активации
    /// </summary>
    [AllowNull]        
    //[InputDate( )]
    [InputHidden(true)]
    [NotInput("Свойство " + nameof(Activated) + " не вводится пользователем, оно устанавливается системой после ввода ключа активации")]
    public DateTime? Activated { get; set; }

    [InputHidden(true)]
    [NotInput("Свойство " + nameof(ActivationKey) + " не вводится пользователем, оно устанавливается системой перед созданием сообщения на эл. почту пользорвателя с инструкциями по активации")]
    public string ActivationKey { get; set; } = "";

    [Label("Хэш-ключ")]
    [InputHidden(true)]
    [NotNull]
    [NotInput("Свойство " + nameof(Hash) + " не вводится пользователем, оно устанавливается системой при регистрации")]
    public string Hash { get; set; }

    [Label("Радио метка")]
    [InputHidden(true)]
    [NotInput("Свойство " + nameof(RFID) + " не вводится пользователем, оно устанавливается системой при регистрации служебного билета")]
    public string RFID { get; set; } = "";


    public UserAccount() :base(){ }
    public UserAccount(string email, string password):base(   )
    {
            
        this.Email = email;
        this.Password = password;
        this.Hash = GetHashSha256(password);
    }


    /// <summary>
    /// Хэширование текстых данных
    /// </summary>
    /// <param name="text"> текст </param>
    /// <returns> результат хэширования </returns>
    public static string GetHashSha256(string text)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(text);
        SHA256Managed hashstring = new SHA256Managed();
        byte[] hash = hashstring.ComputeHash(bytes);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += String.Format("{0:x2}", x);
        }
        return hashString;
    }

}

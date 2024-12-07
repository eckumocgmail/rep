using Microsoft.EntityFrameworkCore;
using System;

[Label("Личные данные")]
//[UniqValues($"{nameof(SurName)},{nameof(FirstName)},{nameof(LastName)}")]
[Index(nameof(SurName),nameof(LastName))]
public class UserPerson: BaseEntity
{
    public string GetFullName()
    {
        return $"{SurName} {FirstName} {LastName}";
    }

    public UserPerson( )
    {
          
    }


    public UserPerson(string surName, string firstName, string lastName, DateTime? birthday, string tel)
    {
        SurName = surName;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Tel = tel;
    }


    [InputOrder(1)]
    [Label("Фамилия")]
    [NotNullNotEmpty("Не указана фамилия пользователя")]
    [InputRusText("Записывайте фамилию кирилицей")]
    [Icon("person")]
    public string SurName { get; set; } = "Батов";

    [InputOrder(2)]
    [Label("Имя")]
    [NotNullNotEmpty("Не указано имя пользователя")]
    [InputRusText("Записывайте имя кирилицей")]
    [Icon("person")]
    public string FirstName { get; set; } = "Константин";

    [InputOrder(3)]
    [Label("Отчество")]
    [NotNullNotEmpty("Не указано отчество пользователя")]
    [InputRusText("Записывайте отчество кирилицей")]
    [Icon("person")]
    public string LastName { get; set; } = "Александрович";

    [InputOrder(4)]
    [Label("Дата рождения")]
    [InputDate()]
    [NotNullNotEmpty("Не указана дата рождения пользователя")]
    [Icon("person")]
    public DateTime? Birthday { get; set; } = DateTime.Parse("26.08.1989");

    [InputOrder(5)]
    [InputPhone("Номер телефона указан неверно")]
    [UniqValue("Этот номер телефона уже зарегистрирован")]
    [Label("Номер телефона")]
    [NotNullNotEmpty("Не указана номер телефона")]
    [Icon("phone")]
    public string Tel { get; set; } = "7-904-334-1124";

    [InputOrder(6)]
    [Label("Файл")]
    [InputImage()]
    [Icon("add_a_photo")]
    public byte[] Data { get; set; } = new byte[0];

}

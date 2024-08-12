using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


[Label("Сообщения в группе")]
public class UserGroupMessage: BaseEntity
{
    
    public int GroupID { get; set; }
    public virtual UserGroup Group { get; set; }


    [Label("Источник")]
    [NotNullNotEmpty("Свойство " + nameof(FromUserID) + " дожно иметь действительное значение" )]
    [NotInput("Свойство " + nameof(FromUserID) + " не вводится пользователем, оно устанавливается системой перед созданием сообщения на эл. почту пользорвателя с инструкциями по активации")]
    [NotMapped]
    public int FromUserID { get; set; }

    [Label("Источник")]        
    [NotInput("Свойство " + nameof(FromUser) + " не вводится пользователем, оно устанавливается системой перед созданием сообщения на эл. почту пользорвателя с инструкциями по активации")]
    [JsonIgnore()]
    [NotMapped]
    public virtual UserContext  FromUser { get; set; }

   


    [Label("Создано")]
    [InputHidden()]
    public DateTime Created { get; set; } = DateTime.Now;


    [Label("Тема")]
    [NotNullNotEmpty("Необходимо указать тему сообщения")]
    public virtual string Subject { get; set; }


    [Label("Текст сообщения")]
    [InputMultilineText( )]
    [NotNullNotEmpty("Необходимо ввести текст сообщения")]
    public virtual string Text { get; set; }
}


using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
public class UserMessageFile: BaseEntity
{
    public int UserMessageId { get;set; }
    public UserMessage UserMessage { get;set; }
    public string ContentType { get;set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }


}

[Label("Сообщение")]
[Icon("drafts")]
[SearchTerms(nameof(Subject) + ","+ nameof(Text))]
public class UserMessage : BaseEntity
{
    public UserMessage(): base()
    {           
    }

    [Label("Источник")]
    //[NotNullNotEmpty("Свойство " + nameof(FromUserID) + " дожно иметь действительное значение" )]
    //[NotInput("Свойство " + nameof(FromUserID) + " не вводится пользователем, оно устанавливается системой перед созданием сообщения на эл. почту пользорвателя с инструкциями по активации")]    
    public int? FromUserID { get; set; }

    [Label("Источник")]        
    [NotInput("Свойство " + nameof(FromUser) + " не вводится пользователем, оно устанавливается системой перед созданием сообщения на эл. почту пользорвателя с инструкциями по активации")]
    [JsonIgnore()]
    [NotMapped]
    public virtual UserContext  FromUser { get; set; }

    [Label("Назначение")]
    [InputDictionary(nameof(UserContext ) + ",GetFullName()")]   
    public int? ToUserID { get; set; }

        
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

    public bool Readed { get; set; } = false;

 
}
 

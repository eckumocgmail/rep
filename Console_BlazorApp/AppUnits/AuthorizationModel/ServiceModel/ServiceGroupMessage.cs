using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


[Label("Сообщения в группе")]
public class ServiceGroupMessage: BaseEntity
{
    
    public int ServiceGroupId { get; set; }

    [JsonIgnore]
    [NotMapped]
    public ServiceGroup ServiceGroup { get; set; }


    [Label("Создано")]
    [InputHidden()]
    public DateTime Created { get; set; } = DateTime.Now;

    [Label("Текст сообщения")]
    [InputMultilineText()]
    [NotNullNotEmpty("Необходимо ввести текст сообщения")]
    public virtual string Text { get; set; } = $"Текст сообщения";
}
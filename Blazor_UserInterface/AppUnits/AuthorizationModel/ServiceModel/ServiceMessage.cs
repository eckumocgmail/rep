using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

[Label("Сообщение об глобальных изменени")]
public class ServiceMessage: BaseEntity
{

    [Label("Заголовок")]
    [NotNullNotEmpty("Необходимо указать заголовок сообщения")]
    public string Title { get; set; } = "Заголовок сообщения";


    [Label("Изображение")]
    public virtual byte[] Image { get; set; }


    [Label("URL")]
    [InputUrl("Значение не является URL адресом ресурса")]
    public string Href { get; set; } = "https://localhost";


    public int ServiceContextId { get; set; }

 
  
}

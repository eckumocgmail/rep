using Microsoft.EntityFrameworkCore;

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
    public virtual byte[] Image { get; set; } = new byte[0];


    [Label("URL")]
    [InputUrl("Значение не является URL адресом ресурса")]
    public string Href { get; set; } = "https://localhost";

    [NotInput]
    private int _ServiceContextId;

    [InputDictionary($"{nameof(ServiceInfo)},{nameof(ServiceInfo.Name)}")]
    public int ServiceContextId 
    { 
        get
        {
            return _ServiceContextId;
        }
        set
        {
            using (DbContextService db = new())
            {
                var ctx = db.ServiceContexts.Include(ctx => ctx.ServiceInfo).FirstOrDefault(ctx => ctx.ServiceInfo.Id == value);
                _ServiceContextId = ctx is null? -1: ctx.Id;
            }                
        }
    }
  



}

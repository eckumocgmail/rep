using Microsoft.AspNetCore.Identity;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


public interface IActiveObject
{

    bool IsActive { get; set; }
    long LastActive { get; set; }
    DateTime LastActiveTime { get; set; }
    string SecretKey { get; set; }
    string URL { get; set; }
    string Ip4 { get; set; }
    string UserAgent { get; set; }
    


    Task DoCheck(object context, string key);



}


/// <summary>
/// Активные объекты.
/// Проходят процедуру авторизации в приложении.
/// Система будет отслеживать их состояние.
/// </summary>
public class ActiveObject : NamedObject, IActiveObject
{



    [DisplayName("Последнее посещение")]
    public long LastActive { get; set; }
    public DateTime LastActiveTime { get; set; }
    public string UserAgent { get; set; }


    [DisplayName("Онлайн")]
    public bool IsActive { get; set; } = false;


    [DisplayName("Секретный ключ")]
    public string SecretKey { get; set; } = "";

    [InputUrl("Значение не похоже на URL")]
    public string URL { get; set; } = "";



    public async Task DoCheck(object context, string key)
    {
        this.Info($"DoCheck({context},{key})");
        await Task.CompletedTask;
    }

    public string Ip4 { get; set; }
} 
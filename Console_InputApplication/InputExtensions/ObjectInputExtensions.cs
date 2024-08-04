using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class ObjectInputExtensions
{
    public static T GetRandom<T>(this object p) where T: class => null;
    /// <summary>
    /// Выбор элемента из предложенных
    /// </summary>   
    public static string NavMenu(this IDictionary<string, string> enumerable, string title, ref string[] args)
    {
        if (enumerable.Count() == 0)
            throw new Exception("Функция именования объектов возвращает не уникальные значения. ");        
        return enumerable[enumerable.Keys.SingleSelect(title, ref args)];
    }

}
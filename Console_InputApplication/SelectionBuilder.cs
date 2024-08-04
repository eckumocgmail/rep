using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


using static InputConsole;


/// <summary>
/// Используется для формирования выбора из исключающего списка.
/// Т.е. уже выбранные элементы в предыдущий раз не будут отображаться
/// </summary>
public class SelectionBuilder<TOption>: InputConsole
{
    //выбранные элементы
    public IList<TOption> Selected { get; set; } = new List<TOption>();

    //все возможные элементы
    private IList<TOption> Options = new List<TOption>();

    [Label("Функция предоставляет текст для отображения данных об элементе")]
    private Func<TOption, string> Display;

    public SelectionBuilder(  IEnumerable<TOption> Options, Func<TOption, string> Display )
    {
        this.Options = Options!=null? Options.ToList(): new List<TOption>();
        this.Display = Display;
    }
    string[] args = new string[] { };
    
    [Label("Добавить")]
    public void AddItem()
    {        
        var ForSelect = this.Options.ToHashSet().Except(this.Selected).ToList();
        var Item = SingleSelect<TOption>(ForSelect, this.Display, ref this.args);
        Selected.Add(Item);
    }

    [Label("Удалить")]
    public void RemoveItem()            
        => Selected.Remove(
            InputConsole.SingleSelect<TOption>(this.Selected, this.Display, ref this.args));
    






}



public static class SelectOptionsBuilderExtension
{
    public static void Interactive<T>(this IList<T> items, IEnumerable<T> Available)
    {
        var builder = new SelectionBuilder<T>(Available, item => item.ToString())
        {
            Selected = items
        };

    }

}

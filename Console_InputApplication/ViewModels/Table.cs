using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/**
 * Таблица на коллекцию таблицу базу данных
 */
public class Table : ViewItem
{
    public string Title { get; set; }
    public Type type { get; set; }
    public List<string> Columns { get; set; } = new List<string>();
    public List<List<object>> Cells { get; set; } = new List<List<object>>();

    public bool IsPrimitive { get; set; } = false;


    public Table() : base()
    {
        Init();
        ClassList.Add("front-pane");
        type = typeof(Table);
    
        /*
        Columns = (from p in type.GetProperties() select p.Name).ToList();
        foreach (var item in new CRUDS(new ApplicationDbContext()).List("Person"))
        {
            Cells.Add(new List<object>(ReflectionService.Values(item, Columns)));
        }*/
      

    }


    public object CreateSearch()
    {
        return null;

    }


}
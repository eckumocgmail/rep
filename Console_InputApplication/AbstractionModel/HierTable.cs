using System.ComponentModel;
using System.Text.Json.Serialization;

[Label("Иерархический справочник")]
public class HierTable<T>: DictionaryTable
{

    [Label("Корневой каталог")]
    [DisplayName("Корневой каталог")]
    public int? ParentId { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public virtual T Parent { get; set; }

    public virtual string GetPath(string separator)
    {
        HierTable<T> parentHier = ((HierTable<T>)((object)Parent));
        return (Parent != null) ? parentHier.GetPath(separator) + separator + Name : Name;
    }
    
}

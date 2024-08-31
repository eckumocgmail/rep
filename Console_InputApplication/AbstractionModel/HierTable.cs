using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

 
public class HierTable<T>: DictionaryTable
{

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
 
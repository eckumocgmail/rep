using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

public class Card : ViewItem
{
    
    [JsonIgnore()]
    public IEnumerable<INavigation> Navigation { get; set; }
    public object Item { get; set; }
    public string ActivePropertyName { get; set; }


    

    public bool IsCollection { get; set; } = false;
    public object Active 
    { 
        get
        {
            if(ActivePropertyName == null)
            {
                ActivePropertyName = Navigation.First().Name;
            }
            return (Item==null || ActivePropertyName==null)? null:
                    new ReflectionService().GetValue(Item, ActivePropertyName );
        }
    }

    public bool SetActive(string Name)
    {
        ActivePropertyName = Name;
        Changed = true;
        IsCollection = Attrs.IsCollection(Item.GetType(), Name);        
        return true;
    }
      


    public Card() : this(new ViewItem())
    {
        Changed = false;
    }


    public Card(object item)
    {
        
        Item = item;
        Navigation = Attrs.GetNavigation(Item.GetType());
        ActivePropertyName = Navigation.First().Name;
        Changed = false;
    }

}


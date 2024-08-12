using System.Collections.Generic;

public class TableDictionary: Table
{
    public Dictionary<string,object> Model { get; set; }

    public TableDictionary(){        
    }

    public void InitTable(){        
        this.Columns = new List<string> { "Ключ","Значение"};
        this.Cells = new List<List<object>>();
        foreach (var p in Model)
        {
            this.Cells.Add(new List<object> { p.Key, p.Value });
        }
        this.Changed = true;
    }




}
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

[Label("Много ко многим")]
public class UserGroups: BaseEntity 
{        

    [JsonIgnore()]
    public virtual UserContext  User { get; set; }
    public int UserID { get; set; }

    [JsonIgnore()]
    public virtual UserGroup Group { get; set; }
    public int GroupID { get; set; }
}

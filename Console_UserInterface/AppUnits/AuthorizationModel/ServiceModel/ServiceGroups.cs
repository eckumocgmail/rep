
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


[Label("Много ко многим")]
public class ServiceGroups : BaseEntity 
{

    [JsonIgnore()]
    public virtual ServiceContext ServiceContext { get; set; }
    public int ServiceContextId { get; set; }



    [JsonIgnore()]
    public virtual ServiceGroup ServiceGroup { get; set; }
    public int ServiceGroupId { get; set; }

}


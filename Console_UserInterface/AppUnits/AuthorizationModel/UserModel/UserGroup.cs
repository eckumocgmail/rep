




using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Label("Группа пользователей")]
public class UserGroup: DictionaryTable 
{


    /* [Label("Бизнес-процесс")]
    [InputDictionary(nameof(BusinessProcess)+",Name")]
    public int? BusinessProcessId { get; set; }

    [Label("Бизнес-процесс")]
    public virtual BusinessProcess BusinessProcess { get; set; }*/


    [Label("Уникальный код")]
    [UniqValue]
    [InputEngWord]
    [StringLength(25)]
    [NotNullNotEmpty()]    
    public string Code { get; set; }

    [NotMapped]
    [NotInput]
    [JsonIgnore()]
    public virtual List<UserGroupMessage> UserGroupMessages { get; set; }

    [NotMapped]
    [NotInput]
    [JsonIgnore()]
    public virtual List<UserGroups> UserGroups { get; set; }

    [NotMapped]
    [NotInput]
    [JsonIgnore()]
    public virtual List<UserPerson> People { get; set; }


    [InputImage()]
    [Label("Аватар")]
    [AllowNull]
    public string Avatar { get; set; } = "data:image/json";

    /*[NotMapped]
    [JsonIgnore()]
    public virtual List<MessageProtocol> MessageProtocols { get; set; }

    [NotMapped]
    [JsonIgnore()]
    public virtual List<BusinessFunction> BusinessFunctions { get; set; }

    
    [JsonIgnore()]
    [ManyToMany("BusinessFunctions")]
    public virtual List<global::GroupsBusinessFunctions> GroupsBusinessFunctions { get; set; }*/

}

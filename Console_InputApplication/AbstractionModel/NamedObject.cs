using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

[Label("Именнованный объект")]
[Description("Содержит уникальное значение Name")]
public class NamedObject: BaseEntity,  INamedObject  
{
    [Label("Наименование")]
    [NotNullNotEmpty(ErrorMessage = "Необходимо указать наменование")]
    public virtual string Name { get; set; } = "наименование";

    [Label("Описание")]
    [NotNullNotEmpty(ErrorMessage = "Необходимо указать наменование")]
    public virtual string Description { get; set; } = "Описание";


}

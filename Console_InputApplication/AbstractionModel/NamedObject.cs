﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
 
public interface INamedObject
{
    [DisplayName("Наименование")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public string Name { get; set; }// = "наименование";

    [DisplayName("Описание")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public string Description { get; set; }// = "Описание";


}

public class NamedObject: BaseEntity,  INamedObject  
{
    [DisplayName("Наименование")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public virtual string Name { get; set; } = "наименование";

    [DisplayName("Описание")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public virtual string Description { get; set; } = "Описание";


}

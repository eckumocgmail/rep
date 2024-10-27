﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 
public class BusinessProcess: BusinessEntity<BusinessProcess>
{

    [InputDictionary("MessageProtocol" + ",Name")]
    [NotMapped]
    public virtual int InputID { get; set; }
    [NotMapped]
    public virtual MessageProtocol Input { get; set; }

    [NotMapped]
    [InputDictionary(nameof(MessageProtocol)+",Name")]
    public virtual int OutputID { get; set; }
    [NotMapped]
    public virtual MessageProtocol Output { get; set; }

    public virtual List<BusinessFunction> BusinessFunctions { get; set; }

      
}
 
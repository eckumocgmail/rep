
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Label("Функции рабочей группы")]
public class GroupsBusinessFunctions : BaseEntity
{
    public int GroupID { get; set; }
    //public virtual UserGroup Group { get; set; }
    public int BusinessFunctionID { get; set; }
    public virtual BusinessFunction BusinessFunction { get; set; }

}
 

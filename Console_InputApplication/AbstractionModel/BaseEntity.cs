using System.Collections;
using System.ComponentModel.DataAnnotations;

[Label("Общий класс для всех сущностей")]
public abstract class BaseEntity
{
    [Key]
    [InputHidden()]
    public virtual int Id { get; set; }
     

} 



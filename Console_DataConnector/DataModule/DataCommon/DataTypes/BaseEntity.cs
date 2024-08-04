using Microsoft.AspNetCore.Components.Web.Virtualization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ValidationAnnotationsNS { }
public class IObjectWithId: MyValidatableObject
{
    [Key]
    public virtual int ID { get; set; } = 0;

    [InputText]
    [NotMapped]
    public virtual string Name { get; set; } = "";

    public bool Is<TType>() => GetType().IsExtendsFrom(typeof(TType).Name);
    public TType Cast<TType>() =>
        Is<TType>() ? ((TType)this.MemberwiseClone()) :
        throw new System.Exception($"Объект типа {GetType().Name} не возможно привети к типу {typeof(TType).Name}");
    public TType Convert<TType>() =>
        Is<TType>() ? ((TType)this.MemberwiseClone()) :
        this.ToJson().FromJson<TType>();
}
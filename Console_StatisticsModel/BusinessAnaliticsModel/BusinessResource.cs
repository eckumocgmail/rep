





using Microsoft.AspNetCore.Identity;

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

[Label("Ресурсы предприятия")]
[SearchTerms("Name")]
public class BusinessResource : BaseEntity, IActiveObject, INamedObject
{

    [Key]
    [InputHidden()]
    public override int Id { get; set; }


    [Label("Корневой каталог")]
    [InputDictionary("GetType().Name,Name")]
    public int? ParentID { get; set; }


    [InputHidden(true)]
    public virtual BusinessResource Parent { get; set; }

   

    [NotNullNotEmpty()]
    [UniqValue( )]
    public string Code { get; set; }

    public bool IsActive { get; set; }
    public long LastActive { get; set; }
    public DateTime LastActiveTime { get; set; }
    public string SecretKey { get; set; } = "****";
    public string URL { get; set; } = "/home";

    public async Task DoCheck(object context, string key)
    {
        await Task.CompletedTask;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    //public string Ip4 { get => ((IActiveObject)Parent).Ip4; set => ((IActiveObject)Parent).Ip4 = value; }
    //public string UserAgent { get => ((IActiveObject)Parent).UserAgent; set => ((IActiveObject)Parent).UserAgent = value; }


    /*
    public string GetPath(string separator)
    {
        BusinessResource parentHier =  Parent ;
        return (Parent != null) ? parentHier.GetPath(separator) + separator + Name : Name;
    }

    public override Tree ToTree()
    {
        throw new System.NotImplementedException();
    }*/
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
public class CustomAttribute
{
    public int Id { get; set; }

    /// <summary>
    /// Может быть:
    /// 1)type
    /// 2)type.property
    /// 3)type.method
    /// 4)type.method.param
    /// </summary>
    public string Qualificator { get; set; }

    /// <summary>
    /// Тип атрибута
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Тип атрибута
    /// </summary>
    public string Value { get; set; }

}

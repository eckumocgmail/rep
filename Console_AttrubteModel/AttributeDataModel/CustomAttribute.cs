using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
/// <summary>
/// Модель данных аттрибута типа
/// </summary>
public class CustomAttribute
{
    public int Id { get; set; }

    /// <summary>
    /// Строка содержащая полный идентификатор объекта применения
    /// 1)type
    /// 2)type.property
    /// 3)type.method
    /// 4)type.method.param
    /// </summary>
    public string Qualificator { get; set; }

    /// <summary>
    /// Наименование типа атрибута
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Значение аттрибута
    /// </summary>
    public string Value { get; set; }

}

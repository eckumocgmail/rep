using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
/// <summary>
/// Таблица срезов данных( определяет множествоаналитических моделей )
/// </summary>
public class DimensionTable: NamedObject
{

    public IEnumerable<string> GetGroups()
    {
        throw new NotImplementedException();
    }
    public IDictionary<string,int> GetStatistics()
    {
        throw new NotImplementedException();
    }
}

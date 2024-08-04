using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public static class DbContextExtensions
{

    /// <summary>
    /// Получение типа в котором реализован DbSet для вызывающей стороны
    /// </summary>
    
    public static Type GetDbContextWithEntity(this object target)
    {
        foreach(var pdbtype in FactoryUtils.Get().GetTypesExtended<DbContext>())
        {
            using (var pdb = (DbContext)pdbtype.New())
            {
                var pentity = pdb.GetEntitiesTypes().FirstOrDefault(e => e.GetTypeName() == target.GetType().GetTypeName());
                if (pentity == null)
                    throw new Exception("не найден контекст данных для " + target.GetType().GetTypeName());
                else return pdbtype;
            }            
        }
        throw new Exception("не найден контекст данных для " + target.GetType().GetTypeName());
    }

    /// <summary>
    /// Нехороший способ извеления наименований сущностей
    /// </summary>
    /// <param name="dbContext"> контекст данных </param>
    /// <returns> множество наименований сущностей </returns>
    public static HashSet<Type> GetEntitiesTypes(this object dbContext)
    {
        Type type = dbContext.GetType();
        HashSet<Type> entities = new HashSet<Type>();
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.Name.StartsWith("get_") == true && info.ReturnType.Name.IndexOf("DbSet")!=-1)
            {
                if (info.Name.IndexOf("MigrationHistory") == -1)
                {
                    entities.Add(info.ReturnType);
                }
            }
        }
        return entities;
    }
    public static dynamic GetDbSet(this DbContext dbContext, string entity)
    {
        Type type = dbContext.GetType();
        HashSet<Type> entities = new HashSet<Type>();
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.Name.StartsWith("get_") == true && info.ReturnType.Name.StartsWith("ISuperSer"))
            {
                if (info.Name.IndexOf("MigrationHistory") == -1)
                {
                    return info.Invoke(dbContext, new object[0]);
                }
            }
        }
        return entities;
    }
}
    

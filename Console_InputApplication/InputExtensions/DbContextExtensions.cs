using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        if (target is Type)
            throw new ArgumentException("Метод GetDbContextWithEntity нужно использовать на ссылки на объект" );
        foreach(var pdbtype in ServiceFactory.Get().GetTypesExtended<DbContext>())
        {
            using (var pdb = (DbContext)pdbtype.New())
            {
                pdb.GetEntitiesTypes().Select(t => t.GetTypeName()).ToJsonOnScreen().WriteToConsole();

                var pentity = pdb.GetEntitiesTypes().FirstOrDefault(e => e.GetTypeName() == target.GetType().GetTypeName());
                if (pentity != null)
                    return pdbtype;
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
        if(dbContext is null)
            throw new ArgumentNullException(nameof(dbContext));
        Type type = dbContext.GetType();
        HashSet<Type> entities = new HashSet<Type>();
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.Name.StartsWith("get_") == true && info.ReturnType.Name.IndexOf("DbSet")!=-1)
            {

                
                if (info.Name.IndexOf("MigrationHistory") == -1)
                {
                    string typeName = info.ReturnType.GenericTypeArguments[0].GetTypeName();
                    var ptype = typeName.ToType();
                    entities.Add(ptype);
                }
            }
        }
        return entities;
    }
    public static IEnumerable<INavigation> GetNavigationPropertiesForType(this DbContext dbContext, Type type)
    {
        foreach(var nav in dbContext.Entry(type).Navigations)
        {
            
        }
        return new List<INavigation>();
    }


    public static dynamic GetDbSet(this DbContext dbContext, string entity)
    {
        Type type = dbContext.GetType();
        HashSet<Type> entities = new HashSet<Type>();
        foreach (MethodInfo info in type.GetMethods())
        {
            var rs = new ReflectionService();
            var all = dbContext.GetType().GetProperties().Where(prop => prop.PropertyType.IsExtendsFrom(typeof(DbSet<>))).Select(prop => prop.PropertyType.GetGenericArguments()[0].GetTypeName()).ToList();
            var dbset = dbContext.GetType().GetProperties().Where(prop => prop.PropertyType.IsExtendsFrom(typeof(DbSet<>))).FirstOrDefault(prop => prop.PropertyType.GetGenericArguments()[0].GetTypeName() == entity).GetValue(dbContext);

            if (dbset is null)
            {
                throw new ArgumentException("entity", $"Не удалось найти массив данных для {entity}");
            }
            else
            {
                return dbset;
            }
        }
        throw new ArgumentException("entity", $"Не удалось найти массив данных для {entity}");
    }
}
    

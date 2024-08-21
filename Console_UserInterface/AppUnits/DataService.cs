using AngleSharp.Dom;

using Microsoft.EntityFrameworkCore;


public class DataService
{
    public static DbContext GetDbContext(Type ptype)
    {
        foreach (Type dbtype in typeof(DataService).Assembly.GetTypes<DbContext>())
        {
            DbContext dbc = (DbContext)dbtype.New();
            List<Type> types = dbc.GetEntitiesTypes().Select(p => p.GenericTypeArguments.Count() > 0 ? p.GenericTypeArguments[0] : p).ToList();
            if (types.Contains(ptype))
            {
                return dbc;
            }           
        }
        throw new Exception("Не удалось найти набор данных " + ptype.GetTypeName());
    }

    public static DbSet<TEntity> GetDbSet<TEntity>(Type ptype) where TEntity : class
    {
        foreach (Type dbtype in typeof(DataService).Assembly.GetTypes<DbContext>())
        {
            using (DbContext dbc = (DbContext)dbtype.New())
            {
                List<Type> types = dbc.GetEntitiesTypes().Select(p => p.GenericTypeArguments.Count() > 0 ? p.GenericTypeArguments[0] : p).ToList();
                if (types.Contains(typeof(TEntity)))
                {
                    var pdbset = dbc.GetType().GetProperties().First(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(typeof(TEntity)));
                    var pdbsetref = pdbset.GetValue(dbc);
                    return (DbSet<TEntity>)pdbsetref;
                }
            }
        }
        throw new Exception("Не удалось найти набор данных "+typeof(TEntity).GetTypeName());
    }
}

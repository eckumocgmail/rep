using System.Collections.Generic;

public interface ISuperSer<TEntity> : ISet<TEntity>, System.Linq.IQueryable<TEntity> where TEntity : IObjectWithId
{
    void Update<TEntity>(TEntity data) where TEntity : IObjectWithId;
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;

public class EntityFasade<T> : IEntityFasade<T> where T : BaseEntity
{
    private readonly IEntityFasade _service;

    public EntityFasade(IEntityFasade service)
    {
        _service = service;
    }

    public int Create(params T[] items)
    {
        var res = 0;
        foreach(var item in items)
        {
            item.EnsureIsValide();
            res += _service.Create(item).Result;
        }
        return res;
    }

    public IEnumerable<T> Get(params int[] ids)
    {
        return ids.Select(id => _service.Find<T>(id).Result);
    }

    public IEnumerable<T> GetAll()
    {
        return _service.GetAll().Result.Select(item => item.ToJsonOnScreen().FromJson<T>());
    }

    public IEnumerable<string> GetOptions(string searchQuery)
    {
        return _service.GetAll().Result.SelectMany(item => item.ToJsonOnScreen().ToLower().SplitWords()).ToHashSet();
    }

    public IEnumerable<T> GetPage(int page, int size)
    {
        return _service.Page(page, size).Result.Select(item => (T)item);    
    }

    public IEnumerable<T> GetPage(int[] ids, int page, int size)
    {
        throw new System.NotImplementedException();
    }

    public int Remove(params int[] ids)
    {
        int res = 0;
        foreach(var id in ids)
        {
            res += _service.Delete(id).Result;
        }
        return res;
    }

    public IEnumerable<T> Search(string query)
    {
        throw new System.NotImplementedException();
    }

    public int TotalPages(string searchQuery, int v)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<object> SearchColumnsInPropertiesPage(string query, string[] properties, string[] columns, int page, int size)
    {
        throw new System.NotImplementedException();
    }

    public int TotalResults(string searchQuery)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<object> SearchInProperties(string query, string[] properties)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<object> SearchInPropertiesPage(string query, string[] properties, int page, int size)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<T> SearchPage(string query, int page, int size)
    {
        throw new System.NotImplementedException();
    }

    public int Update(params T[] items)
    {
        int res = 0;
        foreach (var item in items)
            res += _service.Update(item).Result;
        return res;
    }

    public int Count()
    {
        return _service.Count();
    }
}
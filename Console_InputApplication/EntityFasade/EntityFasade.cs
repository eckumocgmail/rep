using Microsoft.EntityFrameworkCore;
using static System.Console;
using static Newtonsoft.Json.JsonConvert;

using System.Collections.Generic;
using System;
using System.Linq;


public interface IEntitySearch<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetSearchResults(string query);
    IEnumerable<TEntity> GetSearchResultsByPage(string query, int page, int size);
    IEnumerable<object> SearchInProperties(string query, string[] properties);
    IEnumerable<object> SearchInPropertiesByPage(string query, string[] properties, int page, int size);
    IEnumerable<object> SearchInPropertiesAndSelecColumns(string query, string[] properties, string[] columns, int page, int size);

}


public interface IEntityFasade<TEntity> where TEntity : class
{
    int Create(params TEntity[] items);
    IEnumerable<TEntity> Get(params int[] ids);
    IEnumerable<TEntity> GetAll();
    IEnumerable<string> GetOptions(string searchQuery);
    IEnumerable<TEntity> GetPage(int page, int size);
    IEnumerable<TEntity> GetPage(int[] ids, int page, int size);
    int Remove(params int[] ids);
    IEnumerable<TEntity> Search(string query);
    int TotalPages(string searchQuery, int v);
    IEnumerable<object> SearchColumnsInPropertiesPage(string query, string[] properties, string[] columns, int page, int size);
    int TotalResults(string searchQuery);
    IEnumerable<object> SearchInProperties(string query, string[] properties);
    IEnumerable<object> SearchInPropertiesPage(string query, string[] properties, int page, int size);
    IEnumerable<TEntity> SearchPage(string query, int page, int size);
    int Update(params TEntity[] items);
    int Count();
}







public class DbEntityFasade<TEntity> : IEntityFasade<TEntity> where TEntity : class
{

    private DbContext _context;
    private DbSet<TEntity> GetDbSet() => _context.Set<TEntity>();

    public DbEntityFasade(DbContext context)
    {
        _context = context;
    }

    public int Create(params TEntity[] items)
    {
        foreach (var item in items)
        {
            _context.Add(item);
        }
        return _context.SaveChanges();
    }

    public IEnumerable<TEntity> Get(params int[] ids)
        => GetDbSet().Where(item => ids.Contains(int.Parse(item.GetType().GetProperty("Id").ToString())));

    public IEnumerable<TEntity> GetPage(int[] ids, int page, int size)
        => GetDbSet().Where(item => ids.Contains(int.Parse(item.GetType().GetProperty("Id").ToString()))).Skip((page - 1) * size).Take(size);

    public IEnumerable<TEntity> GetAll() => GetDbSet();

    public IEnumerable<TEntity> GetPage(int page, int size) => GetDbSet().Skip((page - 1) * size).Take(size);

    public int Remove(params int[] ids)
    {
        Get(ids).ToList().ForEach(item => _context.Remove(item));
        return _context.SaveChanges();
    }

    public int Update(params TEntity[] items)
    {
        foreach (var item in items)
            _context.Update(item);
        return _context.SaveChanges();
    }



    //TODO: нужно реализовать разбаение по словам
    public IEnumerable<TEntity> Search(string query) 
    {

        List<TEntity> result = new List<TEntity>();
        try
        {
            var set = GetDbSet();
            set.ToList().ForEach(item =>
            {
                if (SerializeObject(item).ToLower().IndexOf(query.ToLower())!=-1)

                    result.Add(item);

            });
        }
        catch (Exception ex)
        {

        }

        return result;
    }


    public IEnumerable<TEntity> SearchPage(string query, int page, int size)
    {
        List<TEntity> result = new List<TEntity>();

        GetDbSet().ToList().ForEach(item =>
        {
            if (string.IsNullOrEmpty(query) || SerializeObject(item).Contains(query))
            {
                result.Add(item);
            }
        });

        WriteLine(SerializeObject(result.Skip((page - 1) * size).Take(size).ToArray()));
        return result.Skip((page - 1) * size).Take(size).ToArray();
    }

    public IEnumerable<object> SearchColumnsInPropertiesPage(string query, string[] properties, string[] columns, int page, int size)
        => SearchInPropertiesPage(query, properties, page, size).Select(next => SelectProperties(next, columns));

    private Dictionary<string, object> SelectProperties(object next, string[] columns)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<object> SearchInPropertiesPage(string query, string[] properties, int page, int size)
        => GetDbSet().Select(item => ToDictionary(item).Where(kv => properties.Contains(kv.Key))).Skip((page - 1) * size).Take(size);

    public IEnumerable<object> SearchInProperties(string query, string[] properties)
        => GetDbSet().Select(item => ToDictionary(item)).Where(dic => SerializeObject(dic.Values).ToLower().Contains(query.ToLower()));

    private Dictionary<string,object> ToDictionary(TEntity item)
    {
        throw new NotImplementedException();
    }

    public int TotalPages(string query, int size)
    {
        List<TEntity> result = new List<TEntity>();
        GetDbSet().ToList().ForEach(item =>
        {
            if (string.IsNullOrEmpty(query) || SerializeObject(item).Contains(query))
            {
                result.Add(item);
            }
        });
        return 1 + (int)Math.Floor((decimal)result.Count() / size);
    }

    public int TotalResults(string query)
    {
        List<TEntity> result = new List<TEntity>();
        GetDbSet().ToList().ForEach(item =>
        {
            if (string.IsNullOrEmpty(query) || SerializeObject(item).Contains(query))
            {
                result.Add(item);
            }
        });
        return result.Count();
    }

    public IEnumerable<string> GetOptions(string query)
    {
        HashSet<string> result = new HashSet<string>();
        GetDbSet().ToList().ForEach(item =>
        {
            GetKeywords(item).ToList().ForEach(i => result.Add(i.ToLower()));
        });
        return result;
    }

    private IEnumerable<string> GetKeywords(TEntity item)
    {

        return ParseKeywords(SerializeObject(item)).Keys;
    }

    private Dictionary<string,int> ParseKeywords(string v)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        return GetDbSet().Count();
    }
}
public static class StringExtensinos
{
    public static bool ContainsAnyWord(this string text, string query)
    {
        if (query == null || string.IsNullOrWhiteSpace(query))
            return true;
        foreach (var word in SplitWords(text.ToLower()).Where(w => string.IsNullOrWhiteSpace(w) == false))
        {
            foreach (var q in SplitWords(query.ToLower()))
            {
                if (word == q)
                    return true;
            }
        }
        return false;


    }

    private static IEnumerable<string> SplitWords(string v)
    {
        throw new NotImplementedException();
    }
}
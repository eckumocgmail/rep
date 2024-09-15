using static System.Console;
using static Newtonsoft.Json.JsonConvert;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Ex;
using Console_BlazorApp.AppUnits.DeliveryApi;

public class EfEntityFasade<TEntity> : IEfEntityFasade<TEntity> where TEntity : BaseEntity
{

    protected DbContext _context;
    protected virtual IQueryable<TEntity> GetDbSet() => _context.Set<TEntity>();

    public EfEntityFasade(DbContext context)
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
        => GetDbSet().Where(item => ids.Contains(item.Id));

    public IEnumerable<TEntity> GetPage(int[] ids, int page, int size)
        => GetDbSet().Where(item => ids.Contains(item.Id)).Skip((page - 1) * size).Take(size);

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



    //TODO: нужно реализовать разбиение по словам
    public IEnumerable<TEntity> Search(string query) 
    {
        List<TEntity> results = new();
        if (String.IsNullOrWhiteSpace(query))
            return GetDbSet().ToList();
        foreach(var item in GetDbSet())
        {
            string text = item.ToDictionary().Values.ToList().ToJson();
            if ( text.ToLower().IndexOf(query.ToLower()) != -1)
            {
                results.Add(item);
            }
        }
        return results;
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
        => SearchInPropertiesPage(query, properties, page, size).Select(next => next.SelectProperties(columns));





    public IEnumerable<object> SearchInPropertiesPage(string query, string[] properties, int page, int size)
        => GetDbSet().Select(item => item.ToDictionary().Where(kv => properties.Contains(kv.Key))).Skip((page - 1) * size).Take(size);

    public IEnumerable<object> SearchInProperties(string query, string[] properties)
        => GetDbSet().Select(item => item.ToDictionary()).Where(dic => SerializeObject(dic.Values).ToLower().Contains(query.ToLower()));

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
            GetKeywords(item).ToList().ForEach(i =>
            {
                if(String.IsNullOrWhiteSpace(query) == false && i.ToLower().IndexOf(query.ToLower()) != -1)
                {
                    result.Add(i.ToLower());
                }
                
            });
        });
        return result;
    }

    private IEnumerable<string> GetKeywords(TEntity item)
    {

        try
        {
            return new StupidKeywordsParserService().ParseKeywords(SerializeObject(item)).Keys;
        }
        catch(Exception)
        {
            return new List<string>(); 
        }
    }
}

public interface IKeywordsParserService
{
    public IDictionary<string, int> ParseKeywords(string Resource);
}


public class StupidKeywordsParserService : IKeywordsParserService
{


    private IDictionary<string, int> keywords = new Dictionary<string, int>();

    public StupidKeywordsParserService()
    {
    }

    public IDictionary<string, int> ParseKeywords(string Resource)
    {

        var statisticsForThisRecord = new Dictionary<string, int>();

        foreach (string text in Resource.SplitWords())
        {
            string word = text.ToUpper();
            if (keywords.ContainsKey(word))
            {
                keywords[word]++;
            }
            else
            {
                keywords[word] = 1;
            }

            if (statisticsForThisRecord.ContainsKey(word))
            {
                statisticsForThisRecord[word]++;
            }
            else
            {
                statisticsForThisRecord[word] = 1;
            }
        }
        return statisticsForThisRecord;
    }
}
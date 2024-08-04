using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class EnumerableExtensions
{
    public static Dictionary<string, T> NewDictionary<T>(this IEnumerable<string> items, Func<string, T> map)
        => new Dictionary<string, T>(items.Select(item => new KeyValuePair<string, T> (item, map(item))));
  
}


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


namespace Ex
{
    public static class ObjectQueringExtrensions
    {
        public static string GetTypeName(this Type propertyType)
        {
            string name = propertyType.Name;
            if (name.Contains("`"))
            {
                string text = propertyType.AssemblyQualifiedName;
                text = text.Substring(text.IndexOf("[[") + 2);
                text = text.Substring(0, text.IndexOf(","));
                name = name.Substring(0, name.IndexOf("`")) + "<" + text + ">";
            }
            return name;
        }

        public static IDictionary<string, int> GetCountOf(this string text, params string[] terms)
        {
            var result = new Dictionary<string, int>();
            foreach (var term in terms)
            {
                int count = 0;
                int startIndex = -term.Length;
                var ltext = text.ToLower();
                int subIndex = ltext.Substring(startIndex + term.Length).IndexOf(term);
                while (startIndex < text.Length)
                {
                    if (subIndex != -1)
                    {
                        count++;
                        startIndex += subIndex;
                    }
                    else
                    {
                        break;
                    }

                }
                result[term] = count;
            }
            return result;
        }
        public static IDictionary<string, int> GetContentStatistics
            (this object target, params string[] words)
        {

            var stat = new Dictionary<string, int>();

            target.GetType().GetProperties().ToList().ForEach(p =>
                stat[p.Name] = p.GetValue(target) == null ? 0 :
                    p.GetValue(target).ToString().ToLower().GetCountOf(words).Values.Sum()
            );
            return stat;
        }
        public static IDictionary<string, object> ToDictionary(this object target)
        {
            var result = new Dictionary<string, object>();
            target.GetType().GetProperties().ToList().ForEach(p =>
                result[p.Name] = p.GetValue(target)
            );
            return result;
        }
        public static IDictionary<string, object> SelectProperties(this object target, params string[] properties)
        {
            var result = new Dictionary<string, object>();
            properties.ToList().ForEach(p =>
                result[p] = target.GetType().GetProperty(p).GetValue(target)
            );
            return result;
        }
    }
}
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class InputExtensions
{
    public static string SingleSelect(this IEnumerable<string> items, string title, ref string[] args)
        => InputConsole.SingleSelect(title, items, ref args);
    public static string SingleSelect(this IDictionary<string, string> items, string title, ref string[] args)
        => items[InputConsole.SingleSelect(title, items.Keys, ref args)];
    public static string UserSelectSingle(this IEnumerable<string> items, Func<string, string> display, ref string[] args)
        => InputConsole.SingleSelect(items, display, ref args);
    public static string UserSelectSingle(this IEnumerable<string> items, string title, Func<string, string> display, ref string[] args)
        => InputConsole.SingleSelect(items, display, ref args);

    public static async Task WriteHtml(this HttpContext http, string html)
    {
        http.Response.ContentType = "text/html; charset=utf-8";
        await http.Response.WriteAsync(html);
    }
}


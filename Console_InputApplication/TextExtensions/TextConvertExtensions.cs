using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Конвертирование данных
/// </summary>
public static class TextConvertExtensions
{
    public static string ReplaceAll(string text, string s1, string s2)
    {
        while (text.IndexOf(s1) != -1)
        {
            text = text.Replace(s1, s2);
        }
        return text;
    }

    public static string ToHtmlText(string text)
    {
        string result = "";
        foreach (string line in text.Split("\n"))
        {
            result += $"<div>{line}</div>";
        }
        return result;
    }

    public static string TransliteToLatine(string rus)
    {
        string latine = "";
        for (int i = 0; i < rus.Length; i++)
        {
            latine += map(rus[i]);
        }
        return latine;
    }

    private static string map(char v)
    {
        switch ((v + "").ToLower())
        {
            case "а": return "a";
            case "б": return "b";
            case "в": return "v";
            case "г": return "g";
            case "д": return "d";
            case "е": return "e";
            case "ё": return "yo";
            case "ж": return "j";
            case "з": return "z";
            case "и": return "i";
            case "й": return "yi";
            case "к": return "k";
            case "л": return "l";
            case "м": return "m";
            case "н": return "n";
            case "о": return "o";
            case "п": return "p";
            case "р": return "r";
            case "с": return "s";
            case "т": return "t";
            case "у": return "u";
            case "ф": return "f";
            case "х": return "h";
            case "ц": return "c";
            case "ш": return "sh";
            case "щ": return "sh";
            case "ъ": return "'";
            case "ы": return "u";
            case "ь": return "'";
            case "э": return "a";
            case "ю": return "y";
            case "я": return "ya";
            default: throw new Exception("Не удалось транслировать сообщение");
        }
    }
    public static string ToStringInline(this object target)
       => target.ToJson();
    public static int ToInt(this string text)
    {
        text.EnsureIsInt();
        return int.Parse(text);
    }
    public static bool ToBool(this string text)
    {
        text.EnsureIsBool();
        return text is not null && text.ToLower().Trim() == "true" ;
    }
    public static float ToFloat(this string text)
    {
        text.EnsureIsFloat();
        return float.Parse(text.Replace(".", ","));
    }
    public static DateTime? ToDate(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;
        text.EnsureIsDate();
        char? separator = text.FirstChar(@"-.\/:");
        string[] arr = text.Split((char)separator);
        if ((arr[0].Length == 4 && arr[1].Length == 2 && arr[2].Length == 2))
        {
            int year = int.Parse(arr[0]);
            int month = int.Parse(arr[1]);
            int day = int.Parse(arr[2]);

            return new DateTime(year, month, day);
        }
        else if ((arr[0].Length == 2 && arr[1].Length == 2 && arr[2].Length == 4))
        {
            int year = int.Parse(arr[2]);
            int month = int.Parse(arr[1]);
            int day = int.Parse(arr[0]);
            return new DateTime(year, month, day);
        }
        throw new Exception("Не удалось преобразовать текст в дату");
    }
}
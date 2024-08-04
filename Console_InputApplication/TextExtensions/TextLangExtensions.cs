using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// Определение языковых признаков
/// </summary>
public static class TextLangExtensions
{
    private static readonly string ENG_CHARS = "qwertyuiopasdfghjklzxcvbnm" + "qwertyuiopasdfghjklzxcvbnm".ToUpper();

    public static IEnumerable<string> GetRusWords(this string text)
    {
        return text.SplitWords().Where(word => word.IsRus()).Select(word => word.Trim().ToLower());
    }

    public static IEnumerable<string> GetEngWords(this string text)
    {
        return text.SplitWords().Where(word => word.IsEng()).Select(word => word.Trim().ToLower());
    }
    public static IEnumerable<string> GetNumbers(this string text)
    {
        return text.SplitWords().Where(word => word.IsNumber()).Select(word => word.Trim().ToLower());
    }
    public static IEnumerable<string> GetDates(this string text)
    {
        return text.SplitWords().Where(word => word.IsDate()).Select(word => word.Trim().ToLower());
    }
    public static bool IsType(this string text)
    {

        return text.ToType() != null;
    }

    public static IEnumerable<string> GetTypeNames(this string text)
    {
        return text.SplitWords().Where(word => word.IsType()).Select(word => word.Trim().ToLower());
    }

    public static string Before(this string text, string substring)
    {
        if (text.IndexOf(substring) == -1)
            return "";
        string ends = text.After(substring);
        return text.Substring(0, text.Length - ends.Length - substring.Length);        
    }
    public static string After(this string text, string substring)
    {
        int i = text.IndexOf(substring);
        if (i == -1) return "";
        return text.Substring(i + substring.Length);
    }

    public static IEnumerable<string> SplitWords(this string text)
    {
        var words = new List<string>();
        string p = "";

        foreach(char ch in text.ToCharArray())
        {
            if((ch + "").IsRus() || (ch + "").IsEng())
            {
                p += ch;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(p) == false)
                    words.Add(p);
                p = "";
            }
        }
        if (String.IsNullOrWhiteSpace(p) == false)
            words.Add(p);
        return words;
    }


    public static bool IsRus(this char word)
        => "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя".Contains(word);
    public static bool IsRus(this string word)
    {
        string alf = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
        string text = word;
        for (int i = 0; i < text.Length; i++)
        {
            if (!alf.Contains(text[i]))
            {
                return false;
            }
        }
        return true;
    }
    
    public static bool IsPuntuationsOrSplitters(this string word)
    {
        var charset = GetAsciiChars().Where(ch => ch.IsRus() == false && ch.IsEng() == false && ch.IsNumber() == false);

        for (int i = 0; i < word.Length; i++)
        {

            
            if (!charset.Contains(word[i]))
            {
                return false;
            }
        }
        return true;
    }

    private static IEnumerable<char> GetAsciiChars()
    {
        for(int i=09; i<byte.MaxValue; i++)
        {
            yield return (char)i;
        }
    }

    public static bool IsEng(this char word)
        => ENG_CHARS.ToCharArray().Contains(word);
    public static bool IsEng(this string word)
    {
        string alf = ENG_CHARS;
        string text = word;
        for (int i = 0; i < text.Length; i++)
        {
            if (!alf.Contains(text[i]))
            {
                return false;
            }
        }
        return true;
    }
}
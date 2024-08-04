using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Склонение по численному признаку
/// </summary>
public static class TextCountingExtensions
{

    /// <summary>
    /// Форматирует численность существительного по правилам англ. языка
    /// </summary>
    public class TextCounting
    {
        /// <summary>
        /// Возвращает существительное во множественном числе
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetMultiCountName(string table)
        {
            //определение наименования в множественном числе и единственном                        
            string tableName = table;
            string multicount_name = null;
            if (tableName.EndsWith("s"))
            {
                if (tableName.EndsWith("ies"))
                {
                    multicount_name = tableName;
                }
                else
                {
                    multicount_name = tableName;
                }
            }
            else
            {
                if (tableName.EndsWith("y"))
                {
                    multicount_name = tableName.Substring(0, tableName.Length - 1) + "ies";
                }
                else
                {
                    multicount_name = tableName + "s";
                }
            }
            return multicount_name;
        }


        /// <summary>
        /// Возвращает существительное в единственном
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSingleCountName(string name)
        {
            //определение наименования в множественном числе и единственном                        
            string tableName = name.Trim();
            string singlecount_name = null;
            if (tableName.EndsWith("s"))
            {
                if (tableName.EndsWith("ies"))
                {

                    singlecount_name = tableName.Substring(0, tableName.Length - 3) + "y";
                }
                else
                {
                    singlecount_name = tableName.Substring(0, tableName.Length - 1);
                }
            }
            else
            {
                if (tableName.EndsWith("y"))
                {

                    singlecount_name = tableName;

                }
                else
                {
                    singlecount_name = tableName;
                }
            }
            return singlecount_name;
        }
    }

    /// <summary>
    /// Возвращает существительное во множественном числе
    /// </summary>   
    public static bool IsMultiCount(this string name)
    {
        return TextCounting.GetMultiCountName(name) == name;
    }

    /// <summary>
    /// Возвращает существительное в ед. числе
    /// </summary>   
    public static bool IsSingleCount(this string name)
    {
        return TextCounting.GetSingleCountName(name) == name;
    }
    public static bool IsTsqlStyled(this string name)
    {
        throw new NotImplementedException($"{TypeExtensions2.GetTypeName(typeof(TextCountingExtensions))}");
    }
    

    
    /// <summary>
    /// Возвращает существительное во множественном числе
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string ToMultiCount(this string name)
    {
        return TextCounting.GetMultiCountName(name);
    }


    /// <summary>
    /// Возвращает существительное в единственном
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string ToSingleCount(this string name)
    {
        return TextCounting.GetSingleCountName(name);
    }

}

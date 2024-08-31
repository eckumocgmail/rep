using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TextCountingExtensions;

/// <summary>
/// Стилизация идентификаторов
/// </summary>
public static class TextNamingExtensions
{



    /// <summary>
    /// Реализует методы работы с идентификаторами и стилями записи
    /// </summary>
    public class TextNaming : TextCounting
    {
        private static string SPEC_CHARS = ",.?~!@#$%^&*()-=+/\\[]{}'\";:\t\r\n";
        private static string RUS_CHARS = "ЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ" + "ёйцукенгшщзхъфывапролджэячсмитьбю";
        private static string DIGIT_CHARS = "0123456789";
        private static string ENG_CHARS = "qwertyuiopasdfghjklzxcvbnm" + "QWERTYUIOPASDFGHJKLZXCVBNM";




        /// <summary>
        /// Метод разбора идентификатора на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitName(string name)
        {
            TextNamingStyles style = ParseStyle(name);
            switch (style)
            {
                case TextNamingStyles.Cascading: return SplitCascadingName(name);
                case TextNamingStyles.Kebab: return SplitKebabName(name);
                case TextNamingStyles.Snake: return SplitSnakeName(name);
                case TextNamingStyles.Capital: return SplitCapitalName(name);
                case TextNamingStyles.Camel: return SplitCamelName(name);
                default:
                    throw new Exception($"Не удалось разобрать идентификатор {name}.");
            }
        }

        /// <summary>
        /// Метод разбора идентификатора записанного в CascadingStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в KebabStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitCascadingName(string name)
        {
            return name.Split(":");
        }


        /// <summary>
        /// Запись идентификатора в CapitalStyle
        /// </summary>
        /// <param name="lastname"> идентификатор </param>
        /// <returns>идентификатор в CapitalStyle</returns>
        public static string ToCapitalStyle(string lastname)
        {
            if (string.IsNullOrEmpty(lastname)) return lastname;
            string[] ids = SplitName(lastname);
            return ToCapitalStyle(ids);
        }
        public static string ToCapitalStyle(string[] ids)
        {
            string name = "";
            foreach (string id in ids)
            {
                name += id.Substring(0, 1).ToUpper() + id.Substring(1).ToLower();
            }
            return name;
        }


        /// <summary>
        /// Запись идентификатора в CamelStyle
        /// </summary>
        /// <param name="lastname"> идентификатор </param>
        /// <returns>идентификатор в CamelStyle</returns>
        public static string ToCamelStyle(string lastname)
        {
            string name = ToCapitalStyle(lastname);
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }




        /// <summary>
        /// Запись идентификатора в KebabStyle
        /// </summary>
        /// <param name="lastname"> идентификатор </param>
        /// <returns>идентификатор в KebabStyle</returns>
        public static string ToKebabStyle(string lastname)
        {
            string name = "";
            string[] names = SplitName(lastname);
            foreach (string id in names)
            {
                name += "_" + id.ToUpper();
            }
            return name.Substring(1);
        }





        /// <summary>
        /// Запись идентификатора в SnakeStyle
        /// </summary>
        /// <param name="lastname"> идентификатор </param>
        /// <returns>идентификатор в SnakeStyle</returns>
        public static string ToSnakeStyle(string lastname)
        {
            string name = "";
            string[] names = SplitName(lastname);
            foreach (string id in names)
            {
                name += "_" + id.ToLower();
            }
            return name.Substring(1);
        }


        /// <summary>
        /// Метод разбора идентификатора записанного в CapitalStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в CapitalStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitCapitalName(string name)
        {
            List<string> ids = new List<string>();
            string word = "";
            bool WasUpper = false;
            foreach (char ch in name)
            {
                if (IsUpper(ch) && WasUpper == false)
                {
                    if (word != "")
                    {
                        ids.Add(word);
                    }
                    word = "";
                    WasUpper = true;
                }
                WasUpper = false;
                word += (ch + "");
            }
            if (word != "")
            {
                ids.Add(word);
            }
            word = "";
            return ids.ToArray();
        }


        /// <summary>
        /// Метод разбора идентификатора записанного в DollarStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в DollarStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitDollarName(string name)
        {
            List<string> ids = new List<string>();
            string word = "";
            bool first = true;
            foreach (char ch in name)
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (IsUpper(ch))
                {
                    if (word != "")
                    {
                        ids.Add(word);
                    }
                    word = "";
                }
                word += (ch + "");
            }
            if (word != "")
            {
                ids.Add(word);
            }
            word = "";
            return ids.ToArray();
        }


        /// <summary>
        /// Метод разбора идентификатора записанного в CamelStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в CamelStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitCamelName(string name)
        {
            List<string> ids = new List<string>();
            string word = "";
            foreach (char ch in name)
            {
                if (IsUpper(ch))
                {
                    if (word != "")
                    {
                        ids.Add(word);
                    }
                    word = "";
                }
                word += (ch + "");
            }
            if (word != "")
            {
                ids.Add(word);
            }
            word = "";
            return ids.ToArray();
        }


        /// <summary>
        /// Метод разбора идентификатора записанного в SnakeStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в SnakeStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitSnakeName(string name)
        {
            return name.Split("_");
        }


        /// <summary>
        /// Метод разбора идентификатора записанного в KebabStyle на модификаторы 
        /// </summary>
        /// <param name="name"> идентификатор записанный в KebabStyle </param>
        /// <returns> модификаторы </returns>
        public static string[] SplitKebabName(string name)
        {
            return name.Split("-");
        }


        /// <summary>
        /// Метод определния стиля записи идентификатора
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> стиль записи </returns>
        public static TextNamingStyles ParseStyle(string name)
        {
            if (IsCapitalStyle(name))
                return TextNamingStyles.Capital;
            if (IsKebabStyle(name))
                return TextNamingStyles.Kebab;
            if (IsSnakeStyle(name))
                return TextNamingStyles.Snake;


            if (IsCasacadingStyle(name))
                return TextNamingStyles.Cascading;
            if (IsCamelStyle(name))
                return TextNamingStyles.Camel;

            throw new Exception($"Стиль идентификатора {name} не определён.");
        }

        private static bool IsCasacadingStyle(string name)
        {
            foreach (char ch in name)
            {
                if ((ch + "").IsEng() || (ch + "").IsRus())
                {
                    continue;
                }
                else
                {
                    if (ch == ':' && name.StartsWith(":") == false && name.EndsWith(":") == false)
                        continue;
                    else return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Проверка сивола на принадлежность с множеству цифровых символов
        /// </summary>
        /// <param name="ch"> символ </param>
        /// <returns>true, если символ цифровой</returns>
        public static bool IsDigit(char ch)
        {
            return Contains(DIGIT_CHARS, ch);
        }


        /// <summary>
        /// Проверка сивола на принадлежность с множеству символов русского алфавита
        /// </summary>
        /// <param name="ch"> символ </param>
        /// <returns>true, если символ из русского алфавита </returns>
        public static bool IsCharacter(char ch)
        {
            return IsRussian(ch) || IsEnglish(ch);
        }


        /// <summary>
        /// Проверка сивола на принадлежность с множеству символов русского алфавита
        /// </summary>
        /// <param name="ch"> символ </param>
        /// <returns>true, если символ из русского алфавита </returns>
        public static bool IsRussian(char ch)
        {
            return Contains(RUS_CHARS, ch);
        }


        /// <summary>
        /// Проверка сивола на принадлежность с множеству символов русского алфавита
        /// </summary>
        /// <param name="ch"> символ </param>
        /// <returns>true, если символ из русского алфавита </returns>
        public static bool IsEnglish(char ch)
        {
            return Contains(ENG_CHARS, ch);
        }


        /// <summary>
        /// Проверка принадлежности символа к строке
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool Contains(string text, char ch)
        {
            bool result = false;
            foreach (char rch in text)
            {
                if (rch == ch)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }


        /// <summary>
        /// Метод проверки символа на принадлежность к верхнему регистру
        /// </summary>
        /// <param name="ch"> символ </param>
        /// <returns> true, если принадлежит верхнему регистру </returns>
        public static bool IsUpper(char ch)
        {
            return (ch + "") == (ch + "").ToUpper();
        }


        /// <summary>
        /// Проверка стиля записи CapitalStyle( UserId )
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> true, если идентификатор записан в CapitalStyle </returns>
        public static bool IsCapitalStyle(string name)
        {
            bool startedWithUpper = (name[0] + "") == (name[0] + "").ToUpper();
            bool containsSpecCharaters = name.IndexOf("_") != -1 || name.IndexOf("$") != -1;
            return startedWithUpper && !containsSpecCharaters;
        }


        /// <summary>
        /// Проверка стиля записи SnakeStyle( user_id, USER_Id )
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> true, если идентификатор записан в SnakeStyle </returns>
        public static bool IsSnakeStyle(string name)
        {
            bool upperCase = IsUpper(name[0]);
            bool startsWithCharacter = IsCharacter(name[0]);
            char separatorCharacter = '_';
            string anotherChars = new String(SPEC_CHARS).Replace(separatorCharacter + "", "");
            bool containsAnotherSpecChars = false;
            bool containsAnotherCase = false;
            bool containsDoubleSeparator = false;
            bool lastCharWasSeparator = false;
            if (startsWithCharacter == false)
            {
                return !containsDoubleSeparator && !containsAnotherCase && startsWithCharacter && !containsAnotherSpecChars && !containsAnotherCase;
            }
            else
            {
                for (int i = 1; i < name.Length; i++)
                {
                    if (Contains(anotherChars, name[i]))
                    {
                        containsAnotherSpecChars = true;
                        break;
                    }
                    if (name[i] != separatorCharacter)
                    {
                        if (IsUpper(name[i]) != upperCase)
                        {
                            containsAnotherCase = true;
                            break;
                        }
                        lastCharWasSeparator = false;
                    }
                    else
                    {
                        if (lastCharWasSeparator)
                        {
                            containsDoubleSeparator = true;
                            break;
                        }
                        lastCharWasSeparator = true;
                    }
                }
            }
            return !containsDoubleSeparator && !containsAnotherCase && startsWithCharacter && !containsAnotherSpecChars && !containsAnotherCase;
        }


        /// <summary>
        /// Проверка стиля записи CamelStyle( userId  )
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> true, если идентификатор записан в CamelStyle </returns>
        public static bool IsCamelStyle(string name)
        {
            return IsCapitalStyle(name.Substring(0, 1).ToUpper() + name.Substring(1)) && !IsUpper(name[0]) && IsCharacter(name[0]);
        }


        /// <summary>
        /// Проверка стиля записи DollarStyle( $userId  )
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> true, если идентификатор записан в DollarStyle </returns>
        public static bool IsDollarStyle(string name)
        {
            return IsCamelStyle(name.Substring(1)) && name[0] == '$';
        }


        /// <summary>
        /// Проверка стиля записи KebabStyle( user-id, USER-Id )
        /// </summary>
        /// <param name="name"> идентификатор </param>
        /// <returns> true, если идентификатор записан в KebabStyle </returns>
        public static bool IsKebabStyle(string name)
        {

            bool upperCase = IsUpper(name[0]);
            bool startsWithCharacter = IsCharacter(name[0]);
            char separatorCharacter = '-';
            if (name.Contains(separatorCharacter) == false)
                return false;
            string anotherChars = new String(SPEC_CHARS).Replace(separatorCharacter + "", "");
            bool containsAnotherSpecChars = false;
            bool containsAnotherCase = false;
            bool containsDoubleSeparator = false;
            bool lastCharWasSeparator = false;
            if (startsWithCharacter == false)
            {
                return !containsDoubleSeparator && !containsAnotherCase && startsWithCharacter && !containsAnotherSpecChars && !containsAnotherCase;
            }
            else
            {
                for (int i = 1; i < name.Length; i++)
                {
                    if (Contains(anotherChars, name[i]))
                    {
                        containsAnotherSpecChars = true;
                        break;
                    }
                    if (name[i] != separatorCharacter)
                    {
                        if (IsUpper(name[i]) != upperCase)
                        {
                            containsAnotherCase = true;
                            break;
                        }
                        lastCharWasSeparator = false;
                    }
                    else
                    {
                        if (lastCharWasSeparator)
                        {
                            containsDoubleSeparator = true;
                            break;
                        }
                        lastCharWasSeparator = true;
                    }
                }
            }
            return !containsDoubleSeparator && !containsAnotherCase && startsWithCharacter && !containsAnotherSpecChars && !containsAnotherCase;
        }
    }

    /// <summary>
    /// Метод определния стиля записи идентификатора
    /// </summary>
    /// <param name="name"> идентификатор </param>
    /// <returns> стиль записи </returns>
    public static TextNamingStyles ParseStyle(this string name)
    {
        return TextNaming.ParseStyle(name);
    }


    /// <summary>
    /// Запись идентификатора в CamelStyle
    /// </summary> 
    public static string ToCamelStyle(this string name)
    {
        return TextNaming.ToCamelStyle(name);
    }

    /// <summary>
    /// Запись идентификатора в CapitalStyle
    /// </summary> 
    public static string ToCapitalStyle(this string name)
    {
        return TextNaming.ToCapitalStyle(name);
    }




    /// <summary>
    /// Запись идентификатора в KebabStyle
    /// </summary> 
    public static string ToKebabStyle(this string lastname)
    {
        return TextNaming.ToKebabStyle(lastname);
    }
    
    public static string[] SplitName(this string name)
    {
        return TextNaming.SplitName(name);
    }

    /// <summary>
    /// Запись идентификатора в KebabStyle
    /// </summary> 
    public static string ToTSQLStyle(this string lastname)
    {
        
        if (lastname.ToLower().EndsWith("_id"))
            return TextNaming.ToKebabStyle(lastname.Substring(0,lastname.Length-3))+"_Id";
        else
        return TextNaming.ToKebabStyle(lastname);
    }

    /// <summary>
    /// Запись идентификатора в SnakeStyle
    /// </summary>

    public static string ToSnakeStyle(this string lastname)
    {
        return TextNaming.ToSnakeStyle(lastname);
    }

}

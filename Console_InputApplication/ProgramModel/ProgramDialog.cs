using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Функции исп. программой
/// </summary>
using static InputConsole;
using static InputConsole; 

/// <summary>
/// Функции взаимодейтсвия с пользователем через консоль
/// </summary>
public class ProgramDialog: ProgramBase
{
    public static string NUMBERS = "1234567890";
    public static bool UserInteractive = Environment.UserInteractive;
     
    public ProgramDialog(): base(){}
  


    /// <summary>
    /// Пользователь вводит значение параметра в консоли,
    /// если консоль общается не с пользователем, по 
    /// ввод осуществляет "Поток управления" базового типа.
    /// </summary>
    public static IList<string> ReadArgs(params string[] keys)
    {
        var result = new List<string>();
        IDictionary<string, string> pars = new ConcurrentDictionary<string, string>();
        foreach (var key in keys)
        {
            Console.Write(key + ">");
            result.Add(key);

            pars[key] = Console.ReadLine();
            result.Add(pars[key]);
        }
        return result;
    }

    [Label("ввод чисел")]
    public static int InputNumber( string name, Func<int, List<string>> validate,ref string[] args )
    {
        string text = "";
        do
        {
            text = InputConsole.InputString(name, value => value!=null & value.ToString().IsNumber() ? new List<string>() : new List<string>() {
                "Значение не является числовым"
            }, ref args);
        } while (text.IsInt() == false);
        return text.ToInt();
    }

    [Label("ввод адреса эл. почты")]
    public static string InputEmail(ref string[] args, string name)
    {
        string text = "";
        do
        {
            text = InputConsole.InputString(name, value => value != null & value.ToString().IsEmail() ? new List<string>() : new List<string>() {
                "Значение не является Email"
            }, ref args);
        } while (text.IsEmail() == false);
        return text;
    }

    [Label("ввод URL адреса")]
    public static string InputUrl(ref string[] args, string name)
    {
        string text = "";
        do
        {
            text = InputConsole.InputString(name, value=> value != null & value.ToString().IsUrl()? new List<string>(): new List<string>() { 
                "Значение не является URL"
            }, ref args);
        } while (text.IsUrl() == false);
        return text;
    }


    public static void Question(params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        InputConsole.GetApp().Info(args);
        Console.ResetColor();
    }


    /// <summary>
    /// Запрос ввода 
    /// </summary>   
    public static string ReadLine(string key="")
    {
        InputConsole.Get().Info(key);
        Write("\t>");
        return Console.ReadLine();
    }
        

    /// <summary>
    /// Считавание из консоли
    /// </summary>
    public static int ReadNatural(int max)
    {
        if (max > 9 || max <= 0)
            throw new ArgumentException("max");
        string result = "";
        do
        {
            var key = Console.ReadKey();
            result = key.KeyChar+"";
        } while ((NUMBERS.Contains(result) == false) || (int.Parse(result + "") > max));
        return int.Parse(result);
    }


    /// <summary>
    /// Предоставляет интерфейс для выбора одного элмента из множества вариантов
    /// </summary>
    /// <param name="options">доступные варианты</param>
    public static string SelectOption(string[] options)
    {
        int counter = 0;
        Info(options.Select(option => $"\t  ({(++counter)}) {option}").ToArray());
        string readed = (options.Length <= 9) ? ReadNatural(counter).ToString() : ReadNumber(0, options.Length);
        Info($"Введено {readed}");
        string state = options[int.Parse(readed) - 1];
        Info("\n\t" + state);
        return state;
    }
   

    /// <summary>
    /// Считывание текстового обозначения нажатой клавиши
    /// </summary>
    public static string ReadKeyTextValue()
        => Console.ReadKey().Key.ToString();

    /// <summary>
    /// Считывание числового значения с устройвт ввода
    /// </summary>
    public static string ReadNumber(int min, int max)
    {
        bool correct = false;
        string text = "";
        while(correct == false)
        {
            string key = ReadKeyTextValue();
            switch (key.ToString().ToLower())
            {
                case "d1":
                case "d2":
                case "d3":
                case "d4":
                case "d5":
                case "d6":
                case "d7":
                case "d8":
                case "d9":
                case "d0":
                        text += key.Substring(1);
                        break;
                case "enter":
                    if (text.Length > 0)
                        return text;
                    else return "0";
                case "backspace":
                    if (text.Length > 0)
                        text = text.Substring(0, text.Length - 1);
                    break;
            }
        }
        throw new Exception();
    }

    /// <summary>
    /// Предоставляет интерфейс для выбора одного элмента из множества вариантов
    /// </summary>
    /// <param name="options">доступные варианты</param>
    public static string SingleSelectOption( IEnumerable<string> options)
    {
        Console.Title = "";
        string readed = "";
        WriteLine(options.Select(option => $"\t  ({(options.Count())}) {option}").ToArray());
        readed = ReadNatural(options.Count()).ToString();
        Info($"Введено: {readed}");
        string state = options.ToArray()[int.Parse(readed) - 1];
        return state;
    }
    public static Action SwitchContinue(string title, IDictionary<string,Action> options, ref string[] args)
    {
        Clear();
        options["Назад"] = () => { };
        return options[SingleSelect(title, options.Keys.ToList(), ref args)];
    }
    public static string SingleSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        Console.Title = title;
        if( options==null || options.Count()==0 )
            throw new Exception("Элементы выбора не определены");
        int index = -1;
        while(index<0 || index >= options.Count())
        {
            try
            {
                string readed = "";
                if (UserInteractive == false)
                {
                    if (args.Length > 0)
                    {
                        var argsOut = new string[args.Length - 1];
                        for (int i = 1; i < args.Length; i++)
                        {
                            argsOut[i - 1] = args[i];
                        }
                        readed = options.ToList().IndexOf(args[0]).ToString();
                        if (readed != "-1")
                        {
                            throw new ArgumentException($"Аргумент { args[0]} задан неверно");
                        }
                        Info($"Введено: {readed}");


                        args = argsOut;
                        //WriteLine("\n\t" + state);
                        return options.ToArray()[int.Parse(readed) - 1];
                    }
                }

                int counter = 0;
                WriteLine($"\t{title}");

                WriteLine(options.Select(option => $"\t  ({(++counter)}) {option}").ToArray());
                readed = (options.Count() <= 9) ? ReadNatural(counter).ToString() : ReadNumber(0, options.Count());
                Info($"Введено: {readed}");

                index = int.Parse(readed) - 1;
            }catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }
        string state = options.ToArray()[index];
        //WriteLine("\n\t" + state);
        return state;
    }

    /// <summary>
    /// Выбор элемента из предложенных
    /// </summary>   
    public static T SingleSelect<T>(IEnumerable<T> enumerable, string title, Func<T, string> p, ref string[] args)
    {
        Dictionary<string, T> temp = new Dictionary<string, T>();
        string[] names = enumerable.Select(item => { string name = null; temp[name = p(item)] = item; return name; }).ToArray();
        if (names.Length != enumerable.Count())
            throw new Exception("Функция именования объектов возвращает не уникальные значения. ");
        return temp[ProgramDialog.SingleSelect(title, names, ref args)];
    }
    
    public static T SingleSelect<T>(IEnumerable<T> enumerable, Func<T, string> p, ref string[] args)
    {
        Dictionary<string, T> temp = new Dictionary<string, T>();
        string[] names = enumerable.Select(item => { string name = null; temp[name = p(item)] = item; return name; }).ToArray();
        if (names.Length != enumerable.Count())
            throw new Exception("Функция именования объектов возвращает не уникальные значения. ");
        return temp[ProgramDialog.SingleSelect("", names, ref args)];
    }

    /// <summary>
    /// Выбор элемента из предложенных
    /// </summary>   
    public static T SingleSelect<T>(IEnumerable<T> enumerable, Func<T, string> p )
    {
        Dictionary<string, T> temp = new Dictionary<string, T>();
        string[] names = enumerable.Select(item => { string name = null; temp[name = p(item)] = item; return name; }).ToArray();
        if (names.Length != enumerable.Count())
            throw new Exception("Функция именования объектов возвращает не уникальные значения. ");
        return temp[ProgramDialog.SingleSelectOption(names)];
    }
 
    public static object SingleSelect(IEnumerable<object> enumerable, Func<object, string> p,ref string[] args)
    {
        Dictionary<string, object> temp = new Dictionary<string, object>();
        string[] names = enumerable.Select(item => { string name = null; temp[name = p(item)] = item; return name; }).ToArray();
        if (temp.Count != enumerable.Count())
            throw new Exception("Функция именования объектов возвращает не уникальные значения. ");
        return temp[ProgramDialog.SingleSelect("",names,ref args)];
    }


    /// <summary>
    /// Вывод строк
    /// </summary>
    /// <param name="options"></param>
    public static void WriteLines(params string[] options)
    {
        Info("\n\tВыбери один элемент: ");
        foreach (var option in options)
        {
            Info("\n\t" + option);
        }
    }

}

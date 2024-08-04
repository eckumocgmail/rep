using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static InputConsole;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Console_InputApplication;
using static InputConsole;
using System.Collections.Concurrent;
using Console_InputApplication.ConsolePrograms;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using static StackTraceExtensions;

/// <summary>
/// Программа управления объектом через консоль
/// </summary>
public class ConsoleProgram<TServiceModel> : ConsoleProgram
{
    public ConsoleProgram()
    {
        ProgramDialog.UserInteractive = true;
    }

    public override void Run( ref string[] args)    
        => RunInteractive<TServiceModel>();


    public void Configure( IApplicationBuilder app )
    {
         
        app.Use(async (http, next) => 
        {
            try
            {
                this.Info(http.Request.Path.ToString());
                await next();
            }
            catch(Exception ex)
            {
                var info = ex.ToDocument();
                await http.Response.WriteAsJsonAsync(info);
            }
        });
    }
    
}
 
/// <summary>
/// Программа управления объектом через консоль
/// </summary>
public class ConsoleProgram: ProgressProgram
{
    [JsonIgnore]
    [NotMapped]
    public ILogger<ConsoleProgram> Logger = LoggerFactory.Create(options=>options.AddConsole()).CreateLogger<ConsoleProgram>();

    [JsonIgnore]
    [NotMapped]
    public object ProgramData;

    [JsonIgnore]
    [NotMapped]
    public MethodInfo ProgramAction;

    [JsonIgnore]
    [NotMapped]
    public Dictionary<string, object> ProgramArguments { get; set; }

    [JsonIgnore]
    [NotMapped]
    public object ActionResult { get; set; }

    
 
    public void PrintProgramData(ref string[] args)
    {
   
        if (ProgramData == null)
            throw new Exception("Нужно присвоить значение свойству ProgramData");
        Type ProgramType = ProgramData.GetType();

        this.Info($"[{ProgramType.GetTypeName()}] Свойства: \n{ProgramData.ToJsonOnScreen()}");
   
         

  
    }
/// <summary>
    /// вывод результатов процедуры
    /// </summary>
    public void ShowExecuteActionResults(ref string[] args)
    { 
        Confirm(new object[] { "Результаты выполнения", ActionResult }.ToJsonOnScreen()+"\nПродолжим?");
    }


    /// <summary>
    /// ввод параметров процедуры
    /// </summary>
    public void InputActionParameters(ref string[] args)
    {
        Clear();
        PrintProgramData(ref args);
        WriteLine($"\n {(ProgramAction != null ? ProgramAction.Name : "")}");
        int n = 1;
        var invokeParams = new List<object>();
        ProgramArguments = new Dictionary<string, object>();
        if (ProgramAction == null)
        {
            throw new Exception("Не выбрано действие для выполнения");
        }
        else
        {
            foreach (ParameterInfo par in ProgramAction.GetParameters())
            {
                WriteLine($"{n++}) {par.Name}: {par.ParameterType.Name}>");
                if (par.ParameterType.IsEnum == false)
                {
                    object result = TextDataSetter.ToType(ReadLine(), par.ParameterType);
                    ProgramArguments[par.Name] = result;
                    invokeParams.Add(result);
                }
                else
                {

                    string option = InputConsole.SelectOption(Enum.GetNames(par.ParameterType));
                    object result = null;
                    Enum.TryParse(par.ParameterType, option, true, out result);
                    ProgramArguments[par.Name] = result;
                    invokeParams.Add(result);
                }

            }
        }
        try
        {
            ConfirmExecute($"Выполнить: {ProgramAction.Name}({invokeParams.ToJson()})", () =>
            {
                Info(ActionResult = ProgramAction.Invoke(ProgramData, invokeParams.ToArray()));
                InputConsole.ConfirmContinue();
            });
            

        }
        catch (Exception ex)
        {
            throw new Exception($"Исключение проброшено из метода {ProgramData.GetTypeName()}.{ProgramAction.Name}", ex);
        }
    }




    /// <summary>
    /// Интерфейс множественного выбора элементов из заданого множества
    /// </summary>
    /// <typeparam name="T">Тип выбираемого объекта</typeparam>
    /// <param name="applications">Множество доступное для выбора</param>
    /// <param name="p">Функция предосавляет заголовок элемента в списке</param>
    /// <returns>Выбранные элементы</returns>
    /// <exception cref="CancelException"></exception>
    public IEnumerable<T> CheckListTitle<T>(
        string title,
        IEnumerable<T> applications,
        Func<T, string> convert)
    {
        if (ProgramDialog.UserInteractive == false)
            return applications;
        var result = new List<T>();
        Action complete = () => { throw new CompleteException(); };
        var items = applications
                    .Select(Item => new Dictionary<string, object> {
                        { "Item",Item},
                        { "Selected",false},
                        { "Label",convert(Item)}
                    }).ToList();
        int cursor = 0;
        ReadKeyRepeat<List<T>>(

            // before read
            () => {
                Console.WriteLine("\n" + "\n  " + title + "\n");
                int ctn = 0;
                foreach (dynamic ListItemTitle in items)
                {
                    if (ctn++ == cursor)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [{((bool)(ListItemTitle["Selected"]) ? "x" : " ")}]  {ListItemTitle["Label"]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  [{((bool)(ListItemTitle["Selected"]) ? "x" : " ")}]  {ListItemTitle["Label"]}");
                    }

                }
            },

            // onKeyPressed
            (key) => {
                Clear();
                Console.WriteLine(key + " " + cursor);
                switch (key.ToString().Trim())
                {
                    case "Enter": throw new CompleteException();
                    case "Ex":
                        items.ToArray()[cursor]["Selected"] = ((bool)items.ToArray()[cursor]["Selected"]) ? false : true;
                        break;
                    case "Spacebar":
                        items.ToArray()[cursor]["Selected"] = ((bool)items.ToArray()[cursor]["Selected"]) ? false : true;
                        break;
                    case "UpArrow":
                        cursor = cursor - 1;
                        if (cursor < 0)
                            cursor = applications.Count() - 1;
                        break;
                    case "DownArrow":
                        cursor = cursor + 1;
                        if (cursor >= applications.Count())
                            cursor = 0;
                        break;
                    default: break;
                }
            },

            // onCompleted
            () => {
                return result = items.Where(item => ((bool)item["Selected"])).Select(item => (T)item["Item"]).ToList();
            },

            // onCanceled
            () => {
            }
        );
        return result;
    }


    public static string Shift(ref string[] args)
    {
        string value = args[0];
        var argsOut = new string[args.Length - 1];
        for (int i = 1; i < args.Length; i++)
        {
            argsOut[i - 1] = args[i];
        }
        args = argsOut;
        return value;
    }

    public IEnumerable<T> CheckListTitle<T>(
        string title,
        IEnumerable<T> applications,
        IEnumerable<T> selected,
        Func<T, string> convert)
    {
        if (ProgramDialog.UserInteractive == false)
            return applications;
        var result = new List<T>();
        Action complete = () => { throw new CompleteException(); };
        var items = applications
                    .Select(Item => new Dictionary<string, object> {
                        { "Item",Item},
                        { "Selected",selected.Contains(Item)},
                        { "Label",convert(Item)}
                    }).ToList();
        int cursor = 0;
        ReadKeyRepeat<List<T>>(

            // before read
            () => {
                Console.WriteLine("\n" + "\n  " + title + "\n");
                int ctn = 0;
                foreach (dynamic ListItemTitle in items)
                {
                    if (ctn++ == cursor)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [{((bool)(ListItemTitle["Selected"]) ? "x" : " ")}]  {ListItemTitle["Label"]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  [{((bool)(ListItemTitle["Selected"]) ? "x" : " ")}]  {ListItemTitle["Label"]}");
                    }

                }
            },

            // onKeyPressed
            (key) => {

                Clear();
                Console.WriteLine(key + " " + cursor);
                switch (key.ToString().Trim())
                {
                    case "Enter": throw new CompleteException();
                    case "Spacebar":
                        items.ToArray()[cursor]["Selected"] = ((bool)items.ToArray()[cursor]["Selected"]) ? false : true;
                        break;
                    case "UpArrow":
                        cursor = cursor - 1;
                        if (cursor < 0)
                            cursor = applications.Count() - 1;
                        break;
                    case "DownArrow":
                        cursor = cursor + 1;
                        if (cursor >= applications.Count())
                            cursor = 0;
                        break;
                    default: break;
                }
            },

            // onCompleted
            () => {
                return result = items.Where(item => ((bool)item["Selected"])).Select(item => (T)item["Item"]).ToList();
            },

            // onCanceled
            () => {
            }
        );
        return result;
    }


    public string InputString(ref string[] args, string name)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.Write(name + ">");
        Console.ResetColor();
        if (ProgramDialog.UserInteractive)
        {
            string result = Console.ReadLine();
            return result;
        }
        else
        {
            string value = Shift(ref args);
            Console.WriteLine(value);
            return value;
        }
    }

    
    public bool PressEnterForContinue(ref string[] args, string message = "")
    {

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Info(message);


        Console.WriteLine("\nДля продолжения нажмите ENTER....");
        Console.ResetColor();
        if (ProgramDialog.UserInteractive)
        {
            bool result = String.IsNullOrWhiteSpace(Console.ReadLine());
            Console.Clear();
            return result;
        }
        else
        {
            if (args.Length == 0)
            {
                if (DebugMode)
                {
                    string selected = null;
                    switch ((selected = ProgramDialog.SingleSelectOption(new string[] { "Подтвердить", "Отменить" })))
                    {
                        case "Подтвердить": return true;
                        case "Отменить": return true;
                        default:
                            throw new NotSupportedException(selected);
                    }
                }
                else
                {
                    throw new Exception("Не достаточно аргументов");
                }

            }
            else
            {
                string value = args[0];
                var argsOut = new string[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    argsOut[i - 1] = args[i];
                }
                args = argsOut;
                return value.Trim().ToLower() == "1" || value.Trim().ToLower() == "true";
            }
        }
    }


    public void ConfirmExecute(string Name, Action Todo )
    {
        Clear();
        string[] args = new string[0];
        string selected = null;
        switch(selected = InputConsole.SingleSelect
            (
                "\nРазрешите выполнить операцию: " + Name, 
                new string[] {
                    "Разрешаю","Не раазрешаю"
                }, ref args))
        {
            case "Разрешаю":
                try { Todo(); } catch (Exception ex) { throw new Exception("Исключение при выполнении динамической операции",ex); }
                break;
            case "Не раазрешаю": break; 
            default: throw new Exception("Нет обработчика дял выбранного значнеия "+selected);
        }
    }


    /// <summary>
    /// Передача управления клавиатуре
    /// </summary> 
    public static T ReadKeyRepeat<T>(
        Action beforeRead,
        Action<object> onKeyPressed,
        Func<T> onCompleted,
        Action onCanceled)
    {
        try
        {
            while (true)
            {
                beforeRead();
                onKeyPressed(Console.ReadKey().Key);
            }
        }
        catch (CompleteException)
        {
            return onCompleted();
        }
        catch (CancelException ex)
        {
            Error(ex.Message);
            onCanceled();
            throw;
        }
    }


    public static string SingleSelect(IDictionary<string, string> items)
    {
        var selectedKey = SingleSelect<string>(items.Keys);
        return items[selectedKey];
    }


    public static T SingleSelect<T>(IDictionary<string, T> items)
    {
        return items[SingleSelect<string>(items.Keys)];
    }

    public static T SingleSelect<T>(IEnumerable<T> items) where T : class
    {
        try
        {
            int i = 1;

            ConsoleKeyInfo key;
            do
            {
                Clear();
                WriteLine(0 + ")" + "выход");
                foreach (var next in items)
                {
                    string label = next.ToString();
                    Console.WriteLine(i + $"){label}");
                    i++;
                }
                key = Console.ReadKey();
                Thread.Sleep(100);
            } while (key.KeyChar < '0' || key.KeyChar > items.Count().ToString().ToCharArray()[0]);
            int index = int.Parse(key.KeyChar.ToString()) - 1;
            if (index == -1)
            {
                throw new CancelException();
            }

            return items.ToArray()[index];
        }
        catch (Exception ex)
        {
            Error(ex);
            throw;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public void SelectNextAction( ref string[] args )
    {
        try
        {
            var pType = ProgramData.GetType();
            var methods = new Dictionary<string,string>(pType.GetOwnMethodNames().Select(name => new KeyValuePair<string, string>(pType.GetMethodLabel(name),name)));                        
            string action = methods.SingleSelect("Выберите действие:", ref args);
            ProgramAction = ProgramData.GetType().GetMethods().Where(m => m.Name == action).FirstOrDefault();
            if (ProgramAction == null)
                throw new Exception("Неправильно выполнена функция выбора из коллекции");
            RoutingProgram.Run(ProgramData.GetType().GetMethodLabel( ProgramAction.Name));
        }
        catch (CancelException)
        {
            throw;
        
        }catch (Exception ex)
        {
            WriteLine(ex);
        }
    }
    
    public class CancelException : Exception { }
    public class CompleteException : Exception { }

    public static bool DebugMode { get; private set; } = true;

    public static string[] RunInteractive( object target )
    {
        string[] args = new string[0];
        Type ProgramType = target.GetType();
        var console = new ConsoleProgram();
        console.ProgramData = target == null ? ProgramType.New() : target;
        while (true)
        {
            console.PrintProgramData(ref args);
            console.SelectNextAction(ref args);
            Console.Clear();
            console.InputActionParameters(ref args);
            console.ShowExecuteActionResults(ref args);
        }
    }



    public static void RunInteractive<TypeOfPogram>()
        => RunInteractive<TypeOfPogram>((TypeOfPogram)typeof(TypeOfPogram).New());

    public static void RunInteractive<TypeOfPogram>(ref string[] args)
        => RunInteractive<TypeOfPogram>((TypeOfPogram)typeof(TypeOfPogram).New(), ref args);


    public static void RunInteractive<TypeOfPogram>(TypeOfPogram instance)
    {
        var args = new string[0];
        InputConsole.Interactive();
        RunInteractive<TypeOfPogram>(instance,ref args);
    }



    public static void RunInteractive<TypeOfPogram>(TypeOfPogram instance, ref string[] args)
    {
        InputConsole.Interactive();
        Type ProgramType = typeof(TypeOfPogram);
        using (var route = new RoutingProgram($"Конфигурация {ProgramType.GetTypeName()}"))
        {

            var console = new ConsoleProgram();
            console.ProgramData = instance == null ? ProgramType.New() : instance;
            while (true)
            {
                Console.Clear();
                console.PrintProgramData(ref args);
                console.SelectNextAction(ref args);
                Console.Clear();
                console.InputActionParameters(ref args);
                console.ShowExecuteActionResults(ref args);
            }
        }



        
    }



    /// <summary>
    /// 
    /// </summary>    
    public TResult Invoke<TService,TResult>(string action, params object[] args)
    {
        object injected = typeof(TResult).New();
        if (injected == null)
            throw new Exception("Не удалось найти сервис " + typeof(TService).Name);
        TService instance = (TService)injected;
        var methodInfo = typeof(TService).GetMethod(action);
        if (methodInfo == null)
            throw new Exception($"Не удалось найти метод {action} у сервиса " + typeof(TService).Name);
        try
        {
            object result = methodInfo.Invoke(injected, args);
            return (TResult)result;
        }
        catch (Exception ex)
        {
            string text = "";
            if (args != null)
            {
                foreach (var arg in args)
                {
                    text += arg + ",";
                }
                if (args.Length > 0)
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }
            throw new Exception($"Не удалось выполнить метод {action} у сервиса " + typeof(TService).Name +
                " с аргументами: " + text, ex);
        }
    }

 

    /// <summary>
    /// 
    /// </summary>    
    public static void RunConsoleProgram(ref string[] args)
    {
        while (true)
        {
            try
            {
                Clear();
                Info("Укажите путь к сборке dll или exe файлу чтобы подключить консоль управления.");                
                //Assembly target = Assembly.LoadFile(Console.ReadLine());
                //Type type = target.GetClassTypes().UserSelectSingle<Type>(type=>type.GetTypeName());
                
            }
            catch(Exception ex)
            {
                ex.ToString().WriteToConsole();
            }
        }
    }


    
}


public class ConsoleProgramTest : TestingUnit
{


    public ConsoleProgramTest(TestingUnit parent) : base(parent)
    {
        
    }
     
    private void canRunAsConsole(ref string[] args)
    {
        ConsoleProgram.RunInteractive<MyControllerModel>(ref args);
    }
}

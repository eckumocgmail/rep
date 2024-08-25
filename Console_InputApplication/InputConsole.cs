using Console_InputApplication.InputApplicationModule;

 

using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Console_InputApplication;
using Microsoft.Extensions.Logging;
using static ConsoleProgram;
using static System.Net.Mime.MediaTypeNames;
using AngleSharp.Common;

[Label("Программма ввода данных")]
public class InputConsole: ProgramDialog   
{
    public static int InputTcpPort(string title, ref string[] args)
    => InputConsole.InputPositiveNumber(
           title,
           value => GetOpenPort().Any(info => info.Local.ToInt() == value.ToString().ToInt()) ?
               new List<string>() { "Похоже что этотпорт сейчас занят" } : new List<string>(),
           ref args);


    public record PortInfo(int PortNumber, string Local, string Remote, string State);

    /// <summary>
    /// Передача управления клавиатуре
    /// </summary> 
    public T ReadKeyRepeat<T>(
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
            this.Error(ex.Message);
            onCanceled();
            throw;
        }
    }
    public static List<PortInfo> GetOpenPort()
    {
        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
        TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

        return tcpConnections.Select(p =>
        {
            return new PortInfo(
                PortNumber: p.LocalEndPoint.Port,
                Local: String.Format("{0}:{1}", p.LocalEndPoint.Address, p.LocalEndPoint.Port),
                Remote: String.Format("{0}:{1}", p.RemoteEndPoint.Address, p.RemoteEndPoint.Port),
                State: p.State.ToString());
        }).ToList();
    }
    public static bool ConfirmContinue(string title="") {
        Console.WriteLine($"\n{title}");
        Console.WriteLine("\n Для продолжение нажмите пожалуйста клавишу ENTER..");
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey();
            //Console.Write(key.KeyChar);
        }
        while (key.Key != ConsoleKey.Enter) ;
        return true;
    }
    public static void Start(ref string[] args)
    {
        Test(ref args);
        new InputConsole().Run(ref args);
         
    }
    
    private static void Test(ref string[] args)
    {
        //ExceptionHandlingClause
        
        Console.Title = "Тестирование";
        var program = new TestingUnit();
        program.Info(args.ToJsonOnScreen());
        program.Append(new AttributesInfoTest());
        program.Append(new AssemblyExtensionsTest());
        program.Append(new GetEngWordsTest());
        program.Append(new TextConvertExtensionsTest());
        program.Append(new TextFactoryExtensionsTest());
        program.Append(new TextIOExtensionsTest());
        program.Append(new TextLangExtensionsTest());
        program.Append(new TextNamingExtensionsTest());
        program.Append(new TextNamingTest());
        program.Append(new TextValueExtensionsTest());
        program.Append(new AttributesInputTest());
        program.DoTest(false).ToDocument().WriteToConsole();
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


    public static string ReadText(string name, Func<string, List<string>> validate, ref string[] args )
    {
        string result = "";
        List<string> errors = new List<string>();
        do
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write(name + ">");
            Console.ResetColor();
            result = Console.ReadLine();
            errors = validate(result);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            foreach (var error in errors)
            {
                Console.WriteLine($"error: {error}");
            }
            Console.ResetColor();
            ConfirmContinue(  );
        } while (errors.Count() != 0);
        return result;
    }

    
    public static string Input(string name, ref string[] args)
        => InputString(name, value => new List<string>(), ref args);

    public static string InputString(string name, Func<string, List<string>> validate, ref string[] args)
    {
        
        if (ProgramDialog.UserInteractive)
        {

            string result = "";
            List<string> errors = new List<string>();
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.Write(name + ">");
                Console.ResetColor();
                result = Console.ReadLine();
                errors = validate(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                foreach (var error in errors)
                {
                    Console.WriteLine($"error: {error}");
                }
                Console.ResetColor();
                ConfirmContinue();
            } while (errors.Count() != 0);
            return result;
        }
        else
        {
            string value = Shift(ref args);
            Console.WriteLine(value);
            return value;
        }
    }
     

    /// <summary>
    /// ЗАпрос подтверждения у пользователя
    /// </summary>
    public static bool ConfirmActionDialog(string title, Action todo)
    {
        
        string selected = null;
        string[] args = null;
        switch ((selected = InputConsole.SingleSelect(title, new string[] { "Подтвердить", "Отменить" }, ref args)))
        {
            case "Подтвердить": 
                return true;
            case "Отменить": 
                return true;
            default:
                throw new NotSupportedException(selected);
        }
    }

    public static void Info(params object[] args) => GetApp().Info(args);
    public static void Warn(params object[] args) => GetApp().Warn(args);
    public static void Error(params object[] args) => GetApp().Error(args);

      
    public static void Clear()
    {
        Console.Clear();    
        Console.ResetColor();
    }

    public static string GetWrk()
        => System.IO.Directory.GetCurrentDirectory();
    public static IEnumerable<string> GetFiles()
        => GetWrk().GetFiles();
    public static IEnumerable<string> GetDlls()
        => GetWrk().GetFilesInAllSubdirectories("*.dll");
    public static IEnumerable<string> GetJsonFiles()
        => GetWrk().GetFiles().Where(f => f.ToLower().EndsWith(".json"));

    public static IEnumerable<string> GetIniFiles()
        => GetWrk().GetFiles().Where(f => f.ToLower().EndsWith(".ini"));


    public static void Write(object arg)
        => Console.Write(arg);

    public static void Write(object[] args)
    {
        foreach (var arg in args)
        {
            WriteLine(arg);
        }
    }
    private static ConsoleProgram Instance = new ConsoleProgram();

  

    public static string ReadLine() => Console.ReadLine();
    public static bool Confirm(string name)
    {
        Clear();
        string[] args = new string[0];
        string selected = null;
        switch (selected = InputConsole.SingleSelect
        (
                "\nРазрешите выполнить операцию: " + name,
                new string[] {
                    "Разрешаю","Не разрешаю"
                }, ref args))
        {
            case "Разрешаю": return true;
            case "Не разрешаю": return false;
            default: throw new Exception("Нет обработчика дял выбранного значения " + selected);
        }
    }
    public static void ConfirmExecute(string name, Action todo)
        => Instance.ConfirmExecute(name, todo);
    public static ConsoleKeyInfo ReadKey() => Console.ReadKey();
    public static ConsoleProgram Get()
       => Instance;
    public static void Exit() => Process.GetCurrentProcess().Kill();
    public static T Wait<T>(string title, Func<T> todo)
            => ProgressProgram.Wait<T>(title, todo);

    public static void Interactive( ){
        ProgramDialog.UserInteractive = true;        
    }
    public static IEnumerable<string> CheckList(string title, IEnumerable<string> options, ref string[] args)
        => Instance.CheckListTitle<string>(title, options, s=>s);

    public static IEnumerable<T> CheckListTitle<T>(string title, IEnumerable<T> applications, Func<T, string> convert)
        => Instance.CheckListTitle<T>(title, applications, convert);

    public static IEnumerable<T> CheckListTitle<T>(string title, IEnumerable<T> applications, IEnumerable<T> selected, Func<T, string> convert)
        => Instance.CheckListTitle<T>(title, applications, selected, convert);


    public static string GetDirectoryName(string path) => Path.GetDirectoryName(path);
    public static string GetProcessName() => Process.GetCurrentProcess().ProcessName;
    public static string Combine(params string[] path) => System.IO.Path.Combine(path);
    public static object GetApp() => Instance;
    public static void WriteYellow(params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        WriteLine(args);
        Console.ForegroundColor = ConsoleColor.White;

    }
    public static void WriteLine(params object[] args)
    {
        foreach (var arg in args)
        {
            if (arg is IEnumerable && !(arg is String))
            {
                foreach (var arg2 in ((IEnumerable)arg))
                {
                    WriteLine(arg2);
                }

            }
            else
            {
                Console.WriteLine(arg);
            }

        }
    }
    public static string InputFile(string message, Func<object, List<string>> validate, ref string[] args) {
        string dir = System.IO.Directory.GetCurrentDirectory();
        dir = dir.EndsWith(Path.DirectorySeparatorChar) ? dir.Substring(0, dir.Length - 1) : dir;
        do
        {
            
            var options = new List<string>() { ".." };
            var dirs = System.IO.Directory.GetDirectories(dir);
            var files = System.IO.Directory.GetFiles(dir);
            options = options.Concat(dirs.Select(d => d.Substring(d.LastIndexOf(Path.DirectorySeparatorChar) + 1))).ToList();
            options = options.Concat(files.Select(d => d.Substring(d.LastIndexOf(Path.DirectorySeparatorChar) + 1))).ToList();
            string selected = SingleSelect(message, options, ref args);
            if(selected == "..")
            {
                dir = dir.Substring(0, dir.LastIndexOf(Path.DirectorySeparatorChar));
            }
            else
            {
                string path = Path.Combine(dir, selected);
                if (System.IO.File.Exists(path))
                    return path;
                else dir = path;
            }
            
        } while (true);
    }
    public static bool ConfirmContinue(string message, Func<object, List<string>> validate, ref string[] args) {
        
        if(InputBool(message,null, ref args))
        {
            validate(args);
        }
        return true;
    }
    public static bool ConfirmContinue(ref string[] args, string message = "")
    {

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Info(message);


        Console.WriteLine("\nДля продолжения нажмите ENTER....");
        Console.ResetColor();
        if (UserInteractive)
        {
            bool result = String.IsNullOrWhiteSpace(Console.ReadLine());
            Clear();
            return result;
        }
        else
        {
            if (args.Length == 0)
            {
                if (ConsoleProgram.DebugMode)
                {
                    bool result = String.IsNullOrWhiteSpace("");
                    Clear();
                    return result;
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

    public static int ConfirmContinue(string title, Func<object, bool> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<string> MultiSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputCreditCard(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputColor(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputCurrency(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputFilePath(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputEngWord(string title, Func<object, List<string>> value, ref string[] args)
    {
        return InputString(title, inval =>
        {
            var res = value(inval);
            if ( inval == null || inval.ToString().IsEng() == false)
            {
                res.Add("Исп. латиницу");
            }
            return res;

        }, ref args);
    }
 
    public static string InputRusWord(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputPassword(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputPhone(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputXml(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputYear(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputMonth(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputUrl(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputEmail(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputName(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputFloat(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputImage(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputDecimal(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputTime(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputText(string title, Func<object, List<string>> validate, ref string[] args)
    {
 
        string value = null;
        List<string> errors = null;
        do
        {
            Console.Write(title+" > ");
            value = Console.ReadLine();
            
            errors = validate!=null?validate((object)value):null;
            if(errors != null)
            {
                foreach(var error in errors)
                    Console.WriteLine(error);
            }            
        } while (errors != null && errors.Count() != 0);
        return value;
    }

    public static string InputWeek(string title, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static string InputDate(string title, Func<object, List<string>> value, ref string[] args)
        => InputText(title, inputed =>
        {
            List<string> result = null;
            if (inputed != null && inputed.ToString().IsDate())
                result = new List<string>();
            else result = new List<string>() { "Не является датой (исп. формат dd.mm.yyyy)" };
            return value(inputed).Concat(result).ToList();
        }, ref args);
    

    public static string InputIcon(string title, Func<object, List<string>> value, ref string[] args)    
        => InputText(title, inputed =>
           {
               List<string> result = null;
               if (inputed != null && inputed.ToString().IsDate())
                   result = new List<string>();
               else result = new List<string>() { "Не является датой (исп. формат dd.mm.yyyy)" };
               return result.Concat(value(inputed)).ToList();
           },ref args);
    

    public static string InputDirectory(string title, Func<object, List<string>> value, ref string[] args)
        => SelectDirectory(ref args);

    public static TModel Input<TModel>(string title, Func<object, List<string>> value, ref string[] args)
    {
        var result = (TModel)typeof(TModel).New();
        Dictionary<string, List<string>> validationResults = null;
        do
        {
            Clear();
            string typeLabel = typeof(TModel).GetLabel();
            string desc = typeof(TModel).GetDescription();
            Get().Info(typeLabel);
            Get().Info(desc);

            var type = typeof(TModel);
            foreach (string propertyName in type.GetInputProperties())
            {
                try
                {
                    var propertyType = type.GetProperty(propertyName).PropertyType;
                    if (ReflectionService.IsPrimitive(propertyType) == false)
                    {
                        if (propertyType.IsCollectionType())
                        {
                            result.SetValue(propertyName, InputCollection(propertyName, Typing.ParseCollectionType(propertyType), null, ref args));
                        }
                        else
                        {
                            result.SetValue(propertyName, Input(propertyType.New()));
                        }


                    }
                    else
                    {
                        string inputType = type.GetPropertyInputType(propertyName);
                        string label = type.GetPropertyLabel(propertyName);
                        switch (inputType)
                        {
                            case "Time": result.SetValue(propertyName, InputTime(label, value => new List<string>(), ref args)); break;
                            case "DateTime": result.SetValue(propertyName, InputDateTime(label, value => new List<string>(), ref args)); break;
                            case "PostalCode": result.SetValue(label, InputPostalCode(label, value => new List<string>(), ref args)); break;
                            case "PrimitiveCollection":
                            case "StructureCollection":
                            
                                Copy(result, InputCollection
                                (
                                    label,
                                    Typing.ParseCollectionType(propertyType),
                                    null,
                                    ref args
                                ));
                                return result;
                                            
                            case "Select": result.SetValue(label,  SingleSelect("",new string[]{ $"{value}" },ref args)); break;
                            case "Xml": result.SetValue(label, InputXml(label, value => new List<string>(), ref args)); break;
                            case "Image": result.SetValue(label, InputImage(label, value => new List<string>(), ref args)); break;
                            case "EngWord": result.SetValue(label, InputEngWord(label, value => new List<string>(), ref args)); break;
                            case "Duration": result.SetValue(label, InputDuration(label, value => new List<string>(), ref args)); break;
                            case "Custom": result.SetValue(label, InputCustom(label, value => new List<string>(), ref args)); break;
                            case "Number": result.SetValue(label, InputNumber(label, value => new List<string>(), ref args)); break;
                            case "Percent": result.SetValue(label, InputPercent(label, value => new List<string>(), ref args)); break;
                            case "Text": result.SetValue(label, InputText(label, value => new List<string>(), ref args)); break;
                            case "MultilineText": result.SetValue(label, InputMultilineText(label, value => new List<string>(), ref args)); break;
                            case "Year": result.SetValue(label, InputYear(label, value => new List<string>(), ref args)); break;
                            case "Week": result.SetValue(label, InputWeek(label, value => new List<string>(), ref args)); break;
                            case "Month": result.SetValue(label, InputMonth(label, value => new List<string>(), ref args)); break;
                            case "Url": result.SetValue(label, InputUrl(label, value => new List<string>(), ref args)); break;
                            case "Phone": result.SetValue(label, InputPhone(label, value => new List<string>(), ref args)); break;
                            case "Password": result.SetValue(label, InputPassword(label, value => new List<string>(), ref args)); break;
                            case "File": result.SetValue(label, InputFile(label, value => new List<string>(), ref args)); break;
                            case "Email": result.SetValue(label, InputEmail(label, value => new List<string>(), ref args)); break;
                            case "Color": result.SetValue(label, InputColor(label, value => new List<string>(), ref args)); break;
                            case "Icon": result.SetValue(label, InputIcon(label, value => new List<string>(), ref args)); break;
                            case "Currency": result.SetValue(label, InputCurrency(label, value => new List<string>(), ref args)); break;
                            default: throw new NotSupportedException($"Тип ввода [{inputType}] не поддерживается");
                        }
                    }

                }
                catch (Exception ex)
                {
                    InputConsole.Error(ex);
                }

            }
            validationResults = result.Validate();
        } while (validationResults.Count()!=0);
        return result;
    }

    private static object InputCollection(string propertyName, string p, Func<object, List<string>> value, ref string[] args)
    {
        return InputStructureCollection(propertyName, value, ref args);
    }

    private static object InputPostalCode(string propertyName, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    private static object InputDateTime(string propertyName, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    private static object InputDuration(string propertyName, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    private static object InputCustom(string propertyName, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    private static object InputMultilineText(string propertyName, Func<object, List<string>> value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    private static void Copy<TModel>(TModel result, object v)
    {
        throw new NotImplementedException();
    }

  
    public static object Input(object pObject)
    {
        Clear();
        var result = pObject;

        var objectType = pObject.GetType();


        foreach (string name in objectType.GetInputProperties())
        {



            var ptype = objectType.GetProperty(name).PropertyType;
            if (ptype.IsPrimitive)
            {

            }
            else if (ptype.IsCollectionType())
            {

            }
            else
            {

            }
        }
        return result;
    }
    public static List<Dictionary<string, object>> InputPrimitiveCollection(string title, Func<object, List<string>> value, ref string[] args)
    {
        var builder = new CollectionBuilder();
        string selected = null;

        Clear();
        Get().Info("Коллекция: ");
        Get().Info(builder.GetAll().ToJsonOnScreen());
        switch (selected = InputConsole.SingleSelect
        (
                "",
                new string[] {
                    "Добавить",
                    "Удалить",
                    "Сохранить"
                }, ref args))
        {
            case "Добавить":
                builder.OnCreateItem(title, value, ref args);
                break;
            case "Удалить":
                builder.OnRemoveItem();
                break;

            case "Сохранить": return builder.Build();
            default: throw new Exception("Нет обработчика дял выбранного значнеия " + selected);
        }
        return builder.Build();
    }

    public static List<Dictionary<string,object>> InputStructureCollection(string title, Func<object, List<string>> value, ref string[] args)
    {
        var builder = new CollectionBuilder();
        string selected = null;

        Clear();
        Get().Info("Коллекция: ");
        Get().Info(builder.GetAll().ToJsonOnScreen());
        switch (selected = InputConsole.SingleSelect
        (
                "",
                new string[] {
                    "Добавить",
                    "Удалить",
                    "Сохранить"
                }, ref args))
        {
            case "Добавить":
                builder.OnCreateItem(title,value,ref args);
                break;
            case "Удалить":
                builder.OnRemoveItem();
                break;

            case "Сохранить": return builder.Build();
            default: throw new Exception("Нет обработчика дял выбранного значнеия " + selected);
        }
        return builder.Build();
    }

    public static bool InputBool(string title, Func<object, List<string>> value, ref string[] args)
        =>( (SingleSelect(title, new string[] { "да", "нет" }, ref args) == "да") ? true : false);
    






    public static string InputString(string title, Func<object, List<string>> validate, ref string[] args)
        => InputText(title, validate, ref args);






    public static int InputPercent(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static int InputInt(string title, Func<object, List<string>> validate, ref string[] args)
        => InputNumber(title, val => val != null && val.ToString().IsInt() && val.ToString().ToInt() > 0 ? validate(val) : new List<string>(), ref args);


    public static int InputPositiveNumber(string title, Func<object, List<string>> validate, ref string[] args)
        => InputInt(title, val => val != null && val.ToString().IsInt() && val.ToString().ToInt() > 0 ? validate(val) : new List<string>(), ref args);
        
    

    public static string SelectDirectory(ref string[] args)
    {
        var location = "D:\\";
        do
        {
            location = location.Length==2 ? location+Path.DirectorySeparatorChar : location;
            string selected = (location.Length > 3) ?
            SingleSelect("",
                "..".Split(',').ToList().Concat(
                System.IO.Directory.GetDirectories(location).ToList()),
                ref args):
            SingleSelect("",              
                System.IO.Directory.GetDirectories(location).ToList() ,
                ref args);
            if (selected == "..")
                location = location.Substring(0, location.LastIndexOf(Path.DirectorySeparatorChar));
            else location = Path.Combine(location, selected);
            if (Confirm("Выбрать " + location + "?"))
                break;
        } while (true);
        return location;
    }

    public static string InputString(string name, Func<string, IEnumerable<string>> validate, ref string[] args)
    {
        bool isValid = false;
        string value = null;
        do
        {
            Console.Write(name);
            Console.Write(">");
            value = Console.ReadLine();
            var errors = validate(value);
            foreach (var error in errors)
                typeof(InputConsole).Error(error);
            ConfirmContinue("Повторить попытку");
            isValid = errors.Count() == 0;
        } while (isValid);
        return value;
    }

    internal static object InputType(Type step)
    {
        return typeof(InputConsole).GetMethod("Input").MakeGenericMethod(step).Invoke(null, new object[0]);
    }

    public static object InputChar(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputDateTime(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputSmallMoney(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputStructured(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputUdt(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputVariant(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputVarBinary(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputTimestamp(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputTinyInt(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputDateTimeOffset(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputUniqueIdentifier(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputSmallDateTime(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputSmallInt(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputDateTime2(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputReal(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputNchar(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public static object InputMoney(string v, object value, ref string[] args)
    {
        throw new NotImplementedException();
    }

    
    public static void CreateTreeModel(string title, ref string[] args)
    {
        bool ready = false;

        
        static void PrintNode(ITypeNode<BaseEntity> pnode, int level) 
        {
            int next = level + 1;
            while (level > 0)
            {
                Console.Write("    ");
                level = level - 1;
            }
            pnode.WriteLine(pnode.NodeName);
            foreach (var pchild in pnode.GetChildren())
            {
                PrintNode(pchild, level + 1);
            }
        }

        InputConsole.Clear();
        var root = new TypeNode<BaseEntity>();
        TypeNode<BaseEntity> selected = root;
        root.NodeName = InputConsole.InputString("Корень Наименование", null, ref args);
        do
        {
            Clear();
            root.Info("Редактор иерархии");
            PrintNode(root, 0);


        }while (!ready);
    }

    public static ILogger<T> GetLogger<T>()
        => LoggerFactory.Create(options => options.AddConsole()).CreateLogger<T>();


    public string SelectFile(string title, Func<string, List<string>> validate, ref string[] args)
    {
        if (ProgramDialog.UserInteractive == false)
        {
            return Shift(ref args);
        }
        else
        {
            // возвращает вписок для выбора для заданной директории
            Func<string, IEnumerable<string>> nav = (dir) =>
                new List<string>() { ".." }
                    .Concat(System.IO.Directory.GetFiles(dir))
                    .Concat(System.IO.Directory.GetFiles(dir));
            string location = System.IO.Directory.GetCurrentDirectory();
            IEnumerable<string> navs = nav(location);
            //int cursor = 0;



            ConsoleProgram.ReadKeyRepeat<string>(
                null,null,null,null
                
                /*() => {

                    //вывод списка
                    Console.WriteLine("\n" + "\n  " + title + "\n");
                    navs = nav(location);
                    int ctn = 0;
                    foreach (string item in navs)
                        
                        if (ctn++ == cursor)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($" => {item}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"    {item}");
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
                    return result = items.ToList()[cursor];
                },

                // onCanceled
                () => {
                }*/
            );


        }
        return null;
        /*var result = new List<T>();
        Action complete = () => { throw new CompleteException(); };
        var items = applications
                    .Select(Item => new Dictionary<string, object> {
                        { "Item",Item},
                        { "Selected",false},
                        { "Label",convert(Item)}
                    }).ToList();
        
        return result;*/
    }
} 

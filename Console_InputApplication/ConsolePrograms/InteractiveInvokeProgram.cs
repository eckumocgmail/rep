
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static InputConsole;
using static ConsoleProgram;
using static InputConsole;
using Console_InputApplication;
using System.Diagnostics.CodeAnalysis;

[Label("Программа выполнения действия объекта")]
public static class InteractiveInvokeProgram
{
    public static object UserInvokeInteractive(this object instance)
    {
        Interactive();
        Console.Clear();
        PrintData(instance);
        var action = SelectAction(instance);
        Console.Clear();
        var parameters = CreateParameters(action);
        return action.Invoke(instance, parameters.Values.ToArray());


    }

    public static object Input(this object instance, ref string[] args)
    {   
        Func<object, List<string>> validation = (formData) =>
        {
            var results = new List<string>();
            foreach (var kv in formData.Validate())
            {
                results.AddRange(kv.Value);
            }
            return results;
        };
        var invokation = typeof(InputConsole).GetMethods()
            .Where(m => m.GetGenericArguments().Count() == 1 && m.Name == "Input")
            .First().MakeGenericMethod(instance.GetType());
        var result = invokation.Invoke(null,new object[] {
            "Редактирование "+instance.GetTypeName(),
            null,null
        });
        return result;


    }
    public static object EditProperties(this object instance, ref string[] args)
    {
        Interactive();
        Console.Clear();
        PrintData(instance);
        Clear();
        var type = instance.GetType();
        Dictionary<string, object> FormData = new Dictionary<string, object>();
        foreach (string property in instance.GetInputProperties())
        {
            var value = CreateParameters(type.GetProperty(property).SetMethod);

            FormData[property] = value!=null && value.ContainsKey("Value")? value["Value"] : null;
            instance.SetProperty(property, ((Dictionary<string, object>)FormData[property])["Value"]);
        }
        return instance;
    }
    private static Dictionary<string, object> CreateParameters([NotNull][NotNullNotEmpty]MethodInfo ProgramAction)
    {
        Clear();
        MethodBase.GetCurrentMethod().EnsureIsValid(new object[] { ProgramAction });
       

        WriteLine($"\n {(ProgramAction != null ? ProgramAction.Name : "")}");
        int n = 1;
        var args = new List<object>();
        Dictionary<string, object> ProgramArguments = new Dictionary<string, object>();
        foreach (ParameterInfo par in ProgramAction.GetParameters())
        {
            WriteLine($"{n++}) {par.Name}: {par.ParameterType.Name}>");
            if (par.ParameterType.IsEnum == false)
            {
                object result = TextDataSetter.ToType(ReadLine(), par.ParameterType);
                ProgramArguments[par.Name] = result;
                args.Add(result);
            }
            else
            {
                string option = InputConsole.SelectOption(Enum.GetNames(par.ParameterType));
                object result = null;
                Enum.TryParse(par.ParameterType, option, true, out result);
                ProgramArguments[par.Name] = result;
                args.Add(result);
            }
        }
        return ProgramArguments; 
    }



    

    private static MethodInfo SelectAction(object instance)
    {
        string[] args = new string[0];
        try
        {
            var action  = SingleSelect( instance.GetOwnPropertyNames());
            var selected = instance.GetType().GetMethods().Where(m => m.Name == action).FirstOrDefault();
            if (selected != null)
                WriteLine(selected.GetType().GetMethodLabel(selected.Name));
            return selected;
        }
        catch (CancelException)
        {
            throw;

        }
        catch (Exception ex)
        {
            WriteLine(ex);
            throw new Exception("Не удалось выполнить выбор операции",ex);
        }
    }

   
    private static List<string> GetActionNames(object instance)
    {
        return instance.GetType().GetOwnMethodNames();
    }

    private static void PrintData(object instance)
    {         
        Clear();
        if (instance == null)
            throw new Exception("Нужно присвоить значение свойству ProgramData");
        Type ProgramType = instance.GetType();

        Info($"[{TypeExtensions2.GetTypeName(ProgramType)}] Свойства: ");
        ProgramType.GetInputProperties().ToList().ForEach(prop => {
        var property = instance.GetType().GetProperty(prop);
            if (property.GetType().IsEnum)
            {

                WriteLine($"{ prop }: {property.GetValue(instance).ToJsonOnScreen() }");
            }
            else
            {
                if (property.GetType().IsPrimitive == true)
                {
                    WriteLine($"{ prop }: {property.GetValue(instance) }");
                }
                else
                {
                    var val = property.GetValue(instance);
                    string text = val == null ? "null" : val.ToJsonOnScreen();
                    WriteLine($"{ prop }: \n{ text}");
                }
            }

        }); 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public abstract class ProgramBase : IUserControl
{
    private IUserControl Control { get; set; }



    public virtual void Run(ref string[] args)
    {
        //ProgramHistory.TraceHistory();
        var typeName = Assembly.GetCallingAssembly().GetClassTypes().Select(t => t.GetTypeName()).SingleSelect("Выберите тип контроллера", ref args);        
        ConsoleProgram.RunInteractive(typeName.ToType());
    }

    public IEnumerable<string> CheckList(string title, IEnumerable<string> options, ref string[] args)
    {
        return Control.CheckList(title, options, ref args);
    }

    public bool ConfirmContinue(string title)
    {
        return Control.ConfirmContinue(title);
    }

    public TModel Input<TModel>(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.Input<TModel>(title, validate, ref args);
    }

    public bool InputBool(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputBool(title, validate, ref args);
    }

    public string InputColor(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputColor(title, validate, ref args);
    }

    public string InputCreditCard(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputCreditCard(title, validate, ref args);
    }

    public string InputCurrency(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputCurrency(title, validate, ref args);
    }

    public string InputDate(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputDate(title, validate, ref args);
    }

    public string InputDecimal(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputDecimal(title, validate, ref args);
    }

    public string InputDirectory(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputDirectory(title, validate, ref args);
    }

    public string InputEmail(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputEmail(title, validate, ref args);
    }

    public string InputFile(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputFile(title, validate, ref args);
    }

    public string InputFilePath(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputFilePath(title, validate, ref args);
    }

    public string InputIcon(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputIcon(title, validate, ref args);
    }

    public string InputImage(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputImage(title, validate, ref args);
    }

    public int InputInt(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputInt(title, validate, ref args);
    }

    public string InputMonth(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputMonth(title, validate, ref args);
    }

    public string InputName(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputName(title, validate, ref args);
    }

    public int InputNumber(string title, Func<int, List<string>> validate, ref string[] args)
    {
        return Control.InputNumber(title, validate, ref args);
    }

    public string InputPassword(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputPassword(title, validate, ref args);
    }

    public int InputPercent(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputPercent(title, validate, ref args);
    }

    public string InputPhone(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputPhone(title, validate, ref args);
    }

    public int InputPositiveNumber(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputPositiveNumber(title, validate, ref args);
    }

    public List<Dictionary<string, object>> InputPrimitiveCollection(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputPrimitiveCollection(title, validate, ref args);
    }

    public string InputRusWord(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputRusWord(title, validate, ref args);
    }

    public string InputString(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputString(title, validate, ref args);
    }

    public List<Dictionary<string, object>> InputStructureCollection(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputStructureCollection(title, validate, ref args);
    }

    public string InputText(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputText(title, validate, ref args);
    }

    public string InputTime(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputTime(title, validate, ref args);
    }

    public string InputUrl(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputUrl(title, validate, ref args);
    }

    public string InputWeek(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputWeek(title, validate, ref args);
    }

    public string InputXml(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputXml(title, validate, ref args);
    }

    public string InputYear(string title, Func<object, List<string>> validate, ref string[] args)
    {
        return Control.InputYear(title, validate, ref args);
    }

    public IEnumerable<string> MultiSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        return Control.MultiSelect(title, options, ref args);
    }

    public string SingleSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        return Control.SingleSelect(title, options, ref args);
    }

    public Task<IEnumerable<string>> CheckListAsync(string title, IEnumerable<string> options, ref string[] args)
    {
        throw new NotImplementedException();
    }
}
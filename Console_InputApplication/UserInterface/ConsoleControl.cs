 using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Label("Интерфейс управления через консоль")]
public class ConsoleControl : IUserControl
{
    public bool ConfirmContinue(string title)
    => InputConsole.ConfirmContinue(title);

    public IEnumerable<string> CheckList(string title, IEnumerable<string> options, ref string[] args)
    => InputConsole.CheckList(title, options, ref args);

    public IEnumerable<string> MultiSelect(string title, IEnumerable<string> options, ref string[] args)
    => InputConsole.MultiSelect(title, options, ref args);

    public string SingleSelect(string title, IEnumerable<string> options, ref string[] args)
    => InputConsole.SingleSelect(title, options, ref args);

    public string InputCreditCard(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputCreditCard(title, validate, ref args);

    public string InputCurrency(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputCurrency(title, validate, ref args);

    public string InputColor(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputColor(title, validate, ref args);

    public string InputDirectory(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputDirectory(title, validate, ref args);

    public TModel Input<TModel>(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.Input<TModel>(title, validate, ref args);

    public List<Dictionary<string, object>> InputPrimitiveCollection(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputPrimitiveCollection(title, validate, ref args);

    public List<Dictionary<string,object>> InputStructureCollection(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputStructureCollection(title, validate, ref args);

    public bool InputBool(string title, Func<object, List<string>> validate, ref string[] args)
     => InputConsole.InputBool(title, validate, ref args);

    public string InputFile(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputFile(title, validate, ref args);

    public string InputFilePath(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputFilePath(title, validate, ref args);

    public string InputImage(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputEngWord(title, validate, ref args);

    public string InputIcon(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputIcon(title, validate, ref args);

    public string InputRusWord(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputRusWord(title, validate, ref args);

    public string InputPassword(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputPassword(title, validate, ref args);


    public string InputPhone(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputPhone(title, validate, ref args);

    public string InputXml(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputXml(title, validate, ref args);

    public string InputDate(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputDate(title, validate, ref args);

    public string InputWeek(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputWeek(title, validate, ref args);

    public string InputString(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputString(title, validate, ref args);

    public string InputText(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputText(title, validate, ref args);

    public string InputYear(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputYear(title, validate, ref args);

    public string InputMonth(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputMonth(title, validate, ref args);

    public string InputTime(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputTime(title, validate, ref args);

    public string InputEmail(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputEmail(title, validate, ref args);

    public string InputUrl(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputUrl(title, validate, ref args);

    public string InputName(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputName(title, validate, ref args);

    public int InputNumber(string title, Func<int, List<string>> validate, ref string[] args)
    => InputConsole.InputNumber(title, validate, ref args);

    public string InputDecimal(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputDecimal(title, validate, ref args);

    public int InputPercent(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputPercent(title, validate, ref args);

    public int InputInt(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputInt(title, validate, ref args);

    public int InputPositiveNumber(string title, Func<object, List<string>> validate, ref string[] args)
    => InputConsole.InputPositiveNumber(title, validate, ref args);

    public Task<IEnumerable<string>> CheckListAsync(string title, IEnumerable<string> options, ref string[] args)
    {
        throw new NotImplementedException();
    }
}

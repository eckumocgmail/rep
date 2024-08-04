

using System.Collections.Generic;
using System;

[Label("Примитивные интерфейс ввода")]
public interface IUserControl
{
    IEnumerable<string> CheckList(string title, IEnumerable<string> options, ref string[] args);
    bool ConfirmContinue(string title);
    TModel Input<TModel>(string title, Func<object, List<string>> validate, ref string[] args);
    bool InputBool(string title, Func<object, List<string>> validate, ref string[] args);
    string InputColor(string title, Func<object, List<string>> validate, ref string[] args);
    string InputCreditCard(string title, Func<object, List<string>> validate, ref string[] args);
    string InputCurrency(string title, Func<object, List<string>> validate, ref string[] args);
    string InputDate(string title, Func<object, List<string>> validate, ref string[] args);
    string InputDecimal(string title, Func<object, List<string>> validate, ref string[] args);
    string InputDirectory(string title, Func<object, List<string>> validate, ref string[] args);
    string InputEmail(string title, Func<object, List<string>> validate, ref string[] args);
    string InputFile(string title, Func<object, List<string>> validate, ref string[] args);
    string InputFilePath(string title, Func<object, List<string>> validate, ref string[] args);
    string InputIcon(string title, Func<object, List<string>> validate, ref string[] args);
    string InputImage(string title, Func<object, List<string>> validate, ref string[] args);
    int InputInt(string title, Func<object, List<string>> validate, ref string[] args);
    string InputMonth(string title, Func<object, List<string>> validate, ref string[] args);
    string InputName(string title, Func<object, List<string>> validate, ref string[] args);
    int InputNumber(string title, Func<int, List<string>> validate, ref string[] args);
    string InputPassword(string title, Func<object, List<string>> validate, ref string[] args);
    int InputPercent(string title, Func<object, List<string>> validate, ref string[] args);
    string InputPhone(string title, Func<object, List<string>> validate, ref string[] args);
    int InputPositiveNumber(string title, Func<object, List<string>> validate, ref string[] args);
    List<Dictionary<string, object>> InputPrimitiveCollection(string title, Func<object, List<string>> validate, ref string[] args);
    string InputRusWord(string title, Func<object, List<string>> validate, ref string[] args);
    string InputString(string title, Func<object, List<string>> validate, ref string[] args);
    List<Dictionary<string, object>> InputStructureCollection(string title, Func<object, List<string>> validate, ref string[] args);
    string InputText(string title, Func<object, List<string>> validate, ref string[] args);
    string InputTime(string title, Func<object, List<string>> validate, ref string[] args);
    string InputUrl(string title, Func<object, List<string>> validate, ref string[] args);
    string InputWeek(string title, Func<object, List<string>> validate, ref string[] args);
    string InputXml(string title, Func<object, List<string>> validate, ref string[] args);
    string InputYear(string title, Func<object, List<string>> validate, ref string[] args);
    IEnumerable<string> MultiSelect(string title, IEnumerable<string> options, ref string[] args);
    string SingleSelect(string title, IEnumerable<string> options, ref string[] args);

}
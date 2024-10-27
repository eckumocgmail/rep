using Blazored.Modal;
using Blazored.Modal.Services;

using Console_UserInterface.Shared;
using Console_UserInterface.Shared.Controls;
using Console_UserInterface.Shared.Deps;
using Console_UserInterface.Shared.Dialogs;
using Console_UserInterface.Shared.Forms;

using Microsoft.AspNetCore.Components;

/// <summary>
/// Сервис ввода ифномарциив систему
/// </summary>
public interface IInputService: IUserControl
{
    public string CompleteDialog(object result, string key);
    public TModel CreateInputDialog<TModel>() where TModel: class;
    public TModel CreateInputDialog<TModel>(TModel dataref) where TModel : class;
    public TModel CreateExecuteDialog<TModel>(string controller, string action, Action<TModel> oncompleted, Action<TModel> oncanceled, Action<Exception> onerror) where TModel : class;
}


/// <summary>
/// Реализация сервиса ввода
/// </summary>
public class InputService: IInputService
{
    private readonly AsyncContext asyncContext;
    private readonly NavigationManager nav;
    private readonly IInputModalService _input;
    private readonly IModalService _modal;
     
    public InputService(AsyncContext asyncContext, NavigationManager nav, IModalService modal, IInputModalService input)
    {
        this.asyncContext = asyncContext;
        this.nav = nav;
        this._input = input;
        this._modal = modal;
    }
    public string CompleteDialog(object result, string key)
    {
        Action<object> todo = this.asyncContext.Take(key);
        if(todo is not null)
        {
            todo(result);
        }            
        return key;
    }

    public IEnumerable<string> MultiSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string SingleSelect(string title, IEnumerable<string> options, ref string[] args)
    {
        return SingleSelectAsync(title, options).Result;    
    }
    public async Task<string> SingleSelectAsync(string title, IEnumerable<string> options)
    {
        string SelectedItem = null;
        IModalReference modalRef = null;
        var parmeters = new ModalParameters();
        parmeters.Add("Parameters", new Dictionary<string, object>() {
            { "Title", title  },
            { "ListItems", options.ToList() },
            { "OnSelect", (object evt)=>{
                SelectedItem = ((SelectList)evt).SelectedItem;
            } }
        });
        parmeters.Add("OnCompleted", (object evt) =>
        {
            SelectedItem = ((SelectList)evt).SelectedItem;
            modalRef.Close();
        });
        parmeters.Add("OnCanceled", (object evt) =>
        {
            modalRef.Close();
        });
        modalRef = this._modal.Show<SubmitForm<SelectList>>("Выберите из списка", parmeters, new ModalOptions()
        {
            ContentScrollable = true
        });
        var result = await modalRef.Result;
        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
            return null;
        }
        else
        {
            return SelectedItem;
        }
    }

    public Task<IEnumerable<string>> CheckListAsync(string title, IEnumerable<string> options, ref string[] args)
    {
        
        return Task.Run(() => {
            var result = this.CheckListAsync(title, options).Result;
            //TODO ARGS
            return result;
        });
    }

    public async Task<IEnumerable<string>> CheckListAsync(string title, IEnumerable<string> options)
    {
        IModalReference modalRef = null;
        var parmeters = new ModalParameters();
        parmeters.Add("Parameters", new Dictionary<string, object>() {
            { "Title", title  },
            { "ListItems", options.ToList() }
        });
        List<string> CheckedItems = null;
        parmeters.Add("OnCompleted", (object evt) =>
        {
            CheckedItems = ((CheckList)evt).CheckedItems;
            modalRef.Close();
        });
        parmeters.Add("OnCanceled", (object evt) =>
        {
            modalRef.Close();
        });
        modalRef = this._modal.Show<SubmitForm<CheckList>>("Укажите необходимые элементы", parmeters, new ModalOptions()
        {
            ContentScrollable = true
        });
        var result = await modalRef.Result;
        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
            return null;
        }
        else
        {
            return CheckedItems;
        }
    }

    public IEnumerable<string> CheckList(string title, IEnumerable<string> options, ref string[] args)
    {
        return CheckListAsync(title, options, ref args).Result;
    }

    private async Task<List<string>> OpenCheckListDialog(string title, IEnumerable<string> options, string key)
    {
        var checkList = await this._input.Show<CheckList>(new Dictionary<string, object>()
        {
            { "title", title },
            { "options", options } 
        });
        return checkList.CheckedItems;
    }

    public bool? ConfirmContinue(string title)
    {
        string SelectedItem = null;
        IModalReference modalRef = null;
        var parmeters = new ModalParameters();
        parmeters.Add("Parameters", new Dictionary<string, object>() {                           
        });
        bool res = false;
        parmeters.Add("OnCompleted", (object evt) =>
        {
            res = true;
            modalRef.Close();
        });
        parmeters.Add("OnCanceled", (object evt) =>
        {
            res = false;
            modalRef.Close();
        });
        modalRef = this._modal.Show<SubmitForm<TextComponent>>("Выберите из списка", parmeters, new ModalOptions()
        {
            ContentScrollable = true
        });
        var result = modalRef.Result.Result;
        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
            return null;
        }
        else
        {
            return res;
        }
    }

    public TModel Input<TModel>(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public bool InputBool(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputColor(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputCreditCard(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputCurrency(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputDate(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputDecimal(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputDirectory(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputEmail(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputFile(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputFilePath(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputIcon(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputImage(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public int InputInt(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputMonth(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputName(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public int InputNumber(string title, Func<int, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputPassword(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public int InputPercent(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputPhone(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public int InputPositiveNumber(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public List<Dictionary<string, object>> InputPrimitiveCollection(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputRusWord(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputString(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public List<Dictionary<string, object>> InputStructureCollection(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputText(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputTime(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputUrl(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputWeek(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public string InputXml(string title, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }


    public class InputYearModel
    {
        [Label("Год")]
        [InputYear]
        [NotNullNotEmpty]
        public string Value { get; set; } = "2000";
    }
    public string InputYear(string title, Func<object, List<string>> validate, ref string[] args)
    {
        var item = new InputYearModel();

        string SelectedItem = null;
        IModalReference modalRef = null;
        var parmeters = new ModalParameters();
        parmeters.Add("Model", item);

       
        parmeters.Add("OnCompleted", (object evt) =>
        {
            modalRef.Close();
        });
        parmeters.Add("OnCanceled", (object evt) =>
        {
            modalRef.Close();
        });
        modalRef = this._modal.Show<FormEditor>("", parmeters, new ModalOptions()
        {
            ContentScrollable = true
        });
        var result = modalRef.Result.Result;
        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
            return null;
        }
        else
        {
            return "";
        }
    }

    public TModel CreateInputDialog<TModel>() where TModel : class
    {
        return this._input.Create<TModel>().Result;
    }

    public TModel CreateInputDialog<TModel>(TModel dataref) where TModel : class
    {
        return this._input.Create<TModel>(dataref ).Result;
    }

    public TModel CreateExecuteDialog<TModel>(string controller, string action, Action<TModel> oncompleted, Action<TModel> oncanceled, Action<Exception> onerror) where TModel : class
    {
        throw new NotImplementedException();
    }

    bool IUserControl.ConfirmContinue(string title)
    {
        throw new NotImplementedException();
    }
}
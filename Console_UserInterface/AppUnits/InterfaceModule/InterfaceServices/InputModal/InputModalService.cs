using Blazored.Modal;
using Blazored.Modal.Services;
using Console_UserInterface.Attributes;
using Console_UserInterface.Bootstrap;
using Console_UserInterface.Shared.Dialogs;
using Console_UserInterface.Shared.Forms;
using Microsoft.AspNetCore.Components;

public class InputModalService : IInputModalService
{     
    private readonly IModalService _modalService;

    public InputModalService( IModalService modalService)
    {     
        _modalService = modalService;
    }
    public async Task<T> Create<T>(T model) where T : class
    {
        var parmeters = new ModalParameters();
        parmeters.Add("TypeName", typeof(T).Name);
        parmeters.Add("TargetInstance", model);
        
        var modalRef = _modalService.Show<InputModal>("Редактирование", parmeters, new ModalOptions()
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
            return (T)result.Data;
        }
    }

    public async Task<T> Create<T>() where T: class
    {          
        var parmeters = new ModalParameters();
        parmeters.Add("TypeName", typeof(T).Name);
     
        var modalRef = _modalService.Show<InputModal>("Редактирование", parmeters, new ModalOptions()
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
            return (T)result.Data;
        }
            
    }

    public IModalReference Create(Type type)
    {
        var parmeters = new ModalParameters();
        parmeters.Add("Model", new InputFormModel(type.New()));
    
        
        return _modalService.Show<InputModal>("Редактирование", parmeters, new ModalOptions()
        {
            ContentScrollable = true,
        });
    }

    public T Edit<T>(T target)  
    {
        var model = new InputFormModel(target);
        var parmeters = new ModalParameters();
        parmeters.Add("Model", model);
        
        
        var modalRef = _modalService.Show<InputModal>("Редактирование",parmeters, new ModalOptions()
        {
            ContentScrollable = true,
        });
        return (T)modalRef.Result.Result.Data;
    }

    public async Task<T> Show<T>(string title, Dictionary<string, object> parametersMap) where T : class, IComponent
    {
        var parmeters = new ModalParameters();
        if (parametersMap is not null)
        {
            foreach (var kv in parametersMap)
            {
                parmeters.Add(kv.Key, kv.Value);
            }
        }
        var modalRef = _modalService.Show<SubmitForm<T>>(title, parmeters, new ModalOptions()
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
            return (T)result.Data;
        }

    }

    public Task<T> Show<T>(T item = null) where T : class
    {
        throw new NotImplementedException();
    }
    public class CheckListModel
    {
        [CheckListControl]
        public List<string> Checked { get; set; } = new();

    }
    public async Task<T> Show<T>(Dictionary<string, object> item = null) where T : class  
    {
        

        var parmeters = new ModalParameters();
        parmeters.Add("TypeName", typeof(T).Name);

        var modalRef = _modalService.Show<ModalComponent>("Редактирование", parmeters, new ModalOptions()
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
            return (T)result.Data;
        }
    }

    
}
 

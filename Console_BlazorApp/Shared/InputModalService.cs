
using Console_BlazorApp.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
 
public class InputModalService : IInputModalService
{
     
    private readonly IModalService _modalService;

    public InputModalService( IModalService modalService)
    {
     
        _modalService = modalService;
    }

    public async Task<T> Create<T>() where T: class
    {          
        var parmeters = new ModalParameters();
        parmeters.Add("TypeName", typeof(T).Name);
     
        var modalRef = _modalService.Show<Console_BlazorApp.Shared.InputModal>("Редактирование", parmeters, new ModalOptions()
        {
            Scrollable = true 
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

    public IModalReference<InputModal> Create(Type type)
    {
        var parmeters = new ModalParameters();
        parmeters.Add("Model", new InputFormModel(type.New()));
    
        
        return _modalService.Show<InputModal>("Редактирование", parmeters, new ModalOptions()
        {
            Scrollable = true,
        });
    }

    public T Edit<T>(T target)  
    {
        var model = new InputFormModel(target);
        var parmeters = new ModalParameters();
        parmeters.Add("Model", model);
        
        
        var modalRef = _modalService.Show<InputModal>("Редактирование",parmeters, new ModalOptions()
        {
            Scrollable = true,
        });
        return (T)modalRef.Result.Result.Data;
    }

    IModalReference<object> IInputModalService.Create(Type type)
    {
        throw new NotImplementedException();
    }
}
 

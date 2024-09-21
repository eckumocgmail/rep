using Blazored.Modal;
using Blazored.Modal.Services;

using Console_UserInterface.Shared.Forms;

using Microsoft.Extensions.DependencyInjection;
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
 
}
 

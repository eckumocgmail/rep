
using Blazored.Modal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IInputModalService
{
    /// <summary>
    /// Открывает диалоговое окно с формой для регистрации сущности T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<T> Create<T>() where T : class;
    public IModalReference Create(Type type);

    /// <summary>
    /// Отрывает диалоговое окно с формой для регистрации сущности T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Edit<T>(T target);


}


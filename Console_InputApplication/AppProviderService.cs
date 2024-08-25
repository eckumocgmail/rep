using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static InputConsole;


/// <summary>
/// Необходимо чтобы объект был Singleton
/// </summary>
public class AppProviderService: IServiceProvider   
{   
    
    private static AppProviderService _instance = null;    
    public static AppProviderService GetInstance()
    {
        if (_instance == null)
            _instance = GetSingletonInstance();
        return _instance;
    }
    public static AppProviderService GetSingletonInstance()
    {
        if (_instance == null)
        {
            lock (_instance = new AppProviderService())
            {
                _instance.AddSingletons(Assembly.GetCallingAssembly().GetClassTypes());
                _instance.AddSingletons(Assembly.GetEntryAssembly().GetClassTypes());
                _instance.AddSingletons(Assembly.GetExecutingAssembly().GetClassTypes());
            }
            
        }
        return _instance;
    }

  


    /// <summary>
    /// Функции внедрения зависимостей
    /// </summary>
    protected ConcurrentDictionary<Type, Func<IServiceProvider, object>> Factories =
        new ConcurrentDictionary<Type, Func<IServiceProvider, object>>();


    /// <param name="serviceProvider">Исп. в случае если сервис не зарегистрирован</param>
    /// <param name="names">Имена типов сервисов</param>
    public AppProviderService()  
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetNames() =>
        Factories.Keys.Select(t => TypeExtensions2.GetTypeName(t));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public virtual bool HasService(Type type) => 
        Factories.ContainsKey(type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public virtual object GetService(Type serviceType)
        => Factories.ContainsKey(serviceType) ?
            Factories[serviceType].Invoke(this) :
            throw new ArgumentException("serviceType",$"{serviceType}");

    

    
 

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Get<T>() where T : class
    {
        //this.Info("Внедрение сервиса "+ TypeExtensions2.GetTypeName(typeof(T)));
        AddSingletons(typeof(T).Assembly.GetClassTypes());
        var type = typeof(T).GetTypeName().ToType();//Assembly.GetExecutingAssembly().GetTypes().Where(t => t == typeof(T) || t.GetInterfaces().Contains(typeof(T))).FirstOrDefault();
        if (type != null)
        {
            try
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                var argsList = new List<object>();
                if(constructor == null)
                {
                    throw new Exception(TypeExtensions2.GetTypeName(type) + " не реализует конструктор");
                }
                else
                {
                    foreach (Type ptype in constructor.GetParameters().Select(p => p.ParameterType).ToList())
                    {
                        AddSingletons(ptype.Assembly.GetClassTypes());
                        this.Info("Получение зависимости " + TypeExtensions2.GetTypeName(ptype));
                        if (ptype.IsInterface)
                        {
                            if (TypeExtensions2.GetTypeName(ptype).Equals(nameof(System.IServiceProvider)))
                            {
                                argsList.Add(this);
                            }
                            else
                            {
                                var f = this.Factories.Where(kv => kv.Key.IsImplements(ptype)).First();
                                try 
                                {
                                     
                                        object pobj = f.Value(this);
                                        argsList.Add(pobj);

                                   
                                        
                                }
                                catch (Exception ex) { }
                            }
                        }
                        else
                        {
                            try
                            {
                                object pobj = GetService(ptype);
                                argsList.Add(pobj);
                            }
                            catch(Exception ex)
                            {
                                this.Error($"Ошибка при получении зависимости: {ptype.GetTypeName()}", ex);
                            }
                        }


                    }
                    var newInstance = constructor.Invoke(argsList.ToArray());
                    return (T)newInstance;
                }
             
            }
            catch(Exception ex)
            {
                throw new Exception("Не удалось получить ссылку на сервис типа " + TypeExtensions2.GetTypeName(typeof(T)), ex);
            }
        }
        else
        {
            throw new Exception($"Тип сервис а не определён {type.Name}");
        }
        
    }

    //
    public void AddSingletons(IEnumerable<Type> enumerable)
    {
        foreach (Type ptype in enumerable)
        {
            //this.Info("Приступаю к регистрации сервиса " + ptype.GetTypeName());
            this.Factories[ptype] = (sp) => {
                try
                {
                    var constructor = ptype.GetConstructors().FirstOrDefault();
                    var argsList = new List<object>();
                    foreach (Type ptype in constructor.GetParameters().Select(p => p.ParameterType).ToList())
                    {
                        //this.Info(TypeExtensions2.GetTypeName(ptype));
                        object pobj = GetService(ptype);
                        argsList.Add(pobj);

                    }
                    var newInstance = constructor.Invoke(argsList.ToArray());
                    return newInstance;
                }
                catch (Exception ex)
                {
                    throw new Exception("Не удалось получить ссылку на сервис типа " + TypeExtensions2.GetTypeName(ptype), ex);
                }
            };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disc"></param>
    public void Add(IEnumerable<ServiceDescriptor> disc)
    {
        disc.ToList().ForEach(d =>
        {
            Factories[d.ServiceType] = d.ImplementationFactory;
        });
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filepath"></param>
    public void LoadFile(string filepath)
    {
        try
        {
            var types = Assembly.LoadFile(filepath).GetClassTypes();            
            this.Info(new
            {
                FilePath=filepath,
                ClassTypes=types
            });
        }
        catch (Exception ex)
        {
            this.Error(ex, "Не удалось поджключить сборку из файла: " + filepath);
        }
    }



    


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filepath"></param>
    public void LoadBin(byte[] data)
    {
        try
        {
            var Types = Assembly.Load(data).GetClassTypes();
            var Checked = InputConsole.CheckListTitle<Type>("Выберите классы", Types, type => TypeExtensions2.GetTypeName(type));
            //var Item = InputApplicationProgram.InputEnum<ServiceLifetime>();
        }
        catch (Exception ex)
        {
            this.Error(ex, "Не удалось подключить сборку" );
        }
    }




    /// <summary>
    /// 
    /// </summary>    
    public virtual void AddServiceDescriptions(ServiceDescriptor[] descriptors)
    {
        foreach (var descriptor in descriptors)
        {
            var serviceType = descriptor.ServiceType;
            this.Factories[descriptor.ServiceType] = (sp) => {
                return sp.GetService(descriptor.ServiceType);
            };
        }
    }

    public void Log()    
        => this.Info(this.GetNames().ToJsonOnScreen());         
}


/// <summary>
/// 
/// </summary>
public static class IServiceProviderExtrensions
{
    public static T Get<T>(this IServiceProvider sp)
    {
        if( typeof(T).IsInterface )
        {
            var implementation = typeof(T).Assembly.GetTypes().Where(pt => pt.GetInterfaces().Contains(typeof(T))).FirstOrDefault();
            return (T)sp.GetService(implementation);
        }
        else
        {
            return sp.GetService<T>();
        }        
    }
}
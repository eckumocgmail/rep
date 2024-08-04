using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Поддержка событий изменения свойств обекта
/// </summary>
public class ChangeSupport: MyValidatableObject
{ 
    public bool EnableChangeSupport = true;


    /// <summary>
    /// Когда установлен этот флаг работает правило обратного прототипизированного наследования областей
    /// </summary>
    public bool EnableVerticalBinding = true;

    public virtual void SendEvent(object messageEvent)
    {
        BeforeSendEvent(messageEvent);
    }


    public virtual void BeforeSendEvent(object messageEvent)
    {
        //this.Info(messageEvent.GetType().Name);
        
    }


    protected void FireEvent(PropertyChangedMessage  propertyChangedMessage)
    {
        
    }

    protected void FireEvent(CommonEventMessage<string> message)
    {

    }


    public void MarkAsChanged(){
        this.Info("I has changed =). " + GetType().Name + " " + GetHashCode());
        Changed = true;
    }

    /// <summary>
    /// Структура физического хранения свойств
    /// </summary>
    [JsonIgnore()]
    private ConcurrentDictionary<string, object> Scope = new ConcurrentDictionary<string, object>();


    





    /// <summary>
    /// Признак изменения свойств после последнего обновления
    /// </summary>
    private bool _Changed = false;
    public bool Changed
    {
        get
        {
            return _Changed;
        }
        set
        {
            _Changed = value;
            /*var ctrl = this;
            if (value == true)
            {
                this.Info($"{GetType().Name} Changed "+GetHashCode());
            }*/
        }
    }

    /// <summary>
    /// Событие изменения значения свойства
    /// </summary>
    [JsonIgnore()]
    public Action<object> OnChange;


    /// <summary>
    /// Конструктор обьекта 
    /// </summary>
    public ChangeSupport()
    {
        OnChange = (message) => {
            this.Info(message.ToString());
        };
        
        Changed = false;
    }


    /// <summary>
    /// Получение наименований событий обьявленных в обьекте
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public List<string> GetEventListeners()
    {
        object target = this;
        List<string> listeners = new List<string>();
        foreach (EventInfo evt in target.GetType().GetEvents())
        {
            listeners.Add(evt.Name);
        }
        return listeners;
    }


    /// <summary>
    /// Сброс признака изменения данных, предназначен для применения в процедурах обновления данных
    /// </summary>
    /// <returns></returns>
    public virtual bool WasChanged()
    {
        if (Changed)
        {
            Changed = false;
            return true;
        }
        return false;
    }


    /// <summary>
    /// Создание события при извлечении значения из свойства
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    
    public void OnGet<T>(string key, T value)
    {
        this.Info($"GET: /{GetType().Name}/{GetHashCode()}/{key}/{value}");        
      
      
    }


   
    /// <summary>
    /// Метод получения значения свойства
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    protected T Get<T>(string key)  
    {
        if (Scope.ContainsKey(key))
        {
            T value = (T)Scope[key];
            OnGet<T>(key,value);
            return value;
        }
        else
        {
            if (EnableVerticalBinding)
            {
                if (Scope.ContainsKey("Parent"))
                {
                    ChangeSupport parentSupport = (ChangeSupport)Scope["Parent"];
                    T value = parentSupport.Get<T>(key);
                    OnGet<T>(key, value);
                    return value;
                }
                else
                {
                    throw new Exception($"PropertyNotInitiallized {key}");
                }
            }
            else
            {
                throw new Exception($"PropertyNotInitiallized {key}");
            }
            
        }
    }

    



    




    /// <summary>
    /// Установка значения свойства
    /// </summary>
    /// <typeparam name="T">тип свойства</typeparam>
    /// <param name="key">наименование свойства</param>
    /// <param name="value">значение свойства</param>
    protected void Set<T>(string key, T value)
    {        
        object before = Scope.ContainsKey(key) ? Scope[key] : null;
        //this.Info(GetType().Name + $".{key}:  {before} => {value} ");
        try
        {
            Scope[key] = value;    
            var attrs = CommonUtils.ForProperty(this.GetType(), key);            
            var validationResult = this.Validate(key);
            if(validationResult.Count > 0)
            {
                throw new Exception($"{key} {validationResult}" );
            }
            if( attrs.ContainsKey("UpdateWhenChangedAttribute"))
            {
                bool needChange = CommonUtils.IsTrueValue(attrs["UpdateWhenChangedAttribute"]);
                if(needChange)
                {
                    if (before != null && value != null && (before.ToString() != value.ToString()))
                    {
                        Changed = true;
                    }
                    if ((before != null && value == null) || (before == null && value != null))
                    {
                        Changed = true;
                    }

                }
                else
                {                    
                }

            }
            else
            {
                 
                if (before!=null && value!=null&&(before.ToString() != value.ToString())) { 
                    Changed = true;
                }
                if ((before != null && value == null)|| (before == null && value != null))
                {
                    Changed = true;
                }
            }

            this.SendEvent(new PropertyChangedMessage() { 
                Source = this,
                Property = key,
                Before = before,
                After = value
            });

           
        }
        catch(Exception ex)
        {
            string typeName = this.GetType().Name;
            this.Info("Свойство "+key+" класса "+typeName+" не проверено");

            this.Info(ex);
            //Properties[key] = before;
            //throw;
        }
        
               
        
    }

}




/*
           List<string> errors = this.Validate(key);                 
           if (errors.Count > 0) {
               string message = "";
               errors.ForEach((s) => {
                   message += "\n" + s;
               });
               throw new PropertyValidationException(key,message);
           }
           if((value == null && before != null)|| (value != null && before == null))
           {
               Changed = true;
           }else if(value!=null && before != null)
           {
               if (value.Equals(before) == false)
               {
                   Changed = true;
               }
           }


           switch (typeof(T).Name.ToLower())
           {
               default:
                   OnChange.Invoke(this, new PropertyChangeEvent<T>(new PropertyChangedMessage<T>()
                   {
                       Before = (T)before,
                       After = (T)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
                   throw new UnsupportedException($"В настоящий момент тип данных {typeof(T).Name} неподдерживется");
               case "boolean":
               case "bool":
                   OnChange.Invoke(this, new PropertyChangeEvent<Nullable<bool>>(new PropertyChangedMessage<Nullable<bool>>()
                   {
                       Before = (bool?)before,
                       After = (bool?)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "datetime":
                   OnChange.Invoke(this, new PropertyChangeEvent<Nullable<DateTime>>(new PropertyChangedMessage<Nullable<DateTime>>()
                   {
                       Before = (DateTime?)before,
                       After = (DateTime?)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "string":
                   OnChange.Invoke(this, new PropertyChangeEvent<string>(new PropertyChangedMessage<string>()
                   {
                       Before = (string)before,
                       After = (string)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "object":
                   OnChange.Invoke(this, new PropertyChangeEvent<object>(new PropertyChangedMessage<object>()
                   {
                       Before = (object)before,
                       After = (object)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "int":
                   OnChange.Invoke(this, new PropertyChangeEvent<int?>(new PropertyChangedMessage<int?>()
                   {
                       Before = (int?)before,
                       After = (int?)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "long":
                   OnChange.Invoke(this, new PropertyChangeEvent<long?>(new PropertyChangedMessage<long?>()
                   {
                       Before = (long?)before,
                       After = (long?)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;
               case "float":
                   OnChange.Invoke(this, new PropertyChangeEvent<float?>(new PropertyChangedMessage<float?>()
                   {
                       Before = (float?)before,
                       After = (float?)Properties[key],
                       Property = key,
                       Source = this
                   }));
                   break;

           }*/
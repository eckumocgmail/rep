using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class CollectionBuilder : CollectionBuilder<Dictionary<string, object>>
{
}

public class CollectionBuilder<T>: ICollectionBuilder<T> where T : class
{
    private List<T> Items = new List<T>();
    public CollectionBuilder( ) { }


    public List<T> GetAll() => Items;
    public List<T> Build() => Items;

    public T CreateItem(string title, Func<object, List<string>> validate, ref string[] args)
    {
        if (Typing.IsPrimitive(typeof(T)))
            return CreatePrimitive(title, typeof(T), validate, ref args);
        return InputConsole.Input<T>(title, validate, ref args);
    }

    private T CreatePrimitive(string title, Type type, Func<object, List<string>> validate, ref string[] args)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(T item)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemoveItem()
    {
        throw new System.NotImplementedException();
    }

    public void OnCreateItem(string title, Func<object, List<string>> validate, ref string[] args)
    {
        T p = null;
        List<string> messages = new List<string>();
        do
        {
            p = CreateItem(title, value => new List<string>(), ref args);
            messages = validate(p);
            if (messages != null && messages.Count > 0)
                messages.ToList().ForEach(message => this.Info(message)) ;
        } while (messages.Count() != 0);

        
    }
}

public interface ICollectionBuilder<T>
{
  
}
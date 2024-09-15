
using System.Collections.Concurrent;
using System.Collections.Generic;

public interface APIActiveCollection<T>: APICollection<T> where T : ActiveObject
{
    void DoCheck();
    ConcurrentDictionary<string, object> GetSession(string key);
    ConcurrentDictionary<string, T> GetMemory();
    UserContext Take(string token);
    T Remove(string key);
    string Put(T user);
    bool Has(string key);
}


public interface F<T>
{
    bool Has(string key);
    T Take(string key);
    T Remove(string key);
    string Put(T item);
    string Find(T item);
    IEnumerable<T> GetAll();
    void RemoveAll();
}



public interface ISessionServices 
{
    public void AddHistory(string url);
    public TService Get<TService>();
    public int RegistrateModel(object model);
    public ConcurrentDictionary<int, object> GetModels();
    public object Find(int hash);
    public object FindByHash(int id);
}

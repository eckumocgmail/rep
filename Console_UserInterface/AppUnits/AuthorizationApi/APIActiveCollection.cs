
using System.Collections.Concurrent;
using System.Collections.Generic;

public interface APIActiveCollection<T>: APICollection<T> where T : ActiveObject
{
    void DoCheck();
    ConcurrentDictionary<string, object> GetSession(string key);
    ConcurrentDictionary<string, T> GetMemory();
    T Take(string token);
    T Remove(string key);
    string Put(T user);
    bool Has(string key);
}

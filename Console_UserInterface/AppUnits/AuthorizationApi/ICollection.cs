public interface ICollection<T>
{
    bool Has(string key);
    T Take(string key);
    T Remove(string key);
    string Put(T item);
    string Find(T item);
    IEnumerable<T> GetAll();
    void RemoveAll();
}
